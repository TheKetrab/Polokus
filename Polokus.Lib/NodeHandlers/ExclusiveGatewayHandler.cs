using Polokus.Lib.Models;
using Polokus.Lib.Models.BpmnObjects.Xsd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.NodeHandlers
{
    public class ExclusiveGatewayHandler : NodeHandler<tExclusiveGateway>
    {
        public ExclusiveGatewayHandler(ProcessInstance process) : base(process)
        {
            
        }



        bool IsValidSequence(Sequence sequence)
        {
            if (sequence.Name == "valid")
                return true;

            return false;
        }

        public override Task<ProcessResultInfo> ProcessNode(FlowNode node, string? predecessorId)
        {
            foreach (var sequence in node.Outgoing)
            {
                if (IsValidSequence(sequence))
                {
                    return Task.FromResult(
                        new ProcessResultInfo(ProcessResultState.Success, sequence));
                }
            }

            return Task.FromResult(new ProcessResultInfo(ProcessResultState.Failure));
        }


    }
}
