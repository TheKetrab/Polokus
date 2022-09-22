using Polokus.Lib.Models;
using Polokus.Lib.Models.BpmnObjects.Xsd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.NodeHandlers
{
    public class ParallelGatewayNodeHandler : NodeHandler<tParallelGateway>
    {
        public ParallelGatewayNodeHandler(ProcessInstance process) : base(process)
        {

        }

        public override Task<int> ProcessNode(FlowNode node, string? predecessor)
        {
            nextFlowNodes = node.Outgoing.ToList();
            return Task.FromResult(1);
        }
    }
}
