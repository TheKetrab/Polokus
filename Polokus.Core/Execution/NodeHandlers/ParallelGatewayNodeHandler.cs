using Polokus.Core.Execution;
using Polokus.Core.Interfaces.Xsd;
using Polokus.Core.BpmnModels;
using Polokus.Core.Execution.NodeHandlers.Abstract;

namespace Polokus.Core.Execution.NodeHandlers
{
    public class ParallelGatewayNodeHandler : JoiningNodeHandler<tParallelGateway>
    {
        public ParallelGatewayNodeHandler(ProcessInstance processInstance, FlowNode<tParallelGateway> typedNode)
            : base(processInstance, typedNode)
        {
        }
    }
}
