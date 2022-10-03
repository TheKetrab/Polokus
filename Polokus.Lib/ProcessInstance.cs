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

namespace Polokus.Lib
{


    public class ProcessInstance
    {


        public readonly BpmnProcess BpmnProcess;
        public bool IsRunning { get; private set; }

        public NodeHandlersDictionary NodeHandlersDictionary { get; private set; }
        public Dictionary<string, INodeHandler> AvailableNodeHandlers { get; set; } = new();

        public ActiveTasksManager ActiveTasksManager { get; private set; } = new();


        protected IHooksProvider? hooksProvider;


        public ProcessInstance(BpmnProcess bpmnProcess, IHooksProvider? hooksProvider = null)
        {
            NodeHandlersDictionary = new NodeHandlersDictionary(this);
            NodeHandlersDictionary.SetDefaultNodeHandlers();

            BpmnProcess = bpmnProcess;
            this.hooksProvider = hooksProvider;
        }

        public void StartNewSequence(IFlowNode firstNode, IFlowNode? caller)
        {
            hooksProvider?.OnNewSequence();
            int taskId = ActiveTasksManager.AddNewTask();
            Task task = new Task(() => ExecuteNode(firstNode, taskId, caller));            
            task.Start();
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
                AvailableNodeHandlers.Add(node.Id, nodeHandler);
                return nodeHandler;

            }
        }

        public async void ExecuteNode(IFlowNode node, int taskId, IFlowNode? caller)
        {
            INodeHandler nodeHandler = GetNodeHandlerForNode(node);

            hooksProvider?.OnExecute(node, taskId, caller?.Id);
            var executionResult = await nodeHandler.Execute(caller);
            HandleExecutionResult(node, executionResult, taskId);
        }

        public void HandleExecutionResult(IFlowNode node, ProcessResultInfo resultInfo, int taskId)
        {
            switch (resultInfo.State)
            {
                case ProcessResultState.Success:
                    hooksProvider?.OnFinished(node, taskId);
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

        public void Execution()
        {
            // stan
            // watki
            // to gdzie jestesmy (wiele miejsc - tyle ile watkow???)
            // jesli jakis skonczy to mamy event, czyli lecimy do nastepnika

            // proces przetrzymuje tylko stan i odpala kolejne wezly
            // wezly zarzadzaja stanem, lacza watki itp
        }


    }

}
