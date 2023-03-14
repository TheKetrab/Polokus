using Polokus.Core.BpmnModels;
using Polokus.Core.Execution.NodeHandlers.Abstract;
using Polokus.Core.Interfaces.Xsd;

namespace Polokus.Core.Execution.NodeHandlers
{
    public class ParallelGatewayHandler : JoiningHandler<tParallelGateway>
    {
        public ParallelGatewayHandler(ProcessInstance processInstance, FlowNode<tParallelGateway> typedNode)
            : base(processInstance, typedNode)
        {
        }
    }
}
