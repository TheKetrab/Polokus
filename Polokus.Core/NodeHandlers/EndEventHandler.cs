using Polokus.Core.Execution;
using Polokus.Core.Helpers;
using Polokus.Core.Interfaces;
using Polokus.Core.Interfaces.Xsd;
using Polokus.Core.Models;
using Polokus.Core.NodeHandlers.Abstract;
using Polokus.Core.NodeHandlers.Special;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.NodeHandlers
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
                _subhandler = new MessageSendingNodeHandler<tEndEvent>(ProcessInstance, TypedNode);
            }
            else if (eventDefinition is tSignalEventDefinition)
            {
                _subhandler = new SignalEmittingNodeHandler<tEndEvent>(ProcessInstance, TypedNode);
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
