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
