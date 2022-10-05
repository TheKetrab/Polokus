using Polokus.Lib.Models;
using Polokus.Lib.Models.BpmnObjects.Xsd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.NodeHandlers
{
    public class ScriptTaskNodeHandler : NodeHandler<tScriptTask>
    {
        public ScriptTaskNodeHandler(FlowNode<tScriptTask> typedNode) : base(typedNode)
        {
        }

        protected override async Task Process(IFlowNode? caller)
        {
            string script = ScriptProvider.Decode(Node.Name);
            await ScriptProvider.EvalCSharpScriptAsync(script);
        }
    }
}
