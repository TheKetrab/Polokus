
using Polokus.Lib.Models.BpmnObjects.Xsd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Polokus.Lib.Models
{
    public class FlowNode<T> : IFlowNode where T : tFlowNode
    {
        public T XmlElement { get; set; }

        public string Name { get; set; }
        public string Id { get; set; }

        public ICollection<Sequence> Incoming { get; set; } = new List<Sequence>();
        public ICollection<Sequence> Outgoing { get; set; } = new List<Sequence>();
        public Type XmlType { get => typeof(T); }
        public BpmnProcess Process { get;set; }
 
        public FlowNode(BpmnProcess process, T xmlElement) 
        {
            XmlElement = xmlElement;
            Name = xmlElement.name;
            Id = xmlElement.id;
            Process = process;
        }


    }

    
}
