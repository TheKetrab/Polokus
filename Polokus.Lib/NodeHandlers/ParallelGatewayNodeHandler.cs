using Polokus.Lib.Models;
using Polokus.Lib.Models.BpmnObjects.Xsd;
using Polokus.Lib.NodeHandlers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.NodeHandlers
{
    public class ParallelGatewayNodeHandler : JoiningNodeHandler<tParallelGateway>
    {
        public ParallelGatewayNodeHandler(FlowNode<tParallelGateway> node) : base(node)
        {

        }
    }
}
