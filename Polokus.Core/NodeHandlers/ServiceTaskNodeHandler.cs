using Polokus.Core.Execution;
using Polokus.Core.Interfaces;
using Polokus.Core.Interfaces.Xsd;
using Polokus.Core.Models;
using Polokus.Core.NodeHandlers.Abstract;

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
