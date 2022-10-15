using Polokus.Core.Interfaces;
using Polokus.Core.Models;
using Polokus.Core.Models.BpmnObjects.Xsd;
using Polokus.Core.NodeHandlers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.NodeHandlers
{
    public class ManualTaskNodeHandler : NodeHandler<tManualTask>
    {
        public ManualTaskNodeHandler(ProcessInstance processInstance, FlowNode<tManualTask> typedNode)
            : base(processInstance, typedNode)
        {
        }

        protected override Task Action(IFlowNode? caller)
        {
            Console.WriteLine($"Waiting for manual task: {Node.Name}. Press enter.");
            Console.ReadLine();
            return Task.CompletedTask;
        }
    }
}
