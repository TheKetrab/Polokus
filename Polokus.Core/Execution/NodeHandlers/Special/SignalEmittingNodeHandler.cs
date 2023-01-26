using Polokus.Core.Interfaces;
using Polokus.Core.Interfaces.Xsd;
using Polokus.Core.BpmnModels;
using Polokus.Core.Execution.NodeHandlers.Abstract;
using Polokus.Core.Interfaces.Execution;

namespace Polokus.Core.Execution.NodeHandlers.Special
{
    public class SignalEmittingNodeHandler<T> : NodeHandler<T> where T : tFlowNode
    {
        public SignalEmittingNodeHandler(IProcessInstance processInstance, FlowNode<T> typedNode)
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
