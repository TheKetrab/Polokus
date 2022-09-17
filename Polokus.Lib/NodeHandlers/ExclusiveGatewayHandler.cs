using Polokus.Lib.Models;
using Polokus.Lib.Models.BpmnObjects.Xsd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.NodeHandlers
{
    public class ExclusiveGatewayHandler : NodeHandler<tExclusiveGateway>
    {
        public ExclusiveGatewayHandler(ProcessInstance process) : base(process)
        {
            
        }

        public override Task<bool> ProcessNode(FlowNode node)
        {
            return base.ProcessNode(node);
        }


    }
}
