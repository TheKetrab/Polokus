using Polokus.Core.Execution;
using Polokus.Core.Interfaces;
using Polokus.Core.Interfaces.Xsd;
using Polokus.Core.Models;
using Polokus.Core.NodeHandlers.Abstract;

namespace Polokus.Core.NodeHandlers.Special
{
    public class MessageCatchingNodeHandler<T> : NodeHandler<T> where T : tFlowNode
    {
        public MessageCatchingNodeHandler(IProcessInstance processInstance, FlowNode<T> typedNode)
            : base(processInstance, typedNode)
        {
        }

        public override Task<bool> CanProcess(INodeCaller? caller)
        {
            if (caller is NodeHandlerWaiter w)
            {
                return Task.FromResult(true);
            }

            ProcessInstance.Workflow.MessageManager.RegisterWaiter(
                this.ProcessInstance, this.Node, true);
            
            return Task.FromResult(false);
        }
    }
}
