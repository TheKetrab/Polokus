using Polokus.Core.Interfaces;
using Polokus.Core.Interfaces.Xsd;
using Polokus.Core.Models;
using Polokus.Core.NodeHandlers.Special;

namespace Polokus.Core.NodeHandlers
{
    public class SendTaskNodeHandler : MessageSendingNodeHandler<tSendTask>
    {
        public SendTaskNodeHandler(IProcessInstance processInstance, FlowNode<tSendTask> typedNode)
            : base(processInstance, typedNode)
        {
        }
    }
}
