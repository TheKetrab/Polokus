using Polokus.Core.Execution;
using Polokus.Core.Interfaces;
using Polokus.Core.Interfaces.Xsd;
using Polokus.Core.Models;
using Polokus.Core.NodeHandlers.Abstract;

namespace Polokus.Core.NodeHandlers
{
    public class ManualTaskNodeHandler : NodeHandler<tManualTask>
    {
        public ManualTaskNodeHandler(ProcessInstance processInstance, FlowNode<tManualTask> typedNode)
            : base(processInstance, typedNode)
        {
        }

        public override async Task Action(INodeCaller? caller)
        {
            if (Master.ClientConnected)
            {
                string token = AwaitingTokensHelper.CreateAwaitingToken(
                    Workflow.Id, ProcessInstance.Id, Node.Id);

                ProcessInstance.AwaitingTokens.Add(token);
                Master.HooksManager.OnAwaitingTokenCreated(Workflow.Id, ProcessInstance.Id, Node.Id, token);
                while (Master.ClientConnected && ProcessInstance.AwaitingTokens.Contains(token))
                {
                    await Task.Delay(100);
                }
            }
        }
    }
}
