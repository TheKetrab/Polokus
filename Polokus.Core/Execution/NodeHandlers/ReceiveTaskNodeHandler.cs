using Polokus.Core.Interfaces;
using Polokus.Core.Interfaces.Xsd;
using Polokus.Core.BpmnModels;
using Polokus.Core.Execution.NodeHandlers.Special;
using Polokus.Core.Interfaces.Execution;

namespace Polokus.Core.Execution.NodeHandlers
{
    public class ReceiveTaskNodeHandler : MessageCatchingNodeHandler<tReceiveTask>
    {
        public ReceiveTaskNodeHandler(IProcessInstance processInstance, FlowNode<tReceiveTask> typedNode)
            : base(processInstance, typedNode)
        {
        }
    }
}
