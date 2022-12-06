using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Linq;
using System.Xml.Serialization;
using Polokus.Core.Factories;
using Polokus.Core.Helpers;
using Polokus.Core.Interfaces;
using Polokus.Core.Models;
using Polokus.Core.Models.BpmnObjects.Xsd;
using Polokus.Core.NodeHandlers.Abstract;

namespace Polokus.Core
{


    public class ProcessInstance : IProcessInstance
    {
        public string Id { get; set; }

        private DateTime? _beginTime;
        private DateTime? _finishTime;
        public TimeSpan TotalTime =>
            (_beginTime == null) ? TimeSpan.Zero
                : ((_finishTime == null) ? (DateTime.Now - _beginTime.Value)
                : _finishTime.Value - _beginTime.Value);

        public ProcessStatus Status { get; set; } = ProcessStatus.Initialized;
        public bool IsActive { get; private set; }

        public IContextInstance ContextInstance { get; }

        public IBpmnProcess BpmnProcess { get; }
        public IDictionary<string,INodeHandler> AvailableNodeHandlers { get; set; } = new Dictionary<string, INodeHandler>();

        public IEnumerable<ICatchingIntermediateEvent> CatchingIntermediateEvents => throw new NotImplementedException();



        public object TasksMutex { get; } = new object();
        public ActiveTasksManager ActiveTasksManager { get; private set; }

        public bool IsStarted { get; private set;  }

        public bool IsFinished { get; private set; }

        public ICollection<INodeHandlerWaiter> Waiters { get; } = new List<INodeHandlerWaiter>();

        public IHooksProvider? HooksProvider;

        public ProcessInstance(string id, IContextInstance contextInstance, IBpmnProcess bpmnProcess, IHooksProvider? hooksProvider = null)
        {
            Id = id;
            ContextInstance = contextInstance;
            ActiveTasksManager = new ActiveTasksManager(this);

            BpmnProcess = bpmnProcess;
            HooksProvider = hooksProvider;
        }

        
        public IProcessInstance? ParentProcessInstance { get; private set; }
        public ICollection<IProcessInstance> ChildrenProcessInstances { get; } = new List<IProcessInstance>();

        public IProcessInstance CreateSubProcessInstance(IBpmnProcess bpmnProcess)
        {
            ProcessInstance processInstance = (ProcessInstance)ContextInstance.CreateProcessInstance(bpmnProcess);
            processInstance.ParentProcessInstance = this;
            this.ChildrenProcessInstances.Add(processInstance);
            processInstance.HooksProvider = this.HooksProvider;
            return processInstance;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="callers">Callers are nodes that already called target</param>
        /// <returns></returns>
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
                if (AvailableNodeHandlers.ContainsKey(node.Id))
                {
                    return AvailableNodeHandlers[node.Id];
                }

                INodeHandler nodeHandler = ContextInstance.NodeHandlerFactory.CreateNodeHandler(this,node);
                AvailableNodeHandlers.Add(node.Id, nodeHandler);
                return nodeHandler;

            }
        }

        public async void ExecuteNode(IFlowNode node, int taskId, INodeCaller? caller)
        {
            INodeHandler nodeHandler = GetNodeHandlerForNode(node);
            ActiveTasksManager.AssignTaskToAnotherNodeHandler(taskId, nodeHandler);

            HooksProvider?.BeforeExecuteNode(Id, node, taskId, caller);
            var executionResult = await nodeHandler.Execute(caller,taskId);
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
                    HooksProvider?.AfterExecuteNodeSuccess(Id, node, taskId);
                    AvailableNodeHandlers.Remove(node.Id);
                    RunFurtherNodes(node, taskId, resultInfo.SequencesToInvoke.ToArray());
                    break;
                case ProcessResultState.Failure:
                    HooksProvider?.AfterExecuteNodeFailure(Id, node, taskId);
                    ActiveTasksManager.RemoveRunningTask(taskId);
                    AvailableNodeHandlers.Remove(node.Id);
                    break;
                case ProcessResultState.Suspension:
                    HooksProvider?.AfterExecuteNodeSuspension(Id, node, taskId);
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
            HooksProvider?.BeforeStartNewSequence(Id,firstNode, caller);
            var newTask = ActiveTasksManager.AddNewTask(GetNodeHandlerForNode(firstNode));
            int taskId = newTask.Item1;
            CancellationToken ctoken = newTask.Item2;
            Task task = new Task(() => ExecuteNode(firstNode, taskId, caller), ctoken);
            task.Start();
        }

        //public void StartNewSelfTrigger(INodeHandler nodeHandler)
        //{
        //    // hooksProvider?.OnNewSelfTrigger(); // TODO
        //    int taskId = ActiveTasksManager.AddNewTask();
        //    Task task = new Task(() =>
        //    {
        //        while (nodeHandler != null)
        //        {
        //            Task.Delay(100);
        //            ExecuteNode(nodeHandler.Node, taskId, null);
        //            // todo: czy nie trzeba recznie ustawiac nodeHandler = null ???
        //        }
        //    });
        //    task.Start();
        //}

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
        



        public void Pause()
        {
            ActiveTasksManager.Pause();
            Status = ProcessStatus.Paused;
            HooksProvider?.OnStatusChanged(Id);
        }

        public void Stop()
        {
            ActiveTasksManager.Stop();
            IsActive = false;
        }

        public bool IsRunning()
        {
            return IsActive && (ActiveTasksManager.AnyRunning() || Waiters.Any());
        }

        public void Begin(IFlowNode startNode)
        {
            if (!startNode.IsStartNode())
            {
                throw new InvalidOperationException("Not allowed to start process on node which is not 'StartNode'.");
            }

            _beginTime = DateTime.Now;
            IsActive = true;
            Status = ProcessStatus.Running;
            HooksProvider?.OnStatusChanged(Id);
            StartNewSequence(startNode, null);
        }

        public void Resume()
        {
            ActiveTasksManager.Resume();
            Status = ProcessStatus.Running;
            HooksProvider?.OnStatusChanged(Id);
        }

        public void Finish()
        {
            if (IsRunning())
            {
                throw new Exception("Unable to finish running process.");
            }

            IsFinished = true;
            _finishTime = DateTime.Now;
            Status = ProcessStatus.Finished;
            HooksProvider?.OnStatusChanged(Id);
        }

    }

}
