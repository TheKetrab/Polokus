using Polokus.Lib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Polokus.Lib.BpmnObjects
{
    public class SequenceFlow : BpmnObject
    {
        public SequenceFlow(XElement element) : base(element)
        {
            // todo source ref, target ref?
        }

        public string SourceRef { get; set; } = "";
        public string TargetRef { get; set; } = "";

    }
}
