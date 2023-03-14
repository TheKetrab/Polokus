using Polokus.Core.BpmnModels;
using Polokus.Core.Execution.NodeHandlers.Abstract;
using Polokus.Core.Interfaces.Xsd;

namespace Polokus.Core.Execution.NodeHandlers
{
    public class BoundaryEventHandler : NodeHandler<tBoundaryEvent>
    {
        public BoundaryEventHandler(
            IProcessInstance processInstance, FlowNode<tBoundaryEvent> typedNode)
            : base(processInstance, typedNode)
        {
        }
    }
}
