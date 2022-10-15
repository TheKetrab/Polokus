using Polokus.Core.Models;
using Polokus.Core.Models.BpmnObjects.Xsd;
using Polokus.Core.NodeHandlers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.NodeHandlers
{
    public class TaskNodeHandler : NodeHandler<tTask>
    {
        public TaskNodeHandler(ProcessInstance processInstance, FlowNode<tTask> typedNode)
            : base(processInstance, typedNode)
        {
        }
    }
}
