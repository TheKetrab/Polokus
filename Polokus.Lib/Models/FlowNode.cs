
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

        public ICollection<FlowNode> Incoming { get; set; } = new List<FlowNode>();
        public ICollection<FlowNode> Outgoing { get; set; } = new List<FlowNode>();
        public Type XmlType => XmlElement.GetType();

        public FlowNode(tFlowNode xmlElement)
        {
            XmlElement = xmlElement;
            Name = xmlElement.name;
            Id = xmlElement.id;
        }
    }
}
