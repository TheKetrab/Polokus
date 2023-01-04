using Polokus.Core.Interfaces;
using Polokus.Core.Models;
using Polokus.Core.Models.BpmnObjects.Xsd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.BpmnModels
{
    public class BoundaryEvent : FlowNode<tBoundaryEvent>, IBoundaryEvent
    {
        public IFlowNode? AttachedTo { get; set; }

        public BoundaryEventType Type { get; }

        public bool Interrupting { get; }

        public BoundaryEvent(BpmnProcess bpmnProcess, tBoundaryEvent xmlElement)
            : base(bpmnProcess, xmlElement)
        {
            Interrupting = XmlElement.cancelActivity;

            var type = XmlElement.Items[0].GetType();
            if (type == typeof(tErrorEventDefinition))
            {
                Type = BoundaryEventType.Error;
            }
            // TODO rest, or exception
        }
    }
}
