using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.Nodes.Tasks
{
    public class ScriptTaskNode : TaskNode
    {
        // expression to be parsed via c#
        public string Expression { get; set; }
    }
}
