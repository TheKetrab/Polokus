using Polokus.Core.BpmnModels;
using Polokus.Core.Execution.NodeHandlers.Abstract;
using Polokus.Core.Interfaces.Xsd;

namespace Polokus.Core.Execution.NodeHandlers
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
