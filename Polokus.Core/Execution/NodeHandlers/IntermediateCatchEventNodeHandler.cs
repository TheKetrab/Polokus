using Polokus.Core.BpmnModels;
using Polokus.Core.Execution.NodeHandlers.Abstract;
using Polokus.Core.Execution.NodeHandlers.Special;
using Polokus.Core.Interfaces.Xsd;

namespace Polokus.Core.Execution.NodeHandlers
{
    public class IntermediateCatchEventNodeHandler : NodeHandler<tIntermediateCatchEvent>
    {
        private NodeHandler<tIntermediateCatchEvent> _subhandler;

        public IntermediateCatchEventNodeHandler(
            ProcessInstance processInstance, FlowNode<tIntermediateCatchEvent> typedNode)
            : base(processInstance, typedNode)
        {
            if (this.TypedNode.XmlElement.Items.Length == 0)
            {
                throw new Exception($"Unknown definition of node {this.Node.Name}");
            }

            var eventDefinition = TypedNode.XmlElement.Items[0];
            if (eventDefinition is tTimerEventDefinition)
            {
                string timeDefinition = Node.Name;
                _subhandler = new TimerEventNodeHandler(ProcessInstance, TypedNode, timeDefinition);
            }
            else if (eventDefinition is tMessageEventDefinition)
            {
                _subhandler = new MessageCatchingNodeHandler<tIntermediateCatchEvent>(ProcessInstance, TypedNode);
            }
            else if (eventDefinition is tSignalEventDefinition)
            {
                _subhandler = new SignalSlottingNodeHandler<tIntermediateCatchEvent>(ProcessInstance, TypedNode);
            }
            else
            {
                throw new Exception($"Unknown definition of node {this.Node.Name}: {eventDefinition.id}");
            }
        }

        public override Task<bool> CanProcess(INodeCaller? caller)
        {
            return _subhandler.CanProcess(caller);
        }
    }
}
