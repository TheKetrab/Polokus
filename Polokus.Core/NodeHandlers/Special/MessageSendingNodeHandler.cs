using Polokus.Core.Helpers;
using Polokus.Core.Interfaces;
using Polokus.Core.Models;
using Polokus.Core.Models.BpmnObjects.Xsd;
using Polokus.Core.NodeHandlers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.NodeHandlers.Special
{
    public class MessageSendingNodeHandler<T> : NodeHandler<T> where T : tFlowNode
    {
        public MessageSendingNodeHandler(IProcessInstance processInstance, FlowNode<T> typedNode) : base(processInstance, typedNode)
        {
        }

        private async Task PingStarter(IMessageFlow outgoing)
        {
            var starterToPing = Utils.GetStarterName(
                ProcessInstance.ContextInstance.Id,
                outgoing.TargetProcess.Id,
                outgoing.Target!.Id);

            await ProcessInstance.ContextInstance.MessageManager
                .PingListener(starterToPing, $"parent={ProcessInstance.Id}");
        }

        private async Task PingWaiter(IMessageFlow outgoing)
        {
            // NOTE: there is existing process to call. here is very naive solution:
            // find process instance that contains given node

            IProcessInstance piToCall = await GetProcessInstanceToCall(outgoing);

            var waiterToPing = Utils.GetWaiterName(
                ProcessInstance.ContextInstance.Id,
                piToCall.Id,
                outgoing.TargetProcess.Id,
                outgoing.Target!.Id);

            await ProcessInstance.ContextInstance.MessageManager
                .PingListener(waiterToPing);

        }

        private async Task<IProcessInstance> GetProcessInstanceToCall(IMessageFlow outgoing)
        {
            do
            {
                var allWaiters = ProcessInstance.ContextInstance.MessageManager.GetWaiters();
                foreach (var waiter in allWaiters)
                {
                    if (Utils.GetBpmnProcessIdFromWaiter(waiter.Id) == outgoing.TargetProcess.Id
                        && Utils.GetNodeIdFromWaiter(waiter.Id) == outgoing.Target!.Id)
                    {
                        string pid = Utils.GetProcessInstanceIdFromWaiter(waiter.Id);
                        IProcessInstance? p = ProcessInstance.ContextInstance.GetProcessInstanceById(pid);
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
