using Polokus.Core.Interfaces;
using Polokus.Core.Interfaces.Xsd;
using Polokus.Core.Models;
using Polokus.Core.NodeHandlers.Special;

namespace Polokus.Core.NodeHandlers
{
    public class ReceiveTaskNodeHandler : MessageCatchingNodeHandler<tReceiveTask>
    {
        public ReceiveTaskNodeHandler(IProcessInstance processInstance, FlowNode<tReceiveTask> typedNode)
            : base(processInstance, typedNode)
        {
        }
    }
}
