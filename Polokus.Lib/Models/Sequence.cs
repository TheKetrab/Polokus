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

        private IFlowNode? _source;
        public IFlowNode? Source
        {
            get
            {
                return _source ??= Process.GetNodeById(XmlElement.sourceRef);
            }
        }

        private IFlowNode? _target;
        public IFlowNode? Target 
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
