using Polokus.Core.BpmnModels;
using Polokus.Core.Execution.Scripting;
using Polokus.Core.Helpers;
using Polokus.Core.Interfaces.Exceptions;
using Polokus.Core.Interfaces.Execution.NodeHandlers;
using Polokus.Core.Interfaces.Managers;
using Polokus.Core.Interfaces.Serialization;
using Polokus.Core.Managers;
using Polokus.Core.Serialization;

namespace Polokus.Core.Execution
{
    public class ProcessInstance : IProcessInstance,
        IRestorable<IProcessInstanceSnapShot>, IDumpable<ProcessInstanceSnapShot>
    {
        public string Id { get; private set; }
        public IWorkflow Workflow { get; private set; }
        public IBpmnProcess BpmnProcess { get; private set; }
        public object TasksMutex { get; } = new object();
        public IActiveTasksManager ActiveTasksManager { get; private set; }
        public IStatusManager StatusManager { get; private set; }
        public IScriptProvider ScriptProvider { get; private set; }
        public IHooksProvider? HooksProvider { get; set; }

        public IProcessInstance? ParentProcessInstance { get; private set; }

        public ICollection<IProcessInstance> ChildrenProcessInstances { get; }
            = new List<IProcessInstance>();

        public IEnumerable<INodeHandlerWaiter> Waiters
        {
            get
            {
                List<INodeHandlerWaiter> res = new();
                res.AddRange(this.Workflow.TimeManager.GetWaiters(Id));
                res.AddRange(this.Workflow.MessageManager.GetWaiters(Id));
                res.AddRange(this.Workflow.SignalManager.GetWaiters(Id));
                return res;
            }
        }

        /// <summary>
        /// Removes waiters for this process instance including cancelling them.
        /// </summary>
        public void KillWaiters()
        {
            {
                var waiters = this.Workflow.TimeManager.GetWaiters(Id);
                waiters.ForEach(x => this.Workflow.TimeManager.RemoveWaiter(x.Id));
            }
            {
                var waiters = this.Workflow.MessageManager.GetWaiters(Id);
                waiters.ForEach(x => this.Workflow.MessageManager.RemoveWaiter(x.Id));
            }
            {
                var waiters = this.Workflow.SignalManager.GetWaiters(Id);
                waiters.ForEach(x => this.Workflow.SignalManager.RemoveWaiter(x.Id));
            }
        }

        public IDictionary<string, INodeHandler> AvailableNodeHandlers { get; set; }
            = new Dictionary<string, INodeHandler>();

        public ICollection<string> FailedExecutionNodeIds { get; private set; } = new List<string>();
        public ICollection<string> AwaitingTokens { get; } = new List<string>();


        public ProcessInstance(string id, IWorkflow workflow,
            IBpmnProcess bpmnProcess, IProcessInstance? parent = null)
        {
            Id = id;
            Workflow = workflow;
            ActiveTasksManager = new ActiveTasksManager(this);
            StatusManager = new StatusManager(this);

            ScriptProvider = Settings.ScriptingLanguage switch
            {
                "CS" => new CSScriptProvider(),
                "JS" => new JSScriptProvider(),
                _ => throw new SettingNotFoundException("ScriptingLanguage")
            };

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
            var activeNodes = ActiveTasksManager.GetNodeHandlers().ToList().Select(nh => nh.Node).ToList();
            var nodesFromWaiters = Waiters.ToList().Select(x => x.NodeToCall).ToList();

            var potentialCallers = activeNodes.Concat(nodesFromWaiters).ToList();

            foreach (var node in potentialCallers)
            {
                if (callers.Contains(node.Id))
                {
                    continue;
                }
                if (node.Id == target.Id)
                {
                    continue;
                }
                if (BpmnProcess.IsReachable(node, target))
                {
                    return true;
                }
            }

            return false;
        }

        public void Log(string info, MsgType type)
        {
            Workflow.Log(Id, info, type);
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

            HooksProvider?.BeforeExecuteNode(Workflow.Id, Id, node.Id, taskId, caller?.Id);
            var executionResult = await nodeHandler.Execute(caller, taskId);
            lock (TasksMutex)
            {
                HandleExecutionResult(node, executionResult, taskId);
            }
            if (executionResult.State == ProcessResultState.Success)
            {
                bool forceAllNewSequences = (executionResult.Message == "forceAllNewSequences");
                RunFurtherNodes(node, taskId, executionResult.SequencesToInvoke!.ToArray(), forceAllNewSequences);
            }
        }

        public void HandleExecutionResult(IFlowNode node, IProcessResultInfo resultInfo, int taskId)
        {
            switch (resultInfo.State)
            {
                case ProcessResultState.Success:
                    HooksProvider?.AfterExecuteNodeSuccess(Workflow.Id, Id, node.Id, taskId);
                    AvailableNodeHandlers.Remove(node.Id);
                    break;
                case ProcessResultState.Failure:
                    this.FailedExecutionNodeIds.Add(node.Id);
                    HooksProvider?.AfterExecuteNodeFailure(Workflow.Id, Id, node.Id, taskId);
                    ActiveTasksManager.RemoveRunningTask(taskId);
                    AvailableNodeHandlers.Remove(node.Id);
                    break;
                case ProcessResultState.Suspension:
                    HooksProvider?.AfterExecuteNodeSuspension(Workflow.Id, Id, node.Id, taskId);
                    ActiveTasksManager.RemoveRunningTask(taskId);
                    break;
                case ProcessResultState.Cancellation:
                    ActiveTasksManager.RemoveRunningTask(taskId);
                    break;
                default:
                    throw new Exception($"Unhandled state of resultInfo: {resultInfo.State}");

            }
        }

        public void StartNewSequence(IFlowNode firstNode, INodeCaller? caller)
        {
            HooksProvider?.BeforeStartNewSequence(Workflow.Id, Id, firstNode.Id, caller?.Id);

            INodeHandler nodeHandlerForNode;
            if (caller != null && caller is NodeHandlerWaiter)
            {
                if (firstNode is BoundaryEvent)
                {
                    // Waiters can create NodeHandlers only for coundary events!
                    nodeHandlerForNode = GetNodeHandlerForNode(firstNode);
                }
                else
                {
                    var nh = GetNodeHandlerForNodeIfExists(firstNode);
                    if (nh == null)
                    {
                        // Waiters should not create NodeHandlers. If NodeHandler for this node does not exist
                        // it means that the waiter is an 'orphan' and will be deleted soon.
                        // So do not start a sequence!
                        return;
                    }

                    nodeHandlerForNode = nh;
                }

            }
            else
            {
                nodeHandlerForNode = GetNodeHandlerForNode(firstNode);
            }

            var newTask = ActiveTasksManager.AddNewTask(nodeHandlerForNode);
            int taskId = newTask.Item1;
            Task task = new Task(() => ExecuteNode(firstNode, taskId, caller), newTask.Item2.Token);
            task.Start();
        }

        private void RunFurtherNodes(IFlowNode node, int taskId, ISequence[] sequences, bool forceAllNewSequences = false)
        {
            if (StatusManager.IsStopped || StatusManager.IsPaused)
            {
                return;
            }

            if (sequences.Length == 0)
            {
                ActiveTasksManager.RemoveRunningTask(taskId);
                return;
            }

            int start = forceAllNewSequences ? 0 : 1;

            for (int i = start; i < sequences.Length; i++)
            {
                var nextNode = sequences[i]?.Target;
                if (nextNode != null)
                {
                    StartNewSequence(nextNode, node);
                }
            }

            if (!forceAllNewSequences)
            {
                var nn = sequences[0]?.Target;
                if (nn != null)
                {
                    ExecuteNode(nn, taskId, node);
                }

            }

        }

        public void Restore(IPolokusMaster master, IProcessInstanceSnapShot source)
        {
            var id = source.Id;
            var wf = master.GetWorkflow(source.WorkflowId);
            var bpmn = wf.BpmnWorkflow.BpmnProcesses.FirstOrDefault(x => x.Id == source.BpmnProcessId)
                ?? throw new Exception($"Unable to find bpmn with id {source.BpmnProcessId}");

            this.Id = id;
            this.Workflow = wf;
            this.BpmnProcess = bpmn;

            // restore hooks provider
            this.HooksProvider = wf.HooksProvider;

            if (!string.IsNullOrEmpty(source.ParentProcessInstanceId))
            {
                this.ParentProcessInstance = wf.GetProcessInstanceById(source.ParentProcessInstanceId);
                this.ParentProcessInstance?.ChildrenProcessInstances.Add(this);
            }

            ((StatusManager)this.StatusManager).Restore(master, source.Status);
            this.FailedExecutionNodeIds = source.FailedExecutionNodeIds.ToList();

            source.AciveNodes.ForEach(x => this.StartNewSequence(bpmn.GetNodeById(x), null)); // executes new sequences (on another threads)
            Thread.Sleep(300);

            // restore waiters
            foreach (var nodeId in source.IdsOfNodesThatHadWaiters)
            {
                var node = bpmn.GetNodeById(nodeId);
                switch (node.RequireWaiter)
                {
                    case WaiterType.Timer:
                        wf.TimeManager.RegisterWaiter(this, node, true);
                        break;
                    case WaiterType.Message:
                        wf.MessageManager.RegisterWaiter(this, node, true);
                        break;
                    case WaiterType.Signal:
                        wf.SignalManager.RegisterWaiter(this, node, true);
                        break;
                    default:
                        throw new Exception("Not handled situation");
                }
            }

        }

        public ProcessInstanceSnapShot Dump()
        {
            return new ProcessInstanceSnapShot()
            {
                Id = this.Id,
                WorkflowId = this.Workflow.Id,
                BpmnProcessId = this.BpmnProcess.Id,
                AciveNodes = this.ActiveTasksManager.Dump(),
                Status = this.StatusManager.Status.ToString(),
                FailedExecutionNodeIds = this.FailedExecutionNodeIds.ToArray(),
                IdsOfNodesThatHadWaiters = this.Waiters.Select(x => x.NodeToCall.Id).ToArray(),
                ParentProcessInstanceId = this.ParentProcessInstance?.Id ?? ""
            };
        }

        public void RemoveAwaitingToken(string token)
        {
            if (AwaitingTokens.Contains(token))
            {
                AwaitingTokens.Remove(token);
            }
        }
    }

}
