using Polokus.Core.BpmnModels;
using Polokus.Core.Execution.NodeHandlers.Special;
using Polokus.Core.Interfaces.Xsd;

namespace Polokus.Core.Execution.NodeHandlers
{
    public class ReceiveTaskHandler : MessageCatchingHandler<tReceiveTask>
    {
        public ReceiveTaskHandler(IProcessInstance processInstance, FlowNode<tReceiveTask> typedNode)
            : base(processInstance, typedNode)
        {
        }
    }
}
