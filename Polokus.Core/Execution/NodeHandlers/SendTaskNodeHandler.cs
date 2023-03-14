using Polokus.Core.BpmnModels;
using Polokus.Core.Execution.NodeHandlers.Special;
using Polokus.Core.Interfaces.Xsd;

namespace Polokus.Core.Execution.NodeHandlers
{
    public class SendTaskNodeHandler : MessageSendingNodeHandler<tSendTask>
    {
        public SendTaskNodeHandler(IProcessInstance processInstance, FlowNode<tSendTask> typedNode)
            : base(processInstance, typedNode)
        {
        }
    }
}
