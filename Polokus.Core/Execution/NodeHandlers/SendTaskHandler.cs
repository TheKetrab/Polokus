using Polokus.Core.BpmnModels;
using Polokus.Core.Execution.NodeHandlers.Special;
using Polokus.Core.Interfaces.Xsd;

namespace Polokus.Core.Execution.NodeHandlers
{
    public class SendTaskHandler : MessageSendingHandler<tSendTask>
    {
        public SendTaskHandler(IProcessInstance processInstance, FlowNode<tSendTask> typedNode)
            : base(processInstance, typedNode)
        {
        }
    }
}
