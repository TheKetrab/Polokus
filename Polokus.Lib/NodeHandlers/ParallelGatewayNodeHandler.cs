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
        public ParallelGatewayNodeHandler(FlowNode<tParallelGateway> node) : base(node)
        {

        }

        public bool IsJoinGateway => Node.Incoming.Count > 1;
        public bool IsForkGateway => Node.Outgoing.Count > 1;


        private List<IFlowNode> invokedBy = new();
        public override Task<ProcessResultInfo> Execute(IFlowNode? caller)
        {
            if (IsJoinGateway)
            {
                Console.WriteLine("Invoked Parallel JOIN");

                if (caller != null)
                {
                    invokedBy.Add(caller);
                }

                bool canRunFurther = Node.Incoming.All(x => invokedBy.Contains(x.Source));

                if (canRunFurther)
                {
                    return Task.FromResult(
                        new ProcessResultInfo(ProcessResultState.Success, Node.Outgoing.ToList()));
                }
                else
                {
                    return Task.FromResult(new ProcessResultInfo(ProcessResultState.Suspension));
                }
            }
            else
            {
                return Task.FromResult(
                    new ProcessResultInfo(ProcessResultState.Success, Node.Outgoing.ToList()));
            }


        }
    }
}
