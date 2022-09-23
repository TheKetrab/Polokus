using Polokus.Lib.Models;
using Polokus.Lib.Models.BpmnObjects.Xsd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.NodeHandlers
{
    public class ParallelGatewayNodeHandler : NodeHandler<tParallelGateway>
    {
        public ParallelGatewayNodeHandler(ProcessInstance process) : base(process)
        {

        }

        public bool IsJoinGateway => this.ExecutedOnNode.Incoming.Count > 1;
        public bool IsForkGateway => this.ExecutedOnNode.Outgoing.Count > 1;


        private List<string> invokedBy = new();
        public override Task<ProcessResultInfo> ProcessNode(FlowNode node, string? predecessorId)
        {
            if (IsJoinGateway)
            {
                Console.WriteLine("Invoked Parallel JOIN");

                if (predecessorId != null)
                {
                    invokedBy.Add(predecessorId);
                }

                bool canRunFurther = node.Incoming.All(x => invokedBy.Contains(x.Id));

                if (canRunFurther)
                {
                    return Task.FromResult(
                        new ProcessResultInfo(ProcessResultState.Success, node.Outgoing.ToList()));
                }
                else
                {
                    return Task.FromResult(new ProcessResultInfo(ProcessResultState.Suspension));
                }
            }
            else
            {
                return Task.FromResult(
                    new ProcessResultInfo(ProcessResultState.Success, node.Outgoing.ToList()));
            }


        }
    }
}
