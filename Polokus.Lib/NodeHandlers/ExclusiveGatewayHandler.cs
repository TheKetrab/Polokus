using Polokus.Lib.Models;
using Polokus.Lib.Models.BpmnObjects.Xsd;
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
            string condition = WebUtility.HtmlDecode(sequence.Name);

            var test = new ScriptProvider();
            test.globals.x = 2;

            var start = DateTime.Now;
            bool res = await test.EvalCSharpScriptAsync<bool>(condition);
            var end = DateTime.Now;
            Console.WriteLine($"Evaling condition: {condition} in {(start - end)}");

            return res;
        }

        public async override Task<ProcessResultInfo> Execute(IFlowNode? caller)
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
