using Polokus.Lib.Models.BpmnObjects.Xsd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.Models
{
    public class Sequence
    {
        public tSequenceFlow XmlElement { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }

        private FlowNode? _source;
        public FlowNode? Source
        {
            get
            {
                return _source ??= Process.GetNodeById(XmlElement.sourceRef);
            }
        }

        private FlowNode? _target;
        public FlowNode? Target 
        { 
            get
            {
                return _target ??= Process.GetNodeById(XmlElement.targetRef);
            }
        }

        public BpmnProcess Process { get; }
        public Sequence(BpmnProcess process, tSequenceFlow xmlElement)
        {
            XmlElement = xmlElement;
            Name = xmlElement.name;
            Id = xmlElement.id;
            Process = process;
        }

    }
}
