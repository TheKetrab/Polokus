using Polokus.Core.Execution;
using Polokus.Core.Interfaces;
using Polokus.Core.Interfaces.Xsd;
using Polokus.Core.Execution.NodeHandlers.Abstract;
using Polokus.Core.BpmnModels;
using Polokus.Core.Interfaces.Execution;

namespace Polokus.Core.Execution.NodeHandlers
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
