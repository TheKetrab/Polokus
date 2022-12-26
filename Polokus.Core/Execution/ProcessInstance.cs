using Polokus.Core.Helpers;
using Polokus.Core.Interfaces;

namespace Polokus.Core.Execution
{
    public class ProcessInstance : IProcessInstance
    {
        public string Id { get; set; }
        public IWorkflow Workflow { get; }
        public IBpmnProcess BpmnProcess { get; }
        public object TasksMutex { get; } = new object();
        public ActiveTasksManager ActiveTasksManager { get; private set; }
        public IStatusManager StatusManager { get; private set; }

        public IHooksProvider? HooksProvider { get; set; }
        public IProcessInstance? ParentProcessInstance { get; private set; }

        public ICollection<IProcessInstance> ChildrenProcessInstances { get; }
            = new List<IProcessInstance>();

        public ICollection<INodeHandlerWaiter> Waiters { get; }
            = new List<INodeHandlerWaiter>();

        public IDictionary<string, INodeHandler> AvailableNodeHandlers { get; set; }
            = new Dictionary<string, INodeHandler>();



        public ProcessInstance(string id, IWorkflow workflow,
            IBpmnProcess bpmnProcess, IProcessInstance? parent = null)
        {
            Id = id;
            Workflow = workflow;
            ActiveTasksManager = new ActiveTasksManager(this);
            StatusManager = new StatusManager(this);

            BpmnProcess = bpmnProcess;
            ParentProcessInstance = parent;
        }

        public IProcessInstance CreateSubProcessInstance(IBpmnProcess bpmnProcess)
        {
            var processInstance = Workflow.CreateProcessInstance(bpmnProcess, this);
            ChildrenProcessInstances.Add(processInstance);
            processInstance.HooksProvider = HooksProvider;
            return processInstance;
        }

        /// <summary>
        /// This method checks if there is any task that can in the future call target node.
        /// </summary>
        /// <param name="target">FlowNode to call.</param>
        /// <param name="callers">Nodes that already called target.</param>
        public bool ExistsAnotherTaskAbleToCallTarget(IFlowNode target, List<string> callers)
        {
            foreach (var w in Waiters)
            {
                // TODO: zrobic odpowiednia logike i testy
            }

            foreach (var nh in ActiveTasksManager.GetNodeHandlers())
            {
                if (callers.Contains(nh.Node.Id))
                {
                    continue;
                }
                if (nh.Node.Id == target.Id)
                {
                    continue;
                }
                if (BpmnProcess.IsReachable(nh.Node, target))
                {
                    return true;
                }
            }

            return false;

        }


        public INodeHandler? GetNodeHandlerForNodeIfExists(IFlowNode node)
        {
            lock (AvailableNodeHandlers)
            {
                if (AvailableNodeHandlers.ContainsKey(node.Id))
                {
                    return AvailableNodeHandlers[node.Id];
                }
                return null;
            }
        }

        private INodeHandler GetNodeHandlerForNode(IFlowNode node)
        {
            lock (AvailableNodeHandlers)
            {
                INodeHandler? nodeHandler = GetNodeHandlerForNodeIfExists(node);

                if (nodeHandler == null)
                {
                    nodeHandler = Workflow.NodeHandlerFactory.CreateNodeHandler(this, node);
                    AvailableNodeHandlers.Add(node.Id, nodeHandler);
                }

                return nodeHandler;
            }
        }

        public async void ExecuteNode(IFlowNode node, int taskId, INodeCaller? caller)
        {
            INodeHandler nodeHandler = GetNodeHandlerForNode(node);
            ActiveTasksManager.AssignTaskToAnotherNodeHandler(taskId, nodeHandler);

            HooksProvider?.BeforeExecuteNode(Workflow.Id, Id, node.Id, taskId, caller.Id);
            var executionResult = await nodeHandler.Execute(caller, taskId);
            lock (TasksMutex)
            {
                HandleExecutionResult(node, executionResult, taskId);
            }
        }

        public void HandleExecutionResult(IFlowNode node, ProcessResultInfo resultInfo, int taskId)
        {
            switch (resultInfo.State)
            {
                case ProcessResultState.Success:
                    HooksProvider?.AfterExecuteNodeSuccess(Workflow.Id, Id, node.Id, taskId);
                    AvailableNodeHandlers.Remove(node.Id);
                    RunFurtherNodes(node, taskId, resultInfo.SequencesToInvoke!.ToArray());
                    break;
                case ProcessResultState.Failure:
                    HooksProvider?.AfterExecuteNodeFailure(Workflow.Id, Id, node.Id, taskId);
                    ActiveTasksManager.RemoveRunningTask(taskId);
                    AvailableNodeHandlers.Remove(node.Id);
                    break;
                case ProcessResultState.Suspension:
                    HooksProvider?.AfterExecuteNodeSuspension(Workflow.Id, Id, node.Id, taskId);
                    ActiveTasksManager.RemoveRunningTask(taskId);
                    break;
                case ProcessResultState.Cancellation:
                    break; // do nothing, action will be handled by ActiveTasksManager
                default:
                    throw new Exception($"Unhandled state of resultInfo: {resultInfo.State}");

            }
        }

        public void StartNewSequence(IFlowNode firstNode, INodeCaller? caller)
        {
            HooksProvider?.BeforeStartNewSequence(Workflow.Id, Id, firstNode.Id, caller.Id);
            var newTask = ActiveTasksManager.AddNewTask(GetNodeHandlerForNode(firstNode));
            int taskId = newTask.Item1;
            CancellationToken ctoken = newTask.Item2;
            Task task = new Task(() => ExecuteNode(firstNode, taskId, caller), ctoken);
            task.Start();
        }

        private void RunFurtherNodes(IFlowNode node, int taskId, ISequence[] sequences)
        {
            if (sequences.Length == 0)
            {
                ActiveTasksManager.RemoveRunningTask(taskId);
                return;
            }

            for (int i = 1; i < sequences.Length; i++)
            {
                var nextNode = sequences[i]?.Target;
                if (nextNode != null)
                {
                    StartNewSequence(nextNode, node);
                }
            }

            var nn = sequences[0]?.Target;
            if (nn != null)
            {
                ExecuteNode(nn, taskId, node);
            }

        }


    }

}
