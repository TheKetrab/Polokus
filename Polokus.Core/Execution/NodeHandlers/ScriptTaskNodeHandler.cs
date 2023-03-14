using Polokus.Core.BpmnModels;
using Polokus.Core.Execution.NodeHandlers.Abstract;
using Polokus.Core.Interfaces.Xsd;

namespace Polokus.Core.Execution.NodeHandlers
{
    public class ScriptTaskNodeHandler : NodeHandler<tScriptTask>
    {
        public ScriptTaskNodeHandler(ProcessInstance processInstance, FlowNode<tScriptTask> typedNode)
            : base(processInstance, typedNode)
        {
        }

        public override async Task Action(INodeCaller? caller)
        {
            string script = ScriptProvider.Decode(Node.Name);
            await ScriptProvider.EvalScriptAsync(script);
        }

    }
}
