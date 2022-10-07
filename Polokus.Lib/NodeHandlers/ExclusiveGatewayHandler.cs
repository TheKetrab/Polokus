using Polokus.Lib.Models;
using Polokus.Lib.Models.BpmnObjects.Xsd;
using Polokus.Lib.NodeHandlers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.NodeHandlers
{
    public class ExclusiveGatewayHandler : NodeHandler<tExclusiveGateway>
    {
        public ExclusiveGatewayHandler(FlowNode<tExclusiveGateway> node) : base(node)
        {
            
        }

        async Task<bool> IsValidSequence(Sequence sequence)
        {
            string condition = ScriptProvider.Decode(sequence.Name);
            if (string.IsNullOrEmpty(condition))
            {
                return true; // accept by default
            }
            return await ScriptProvider.EvalCSharpScriptAsync<bool>(condition);
        }

        protected override async Task<ProcessResultInfo> Process(IFlowNode? caller)
        {
            foreach (var sequence in Node.Outgoing)
            {
                if (await IsValidSequence(sequence))
                {
                    return new ProcessResultInfo(ProcessResultState.Success, sequence);
                }
            }

            return new ProcessResultInfo(ProcessResultState.Failure);
        }

    }
}
