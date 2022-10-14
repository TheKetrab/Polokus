using Polokus.Lib.Models;
using Polokus.Lib.Models.BpmnObjects.Xsd;
using Polokus.Lib.NodeHandlers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.NodeHandlers
{
    internal class IntermediateCatchEventNodeHandler : NodeHandler<tIntermediateCatchEvent>
    {
        public IntermediateCatchEventNodeHandler(FlowNode<tIntermediateCatchEvent> typedNode) : base(typedNode)
        {
        }


        INodeHandler subhandler;
        public override Task<ProcessResultInfo> Execute(IFlowNode? caller, int taskId)
        {
            if (subhandler == null)
            {

                if (this.TypedNode.XmlElement.Items.Length > 0)
                {
                    var eventDefinition = TypedNode.XmlElement.Items[0];
                    if (eventDefinition is tTimerEventDefinition)
                    {
                        string timeDefinition = Node.Name;
                        subhandler = new TimerEventNodeHandler(TypedNode, timeDefinition) { ProcessInstance = this.ProcessInstance };
                    }

                }
                else
                {
                    return base.Execute(caller, taskId);
                }
            }

            return subhandler.Execute(caller, taskId);
        }

        protected override Task<bool> CanProcess(IFlowNode? caller)
        {
            var xx = this.TypedNode.XmlElement.Items;
            return base.CanProcess(caller);
        }
    }
}
