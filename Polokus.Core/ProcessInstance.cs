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
        private DateTime? _beginTime;
        private DateTime? _finishTime;
        public TimeSpan TotalTime =>
            (_beginTime == null) ? TimeSpan.Zero
                : ((_finishTime == null) ? (DateTime.Now - _beginTime.Value)
                : _finishTime.Value - _beginTime.Value);
                        
        public bool IsActive { get; private set; }

        public IContextInstance ContextInstance { get; }

        public IBpmnProcess BpmnProcess { get; }
        public IDictionary<string,INodeHandler> AvailableNodeHandlers { get; set; } = new Dictionary<string, INodeHandler>();

        public IEnumerable<ICatchingIntermediateEvent> CatchingIntermediateEvents => throw new NotImplementedException();



        public object TasksMutex { get; } = new object();
        public ActiveTasksManager ActiveTasksManager { get; private set; }

        public bool IsStarted { get; private set;  }

        public bool IsFinished { get; private set; }

        public IHooksProvider? hooksProvider;


        public ProcessInstance(IContextInstance contextInstance, IBpmnProcess bpmnProcess, IHooksProvider? hooksProvider = null)
        {
            ContextInstance = contextInstance;
            ActiveTasksManager = new ActiveTasksManager(this);

            BpmnProcess = bpmnProcess;
            this.hooksProvider = hooksProvider;
        }

        
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="callers">Callers are nodes that already called target</param>
        /// <returns></returns>
        public bool ExistsAnotherTaskAbleToCallTarget(IFlowNode target, List<string> callers)
        {
            foreach (var t in ActiveTasksManager.ActiveTasks)
            {
                if (t.Value is INodeHandler nh)
                {
                    if (callers.Contains(nh.Node.Id))
                    {
                        continue;
                    }
                    if (BpmnProcess.IsReachable(nh.Node, target))
                    {
                        return true;
                    }
                }
            }

            return false;
            
        }

        INodeHandler GetNodeHandlerForNode(IFlowNode node)
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

        public async void ExecuteNode(IFlowNode node, int taskId, IFlowNode? caller)
        {
            INodeHandler nodeHandler = GetNodeHandlerForNode(node);
            //ActiveTasksManager.ActiveTasks[taskId] = nodeHandler;

            hooksProvider?.BeforeExecuteNode(node, taskId, caller);
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
                    hooksProvider?.AfterExecuteNodeSuccess(node, taskId);
                    AvailableNodeHandlers.Remove(node.Id);
                    RunFurtherNodes(node, taskId, resultInfo.SequencesToInvoke.ToArray());
                    break;
                case ProcessResultState.Failure:
                    hooksProvider?.AfterExecuteNodeFailure(node, taskId);
                    ActiveTasksManager.RemoveRunningTask(taskId);
                    AvailableNodeHandlers.Remove(node.Id);
                    break;
                case ProcessResultState.Suspension:
                    hooksProvider?.AfterExecuteNodeSuspension(node, taskId);
                    ActiveTasksManager.RemoveRunningTask(taskId);
                    break;
                    // case ProcessResultState. invoke self?????

            }
        }

        public void StartNewSequence(IFlowNode firstNode, IFlowNode? caller)
        {
            hooksProvider?.BeforeStartNewSequence(firstNode, caller);
            int taskId = ActiveTasksManager.AddNewTask(GetNodeHandlerForNode(firstNode));
            Task task = new Task(() => ExecuteNode(firstNode, taskId, caller));
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
        



        public void Stop()
        {
            IsActive = false;
            // TODO: stop all tasks

        }

        public void Kill()
        {
            Finish();
            // TODO: kill all tasks
        }

        public bool IsRunning()
        {
            return IsActive && ActiveTasksManager.AnyRunning();
        }

        public void Begin(IFlowNode startNode)
        {
            if (!startNode.IsStartNode())
            {
                throw new InvalidOperationException("Not allowed to start process on node which is not 'StartNode'.");
            }

            _beginTime = DateTime.Now;
            IsActive = true;
            StartNewSequence(startNode, null);
        }

        public void Run()
        {
            IsActive = true;
            // TODO: rerun stopped tasks
        }

        public void Finish()
        {
            IsFinished = true;
            _finishTime = DateTime.Now;
        }
    }

}
