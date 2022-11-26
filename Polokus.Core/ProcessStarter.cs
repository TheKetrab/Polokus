using Polokus.Core.Helpers;
using Polokus.Core.Interfaces;
using Polokus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core
{
    public class ProcessStarter : IProcessStarter
    {
        public IContextInstance ContextInstance { get; }

        public IBpmnProcess BpmnProcess { get; }

        public IFlowNode StartNode { get; }

        public string Id { get; }


        public ProcessStarter(IContextInstance contextInstance, IBpmnProcess bpmnProcess, IFlowNode startNode)
        {
            ContextInstance = contextInstance;
            BpmnProcess = bpmnProcess;
            StartNode = startNode;
            Id = Utils.GetStarterName(ContextInstance.Id, BpmnProcess.Id, StartNode.Id);
        }
    }
}
