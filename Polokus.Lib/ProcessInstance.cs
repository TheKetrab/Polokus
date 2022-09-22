using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Linq;
using System.Xml.Serialization;
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

        public void StartNewSequence(FlowNode firstNode, string? predecessor)
        {
            hooksProvider?.OnNewSequence();
            int taskId = ActiveTasksManager.AddNewTask();
            Task task = new Task(() => ExecuteNode(firstNode, taskId, predecessor));            
            task.Start();
        }

        public void ExecuteNode(FlowNode node, int taskId, string? predecessor)
        {
            INodeHandler nh;
            if (AvailableNodeHandlers.ContainsKey(node.Id))
            {
                nh = AvailableNodeHandlers[node.Id];
            }
            else
            {
                nh = NodeHandlersDictionary.CreateNodeHandlerFor(node);
                if (nh is EmptyNodeHandler)
                {
                    ActiveTasksManager.RemoveRunningTask(taskId);
                    return;
                }

                nh.Finished += RemoveAvailableNodeHandler;
                nh.Finished += RunFurtherNodes;

                nh.Suspended += RemoveInvokingTask;

                nh.Failed += KillFailedTask;

                if (hooksProvider != null)
                {
                    nh.Finished += hooksProvider.OnNodeHandlerFinished;
                    nh.Suspended += hooksProvider.OnNodeHandlerSuspended;
                    nh.Failed += hooksProvider.OnNodeHandlerFailed;
                }


                AvailableNodeHandlers.Add(node.Id, nh);
            }


            lock (nh)
            {
                hooksProvider?.OnExecute(node,taskId,predecessor);
                nh.Execute(node, taskId, predecessor);
            }
        }

        private void RemoveInvokingTask(object? sender, NodeHandlerSuspendedEventArgs e)
        {
            ActiveTasksManager.RemoveRunningTask(e.TaskId);
        }

        private void RemoveAvailableNodeHandler(object? sender, NodeHandlerFinishedEventArgs e)
        {
            AvailableNodeHandlers.Remove(e.CurrentNode.Id);
        }

        public void KillFailedTask(object? sender, NodeHandlerFailedEventArgs e)
        {
            Logger.LogWarning("Task failed");
            ActiveTasksManager.RemoveRunningTask(e.TaskId);
        }

        private void RunFurtherNodes(object? sender, NodeHandlerFinishedEventArgs e)
        {
            if (e.NextFlowNodes.Length == 0)
            {
                ActiveTasksManager.RemoveRunningTask(e.TaskId);
                return;
            }

            for (int i=1; i<e.NextFlowNodes.Length; i++)
            {
                StartNewSequence(e.NextFlowNodes[i], e.CurrentNode.Id);
            }

            ExecuteNode(e.NextFlowNodes[0],e.TaskId,e.CurrentNode.Id);

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
