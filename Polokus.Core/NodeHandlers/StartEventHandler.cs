using Polokus.Core.Execution;
using Polokus.Core.Interfaces.Xsd;
using Polokus.Core.Models;
using Polokus.Core.NodeHandlers.Abstract;

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
