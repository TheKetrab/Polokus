
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Polokus.Core.Interfaces;
using Polokus.Core.Models.BpmnObjects.Xsd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Polokus.Core.Models
{
    public class FlowNode<T> : IFlowNode, IMessageFlowNode where T : tFlowNode
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public T XmlElement { get; set; }
        public Type XmlType { get => typeof(T); }
        public IBpmnProcess BpmnProcess { get; set; }
        public WaiterType RequireWaiter { get; private set; }
        public ICollection<ISequence> Incoming { get; set; } = new List<ISequence>();
        public ICollection<ISequence> Outgoing { get; set; } = new List<ISequence>();
        public ICollection<IMessageFlow> OutgoingMessages { get; set; } = new List<IMessageFlow>();
        public ICollection<IMessageFlow> IncommingMessages { get; set; } = new List<IMessageFlow>();
        public ICollection<IBoundaryEvent> BoundaryEvents { get; set; } = new List<IBoundaryEvent>();

        public FlowNode(BpmnProcess bpmnProcess, T xmlElement) 
        {
            XmlElement = xmlElement;
            Name = xmlElement.name ?? "";
            Id = xmlElement.id;
            BpmnProcess = bpmnProcess;

            RequireWaiter = WaiterType.None;
            SetDetailedWaiterType();
        }

        protected void SetDetailedWaiterType()
        {
            if (typeof(T) == typeof(tIntermediateCatchEvent))
            {
                var xmlElement = XmlElement as tIntermediateCatchEvent;
                if (xmlElement == null)
                {
                    return;
                }

                Type type = xmlElement.Items[0].GetType();
                if (type == typeof(tTimerEventDefinition))
                {
                    RequireWaiter = WaiterType.Timer;
                }
                else if (type == typeof(tMessageEventDefinition))
                {
                    RequireWaiter = WaiterType.Message;
                }
                else if (type == typeof(tSignalEventDefinition))
                {
                    RequireWaiter = WaiterType.Signal;
                }

            }
            else if (typeof(T) == typeof(tReceiveTask))
            {
                RequireWaiter = WaiterType.Message;                
            }
        }

    }
    
}
