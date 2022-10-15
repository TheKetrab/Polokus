
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
    public class FlowNode<T> : IFlowNode where T : tFlowNode
    {
        public T XmlElement { get; set; }

        public string Name { get; set; }
        public string Id { get; set; }

        public ICollection<ISequence> Incoming { get; set; } = new List<ISequence>();
        public ICollection<ISequence> Outgoing { get; set; } = new List<ISequence>();
        public Type XmlType { get => typeof(T); }
        public IBpmnProcess BpmnProcess { get;set; }
 
        public FlowNode(BpmnProcess bpmnProcess, T xmlElement) 
        {
            XmlElement = xmlElement;
            Name = xmlElement.name;
            Id = xmlElement.id;
            BpmnProcess = bpmnProcess;
        }


    }

    
}
