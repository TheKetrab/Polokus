using Polokus.Core.Execution;
using Polokus.Core.Models;
using Polokus.Core.Models.BpmnObjects.Xsd;
using Polokus.Core.NodeHandlers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.NodeHandlers
{
    public class StartEventHandler : NodeHandler<tStartEvent>
    {
        public StartEventHandler(ProcessInstance processInstance, FlowNode<tStartEvent> typedNode)
            : base(processInstance, typedNode)
        {
        }
    }
}
