using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Linq;
using System.Xml.Serialization;
using Polokus.Lib.Factories;
using Polokus.Lib.Hooks;
using Polokus.Lib.Models;
using Polokus.Lib.Models.BpmnObjects.Xsd;
using Polokus.Lib.NodeHandlers;
using Polokus.Lib.NodeHandlers.Abstract;

namespace Polokus.Lib
{


    public class ProcessInstance
    {
        public object TasksMutex { get; } = new object();

        public readonly BpmnProcess BpmnProcess;
        public bool IsRunning { get; private set; }

        public NodeHandlersDictionary NodeHandlersDictionary { get; private set; }
        public Dictionary<string, INodeHandler> AvailableNodeHandlers { get; set; } = new();

        public ActiveTasksManager ActiveTasksManager { get; private set; }


        public IHooksProvider? hooksProvider;


        public ProcessInstance(BpmnProcess bpmnProcess, IHooksProvider? hooksProvider = null)
        {
            NodeHandlersDictionary = new NodeHandlersDictionary(this);
            NodeHandlersDictionary.SetDefaultNodeHandlers();
            ActiveTasksManager = new ActiveTasksManager(this);

            BpmnProcess = bpmnProcess;
            this.hooksProvider = hooksProvider;
        }

        
        public bool IsReachable(IFlowNode src, IFlowNode dest)
        {
            return IsReachableDFS(src, dest, new HashSet<IFlowNode>());
        }

        private bool IsReachableDFS(IFlowNode src, IFlowNode dest, HashSet<IFlowNode> visited)
        {
            if (src == dest)
            {
                return true;
            }

            if (visited.Contains(src))
            {
                return false;
            }

            foreach (var x in src.Outgoing)
            {
                var newHashSet = new HashSet<IFlowNode>(visited);
                newHashSet.Add(src);
                bool res = IsReachableDFS(x.Target, dest, newHashSet);
                if (res == true)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="callers">Callers are nodes that already called target</param>
        /// <returns></returns>
        public bool SomebodyWhoDidntCallTheNodeCanCallItInTheFuture(IFlowNode target, List<string> callers)
        {
            foreach (var t in ActiveTasksManager.ActiveTasks)
            {
                if (t.Value is INodeHandler nh)
                {
                    if (callers.Contains(nh.Node.Id))
                    {
                        continue;
                    }
                    if (IsReachable(nh.Node, target))
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

                INodeHandler nodeHandler = NodeHandlersDictionary.CreateNodeHandlerFor(node);
                nodeHandler.ProcessInstance = this;
                AvailableNodeHandlers.Add(node.Id, nodeHandler);
                return nodeHandler;

            }
        }

        public async void ExecuteNode(IFlowNode node, int taskId, IFlowNode? caller)
        {
            INodeHandler nodeHandler = GetNodeHandlerForNode(node);
            //ActiveTasksManager.ActiveTasks[taskId] = nodeHandler;

            hooksProvider?.OnExecute(node, taskId, caller?.Id);
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
                    hooksProvider?.OnFinished(node, taskId);
                    AvailableNodeHandlers.Remove(node.Id);
                    RunFurtherNodes(node, taskId, resultInfo.SequencesToInvoke.ToArray());
                    break;
                case ProcessResultState.Failure:
                    hooksProvider?.OnFailure(node, taskId);
                    ActiveTasksManager.RemoveRunningTask(taskId);
                    AvailableNodeHandlers.Remove(node.Id);
                    break;
                case ProcessResultState.Suspension:
                    hooksProvider?.OnSuspension(node, taskId);
                    ActiveTasksManager.RemoveRunningTask(taskId);
                    break;
                    // case ProcessResultState. invoke self?????

            }
        }

        public void StartNewSequence(IFlowNode firstNode, IFlowNode? caller)
        {
            //hooksProvider?.OnNewSequence();
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

        private void RunFurtherNodes(IFlowNode node, int taskId, Sequence[] sequences)
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
        

        // secTimeout < 0 == no timeout
        public async Task<bool> RunProcess(int secTimeout = -1)
        {
            DateTime start = DateTime.Now;

            StartNewSequence(this.BpmnProcess.StartNode,null);


            while (ActiveTasksManager.AnyRunning())
            {
                await Task.Delay(100);

                if (secTimeout >= 0)
                {
                    DateTime tick = DateTime.Now;
                    TimeSpan span = tick - start;
                    if (span.Seconds > secTimeout)
                    {
                        hooksProvider?.OnTimeOut();
                        return false;
                    }

                }
            }
            return true;
        }


    }

}
