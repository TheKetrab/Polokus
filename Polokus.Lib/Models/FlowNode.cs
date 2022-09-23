
using Polokus.Lib.Models.BpmnObjects.Xsd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.Models
{
    public class FlowNode
    {
        public tFlowNode XmlElement { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }

        public ICollection<Sequence> Incoming { get; set; } = new List<Sequence>();
        public ICollection<Sequence> Outgoing { get; set; } = new List<Sequence>();
        public Type XmlType => XmlElement.GetType();


        BpmnProcess Process { get; }
        public FlowNode(BpmnProcess process, tFlowNode xmlElement)
        {
            XmlElement = xmlElement;
            Name = xmlElement.name;
            Id = xmlElement.id;
            Process = process;
        }
    }
}
