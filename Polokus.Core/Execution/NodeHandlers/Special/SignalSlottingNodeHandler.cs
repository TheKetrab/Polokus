using Polokus.Core.BpmnModels;
using Polokus.Core.Execution.NodeHandlers.Abstract;
using Polokus.Core.Interfaces.Xsd;

namespace Polokus.Core.Execution.NodeHandlers.Special
{
    internal class SignalSlottingNodeHandler<T> : NodeHandler<T> where T : tFlowNode
    {
        public SignalSlottingNodeHandler(IProcessInstance processInstance, FlowNode<T> typedNode)
            : base(processInstance, typedNode)
        {
        }

        public override Task<bool> CanProcess(INodeCaller? caller)
        {
            if (caller is NodeHandlerWaiter w)
            {
                return Task.FromResult(true);
            }

            ProcessInstance.Workflow.SignalManager.RegisterWaiter(
                ProcessInstance, Node, true);

            return Task.FromResult(false);
        }
    }
}
