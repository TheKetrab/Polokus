using Polokus.Lib.Models;
using Polokus.Lib.Models.BpmnObjects.Xsd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.NodeHandlers
{
    public class TaskNodeHandler : NodeHandler<tTask>
    {
        public TaskNodeHandler(ProcessInstance process) : base(process)
        {
        }

    }
}
