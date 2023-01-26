using Polokus.Core.Interfaces;
using Polokus.Core.Interfaces.Xsd;
using Polokus.Core.Execution.NodeHandlers.Abstract;
using Polokus.Core.BpmnModels;
using Polokus.Core.Interfaces.Execution;

namespace Polokus.Core.Execution.NodeHandlers
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
