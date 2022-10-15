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
    public class EndEventHandler : NodeHandler<tEndEvent>
    {
        public EndEventHandler(ProcessInstance processInstance, FlowNode<tEndEvent> node)
            : base(processInstance, node)
        {
        }
    }
}
