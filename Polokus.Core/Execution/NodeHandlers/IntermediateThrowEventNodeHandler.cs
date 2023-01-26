using Polokus.Core.Interfaces;
using Polokus.Core.Interfaces.Xsd;
using Polokus.Core.BpmnModels;
using Polokus.Core.Execution.NodeHandlers.Abstract;
using Polokus.Core.Execution.NodeHandlers.Special;
using Polokus.Core.Interfaces.Execution;

namespace Polokus.Core.Execution.NodeHandlers
{
    public class IntermediateThrowEventNodeHandler : NodeHandler<tIntermediateThrowEvent>
    {
        private NodeHandler<tIntermediateThrowEvent> _subhandler;

        public IntermediateThrowEventNodeHandler(
            IProcessInstance processInstance, FlowNode<tIntermediateThrowEvent> typedNode)
            : base(processInstance, typedNode)
        {
            if (this.TypedNode.XmlElement.Items.Length == 0)
            {
                throw new Exception($"Unknown definition of node {this.Node.Name}");
            }

            var eventDefinition = TypedNode.XmlElement.Items[0];
            if (eventDefinition is tMessageEventDefinition)
            {
                _subhandler = new MessageSendingNodeHandler<tIntermediateThrowEvent>(ProcessInstance, TypedNode);
            }
            else if (eventDefinition is tSignalEventDefinition)
            {
                _subhandler = new SignalEmittingNodeHandler<tIntermediateThrowEvent>(ProcessInstance, TypedNode);
            }
            else
            {
                throw new Exception($"Unknown definition of node {this.Node.Name}: {eventDefinition.id}");
            }


        }

        public override async Task Action(INodeCaller? caller)
        {
            await _subhandler.Action(caller);
        }

        

    }
}
