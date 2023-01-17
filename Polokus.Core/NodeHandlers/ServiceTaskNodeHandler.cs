using Polokus.Core.Execution;
using Polokus.Core.Interfaces.NodeHandlers;
using Polokus.Core.Interfaces.Xsd;
using Polokus.Core.Models;
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
        public virtual string TaskName => string.Empty;

        public ServiceTaskNodeHandler(ProcessInstance processInstance, FlowNode<tServiceTask> typedNode)
            : base(processInstance, typedNode)
        {
        }
    }
}
