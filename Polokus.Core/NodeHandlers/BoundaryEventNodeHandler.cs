using Polokus.Core.Interfaces;
using Polokus.Core.Interfaces.Xsd;
using Polokus.Core.Models;
using Polokus.Core.NodeHandlers.Abstract;

namespace Polokus.Core.NodeHandlers
{
    public class BoundaryEventNodeHandler : NodeHandler<tBoundaryEvent>
    {
        public BoundaryEventNodeHandler(
            IProcessInstance processInstance, FlowNode<tBoundaryEvent> typedNode)
            : base(processInstance, typedNode)
        {
        }
    }
}
