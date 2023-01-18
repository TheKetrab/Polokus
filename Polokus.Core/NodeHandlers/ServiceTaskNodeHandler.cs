using Polokus.Core.Execution;
using Polokus.Core.Factories;
using Polokus.Core.Interfaces;
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
        public ServiceTaskNodeHandler(ProcessInstance processInstance, FlowNode<tServiceTask> typedNode)
            : base(processInstance, typedNode)
        {
        }

        public override async Task Action(INodeCaller? caller)
        {
            var impl = Workflow.NodeHandlerFactory.CreateServiceTaskNodeHandlerImpl(this, Node.Name);
            await impl.Run();
        }
    }
}
