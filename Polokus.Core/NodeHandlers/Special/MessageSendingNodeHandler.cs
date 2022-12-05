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

        public async override Task Action(INodeCaller? caller)
        {
            var callerNode = (IMessageCallerNode)Node;
            foreach (var outgoing in callerNode.OutgoingMessages)
            {
                string listenerToPing = string.Empty;

                if (outgoing.Target.IsStartNode())
                {
                    var starterToPing = Utils.GetStarterName(
                        ProcessInstance.ContextInstance.Id,
                        outgoing.TargetProcess.Id,
                        outgoing.Target.Id);

                    listenerToPing = starterToPing;

                    await ProcessInstance.ContextInstance.MessageManager.PingListener(listenerToPing, $"parent={ProcessInstance.Id}");
                }
                else
                {
                    // NOTE: there is existing process to call. here is very naive solution:
                    // find process instance that contains given node

                    IProcessInstance? piToCall = null;

                    var allWaiters = ProcessInstance.ContextInstance.MessageManager.GetWaiters();
                    foreach (var waiter in allWaiters)
                    {
                        if (Utils.GetBpmnProcessIdFromWaiter(waiter.Id) == outgoing.TargetProcess.Id
                            && Utils.GetNodeIdFromWaiter(waiter.Id) == outgoing.Target.Id)
                        {
                            string pid = Utils.GetProcessInstanceIdFromWaiter(waiter.Id);
                            IProcessInstance p = ProcessInstance.ContextInstance.GetProcessInstanceById(pid);
                            piToCall = p;
                            break;
                        }

                    }

                    if (piToCall == null)
                    {
                        Logger.Global.Log("Nobody to call.");
                        return;
                    }

                    var waiterToPing = Utils.GetWaiterName(
                        ProcessInstance.ContextInstance.Id,
                        piToCall.Id,
                        outgoing.TargetProcess.Id,
                        outgoing.Target.Id);

                    listenerToPing = waiterToPing;

                    await ProcessInstance.ContextInstance.MessageManager.PingListener(listenerToPing);
                }

            }
        }
    }
}
