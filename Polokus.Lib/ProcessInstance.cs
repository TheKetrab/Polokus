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
        private int timeout = 50; // 5s
        private int msPerTick = 100;
        private int ticks = 0;

        public readonly Guid Guid;
        public readonly BpmnProcess BpmnProcess;
        public bool IsRunning { get; private set; }

        public NodeHandlersDictionary NodeHandlersDictionary { get; private set; }

        private int c = 0;
        public List<int> myTasks = new();


        public ProcessInstance(BpmnProcess bpmnProcess)
        {
            NodeHandlersDictionary = new NodeHandlersDictionary(this);
            NodeHandlersDictionary.SetDefaultNodeHandlers();

            BpmnProcess = bpmnProcess;
            
        }

        public void StartNewSequence(FlowNode firstNode)
        {
            int cc = c;
            c++;

            myTasks.Add(cc);

            Task task = new Task(() => ExecuteNode(firstNode,cc));
            
            task.Start();
        }

        public void ExecuteNode(FlowNode node, int cc)
        {
            var nh = NodeHandlersDictionary.CreateNodeHandlerFor(node);
            if (nh is EmptyNodeHandler)
            {
                myTasks.RemoveAll(x => x == cc);
                return;
            }

            nh.Finished += RunFurtherNodes;
            nh.Failed += KillFailedTask;
            nh.CC = cc;

            nh.Execute(node);
        }

        public void KillFailedTask(object? sender, NodeHandlerFailedEventArgs e)
        {
            Logger.LogWarning("Task failed");
            myTasks.RemoveAll(x => x == e.CC);
        }

        private void RunFurtherNodes(object? sender, NodeHandlerFinishedEventArgs e)
        {
            if (e.NextFlowNodes.Length == 0)
            {
                myTasks.RemoveAll(x => x == e.CC);
                return;
            }

            for (int i=1; i<e.NextFlowNodes.Length; i++)
            {
                StartNewSequence(e.NextFlowNodes[i]);
            }

            ExecuteNode(e.NextFlowNodes[0],e.CC);

        }

        

        public async Task RunProcess()
        {
            StartNewSequence(this.BpmnProcess.StartNode);
            while (myTasks.Any())
            {
                await Task.Delay(msPerTick);
            }
            Console.WriteLine("end");
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
