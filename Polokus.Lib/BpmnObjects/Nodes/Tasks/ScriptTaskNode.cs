using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Polokus.Lib.BpmnObjects.Nodes.Tasks
{
    public class ScriptTaskNode : TaskNode
    {
        public ScriptTaskNode(XElement element) : base(element)
        {
        }

        // expression to be parsed via c#
        public string Expression { get; set; }
    }
}
