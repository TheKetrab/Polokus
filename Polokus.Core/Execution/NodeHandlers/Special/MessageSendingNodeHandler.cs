using Polokus.Core.BpmnModels;
using Polokus.Core.Execution.NodeHandlers.Abstract;
using Polokus.Core.Helpers;
using Polokus.Core.Interfaces.Xsd;

namespace Polokus.Core.Execution.NodeHandlers.Special
{
    public class MessageSendingNodeHandler<T> : NodeHandler<T> where T : tFlowNode
    {
        public MessageSendingNodeHandler(IProcessInstance processInstance, FlowNode<T> typedNode) : base(processInstance, typedNode)
        {
        }



        private async Task PingStarter(IMessageFlow outgoing)
        {
            var starterToPing = EncodingIds.GetStarterId(
                ProcessInstance.Workflow.Id,
                outgoing.TargetProcess.Id,
                outgoing.Target!.Id);

            List<string> queryArgs = new List<string>();
            queryArgs.Add($"parent={ProcessInstance.Id}");

            string? variablesString = VariablesEncoder.GetVariablesQueryString(
                ProcessInstance.ScriptProvider.Globals, outgoing.Name);
            if (variablesString != null)
            {
                queryArgs.Add(variablesString);
            }

            await ProcessInstance.Workflow.MessageManager
                .PingListener(starterToPing, string.Join('&', queryArgs));
        }

        private async Task PingWaiter(IMessageFlow outgoing)
        {
            // NOTE: there is existing process to call. here is very naive solution:
            // find process instance that contains given node

            IProcessInstance piToCall = await GetProcessInstanceToCall(outgoing);

            var waiterToPing = EncodingIds.GetWaiterId(
                ProcessInstance.Workflow.Id,
                piToCall.Id,
                outgoing.TargetProcess.Id,
                outgoing.Target!.Id);

            List<string> queryArgs = new List<string>();
            string? variablesString = VariablesEncoder.GetVariablesQueryString(
                ProcessInstance.ScriptProvider.Globals, outgoing.Name);

            if (variablesString != null)
            {
                queryArgs.Add(variablesString);
            }

            await ProcessInstance.Workflow.MessageManager
                .PingListener(waiterToPing, string.Join('&', queryArgs));
        }

        private async Task<IProcessInstance> GetProcessInstanceToCall(IMessageFlow outgoing)
        {
            do
            {
                var allWaiters = ProcessInstance.Workflow.MessageManager.GetWaiters();
                foreach (var waiter in allWaiters)
                {
                    if (EncodingIds.GetBpmnProcessIdFromWaiter(waiter.Id) == outgoing.TargetProcess.Id
                        && EncodingIds.GetNodeIdFromWaiter(waiter.Id) == outgoing.Target!.Id)
                    {
                        string pid = EncodingIds.GetProcessInstanceIdFromWaiter(waiter.Id);
                        IProcessInstance? p = ProcessInstance.Workflow.GetProcessInstanceById(pid);
                        if (p != null)
                        {
                            return p;
                        }
                    }
                }

                await Task.Delay(100);

            } while (true);
        }

        public async override Task Action(INodeCaller? caller)
        {
            var callerNode = (IMessageCallerNode)Node;
            foreach (var outgoing in callerNode.OutgoingMessages)
            {
                if (outgoing.Target == null)
                {
                    throw new Exception($"Target starter is null: {outgoing}");
                }

                if (outgoing.Target.IsStartNode())
                {
                    await PingStarter(outgoing);
                }
                else
                {
                    await PingWaiter(outgoing);
                }

            }
        }
    }
}
