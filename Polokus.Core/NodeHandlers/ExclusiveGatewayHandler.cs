using Polokus.Core.Execution;
using Polokus.Core.Interfaces;
using Polokus.Core.Interfaces.BpmnModels;
using Polokus.Core.Interfaces.Xsd;
using Polokus.Core.Models;
using Polokus.Core.NodeHandlers.Abstract;

namespace Polokus.Core.NodeHandlers
{
    public class ExclusiveGatewayHandler : NodeHandler<tExclusiveGateway>
    {
        public ExclusiveGatewayHandler(ProcessInstance processInstance, FlowNode<tExclusiveGateway> typedNode)
            : base(processInstance, typedNode)
        {
            
        }

        async Task<bool> IsValidSequence(ISequence sequence)
        {
            string condition = ScriptProvider.Decode(sequence.Name);
            if (string.IsNullOrEmpty(condition))
            {
                return true; // accept by default
            }
            return await ScriptProvider.EvalCSharpScriptAsync<bool>(condition);
        }

        protected override async Task<ProcessResultInfo> Process(INodeCaller? caller)
        {
            foreach (var sequence in Node.Outgoing)
            {
                if (await IsValidSequence(sequence))
                {
                    return new SuccessProcessResultInfo(sequence);
                }
            }

            return new ProcessResultInfo(ProcessResultState.Failure);
        }

    }
}
