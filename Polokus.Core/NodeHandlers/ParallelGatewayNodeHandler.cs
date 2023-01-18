using Polokus.Core.Execution;
using Polokus.Core.Interfaces.Xsd;
using Polokus.Core.Models;
using Polokus.Core.NodeHandlers.Abstract;

namespace Polokus.Core.NodeHandlers
{
    public class ParallelGatewayNodeHandler : JoiningNodeHandler<tParallelGateway>
    {
        public ParallelGatewayNodeHandler(ProcessInstance processInstance, FlowNode<tParallelGateway> typedNode)
            : base(processInstance, typedNode)
        {
        }
    }
}
