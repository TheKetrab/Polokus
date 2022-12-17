using Polokus.Core.Execution;
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
    public class ServiceTaskNodeHandler : NodeHandler<tServiceTask>
    {
        public ServiceTaskNodeHandler(ProcessInstance processInstance, FlowNode<tServiceTask> typedNode)
            : base(processInstance, typedNode)
        {
        }
    }
}
