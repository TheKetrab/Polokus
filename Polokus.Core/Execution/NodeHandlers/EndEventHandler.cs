using Polokus.Core.BpmnModels;
using Polokus.Core.Execution.NodeHandlers.Abstract;
using Polokus.Core.Execution.NodeHandlers.Special;
using Polokus.Core.Interfaces.Xsd;

namespace Polokus.Core.Execution.NodeHandlers
{
    public class EndEventHandler : NodeHandler<tEndEvent>
    {
        private NodeHandler<tEndEvent>? _subhandler = null;
        public bool ErrorEndEvent { get; } = false;
        public bool TerminateEndEvent { get; } = false;

        public EndEventHandler(ProcessInstance processInstance, FlowNode<tEndEvent> node)
            : base(processInstance, node)
        {
            if (this.TypedNode.XmlElement.Items == null)
            {
                return;
            }
            if (this.TypedNode.XmlElement.Items.Length == 0)
            {
                return;
            }

            var eventDefinition = TypedNode.XmlElement.Items[0];
            if (eventDefinition is tMessageEventDefinition)
            {
                _subhandler = new MessageSendingHandler<tEndEvent>(ProcessInstance, TypedNode);
            }
            else if (eventDefinition is tSignalEventDefinition)
            {
                _subhandler = new SignalEmittingHandler<tEndEvent>(ProcessInstance, TypedNode);
            }
            else if (eventDefinition is tErrorEventDefinition)
            {
                ErrorEndEvent = true;
            }
            else if (eventDefinition is tTerminateEventDefinition)
            {
                TerminateEndEvent = true;
            }
            else
            {
                throw new Exception($"Unknown definition of node {this.Node.Name}: {eventDefinition.id}");
            }

        }

        public override async Task Action(INodeCaller? caller)
        {
            if (ErrorEndEvent)
            {
                throw new Exception();
            }

            else if (TerminateEndEvent)
            {
                ProcessInstance.StatusManager.KillEverythingRunning();
                return;
            }

            else if (_subhandler == null)
            {
                return;
            }

            else
            {
                await _subhandler.Action(caller);
            }
        }

    }
}
