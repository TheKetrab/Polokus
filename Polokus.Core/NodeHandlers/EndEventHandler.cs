using Polokus.Core.Execution;
using Polokus.Core.Helpers;
using Polokus.Core.Interfaces;
using Polokus.Core.Models;
using Polokus.Core.Models.BpmnObjects.Xsd;
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
            else if (eventDefinition is tErrorEventDefinition)
            {
                ErrorEndEvent = true;
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

            if (_subhandler == null)
            {
                return;
            }
            await _subhandler.Action(caller);
        }

    }
}
