using Polokus.Core.Interfaces;
using Polokus.Core.Models;
using Polokus.Core.Models.BpmnObjects.Xsd;
using Polokus.Core.NodeHandlers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.NodeHandlers
{
    internal class IntermediateCatchEventNodeHandler : NodeHandler<tIntermediateCatchEvent>
    {
        public IntermediateCatchEventNodeHandler(ProcessInstance processInstance, FlowNode<tIntermediateCatchEvent> typedNode)
            : base(processInstance, typedNode)
        {
        }


        NodeHandler<tIntermediateCatchEvent> subhandler;
        public override Task<bool> CanProcess(INodeCaller? caller)
        {
            if (subhandler == null)
            {
                if (this.TypedNode.XmlElement.Items.Length > 0)
                {
                    var eventDefinition = TypedNode.XmlElement.Items[0];
                    if (eventDefinition is tTimerEventDefinition)
                    {
                        string timeDefinition = Node.Name;
                        subhandler = new TimerEventNodeHandler(ProcessInstance, TypedNode, timeDefinition);
                    }

                }
                else
                {
                    return base.CanProcess(caller);
                }
            }

            return subhandler.CanProcess(caller);
        }
    }
}
