using Polokus.Core.BpmnModels;
using Polokus.Core.Execution.NodeHandlers.Abstract;
using Polokus.Core.Interfaces.Xsd;

namespace Polokus.Core.Execution.NodeHandlers.Special
{
    public class SignalEmittingHandler<T> : NodeHandler<T> where T : tFlowNode
    {
        public SignalEmittingHandler(IProcessInstance processInstance, FlowNode<T> typedNode)
            : base(processInstance, typedNode)
        {
        }

        public override Task Action(INodeCaller? caller)
        {
            string signal = this.Node.Name; // TODO get signal from properties
            this.ProcessInstance.Workflow.SignalManager.EmitSignal(signal);
            return Task.CompletedTask;
        }
    }
}
