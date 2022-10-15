using Polokus.Core.Interfaces;
using Polokus.Core.Models;
using Polokus.Core.Models.BpmnObjects.Xsd;
using Polokus.Core.NodeHandlers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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

        protected override async Task<ProcessResultInfo> Process(IFlowNode? caller)
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
