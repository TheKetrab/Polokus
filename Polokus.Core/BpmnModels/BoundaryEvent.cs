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
            Type = DetermineType(XmlElement.Items[0].GetType());
        }

        private BoundaryEventType DetermineType(Type type)
        {
            if (type == typeof(tErrorEventDefinition))
            {
                return BoundaryEventType.Error;
            }
            else if (type == typeof(tTimerEventDefinition))
            {
                return BoundaryEventType.Timer;
            }
            else if (type == typeof(tMessageEventDefinition))
            {
                return BoundaryEventType.Message;
            }
            else if (type == typeof(tSignalEventDefinition))
            {
                return BoundaryEventType.Signal;
            }
            else
            {
                return BoundaryEventType.Undefined;
            }

        }
    }
}
