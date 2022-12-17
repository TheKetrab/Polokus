using Polokus.Core.Execution;
using Polokus.Core.Interfaces;
using Polokus.Core.Models;
using Polokus.Core.Models.BpmnObjects.Xsd;
using Polokus.Core.NodeHandlers.Abstract;
using Polokus.Core.NodeHandlers.Special;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.NodeHandlers
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
