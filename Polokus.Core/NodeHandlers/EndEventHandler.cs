﻿using Polokus.Core.Helpers;
using Polokus.Core.Interfaces;
using Polokus.Core.Models;
using Polokus.Core.Models.BpmnObjects.Xsd;
using Polokus.Core.NodeHandlers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.NodeHandlers
{
    public class EndEventHandler : NodeHandler<tEndEvent>
    {
        public EndEventHandler(ProcessInstance processInstance, FlowNode<tEndEvent> node)
            : base(processInstance, node)
        {
        }

        protected override Task Action(INodeCaller? caller)
        {
            var endFlowNode = (FlowNode<tEndEvent>)Node;
            if (endFlowNode.XmlElement.Items?.Any() ?? false)
            {
                var eventDefinition = endFlowNode.XmlElement.Items[0];
                if (eventDefinition is tMessageEventDefinition)
                {
                    SEND();
                    //var processStarter = new ProcessStarter(contextInstance, startFlowNode.BpmnProcess, startNode);
                    //contextInstance.MessageManager.RegisterMessageListener(processStarter);
                }

            }
            return base.Action(caller);
        }

        /// <summary>
        /// TODO: usunac to, zjednolicic z tym co sie dzieje w intermediate throw event
        /// </summary>
        private async Task SEND()
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
