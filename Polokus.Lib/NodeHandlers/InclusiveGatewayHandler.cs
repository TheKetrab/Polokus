using Polokus.Lib.Models;
using Polokus.Lib.Models.BpmnObjects.Xsd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.NodeHandlers
{
    public class InclusiveGatewayHandler : NodeHandler<tInclusiveGateway>
    {
        public InclusiveGatewayHandler(ProcessInstance process) : base(process)
        {
        }




    }
}
