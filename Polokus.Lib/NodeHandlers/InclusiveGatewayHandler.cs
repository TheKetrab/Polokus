using Polokus.Lib.Models;
using Polokus.Lib.Models.BpmnObjects.Xsd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.NodeHandlers
{
    /*
    public class InclusiveGatewayHandler : NodeHandler<tInclusiveGateway>
    {
        public InclusiveGatewayHandler(ProcessInstance process) : base(process)
        {
        }

        private List<string> invokedBy = new();
        public override Task<int> ProcessNode(FlowNode node, string? predecessorId)
        {
            Console.WriteLine("trying invoke inclusive");

            if (predecessorId != null)
            {
                invokedBy.Add(predecessorId);
            }

            bool canRunFurther = node.Incoming.All(x => invokedBy.Contains(x.Id));

            if (canRunFurther)
            {
                nextFlowNodes = node.Outgoing.ToList();
            }

            int result = canRunFurther ? 1 : 0;
            return Task.FromResult(result);
        }

    }
        */
}
