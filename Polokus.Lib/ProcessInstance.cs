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
        public readonly Guid Guid;
        public readonly BpmnProcess BpmnProcess;
        public bool IsRunning { get; private set; }

        public NodeHandlersManager NodeHandlerManager { get; private set; }



        public ProcessInstance(BpmnProcess bpmnProcess)
        {
            NodeHandlerManager = new NodeHandlersManager(this);
            NodeHandlerManager.SetDefaultNodeHandlers();

            


            BpmnProcess = bpmnProcess;
            
        }

        public void Stop()
        {

        }

        public void Start()
        {
            NodeHandlerManager.GetNodeHandlers().ForEach(
                x => x.Finished += (s, e) => 
                {
                    Console.WriteLine($"{e.Name} Finished"); 
                });

            NodeHandlerManager.Execute(BpmnProcess.StartNode);


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
