using Polokus.Lib.Models;
using Polokus.Lib.Models.BpmnObjects.Xsd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.NodeHandlers
{
    public class ManualTaskNodeHandler : NodeHandler<tManualTask>
    {
        public ManualTaskNodeHandler(FlowNode<tManualTask> typedNode) : base(typedNode)
        {
        }

        protected override Task Process(IFlowNode? caller)
        {
            Console.WriteLine($"Waiting for manual task: {Node.Name}. Press enter.");
            Console.ReadLine();
            return Task.CompletedTask;
        }
    }
}
