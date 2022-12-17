using Polokus.Core.Helpers;
using Polokus.Core.Interfaces;
using Polokus.Core.Models.BpmnObjects.Xsd;
using Polokus.Core.Models;
using Polokus.Core.NodeHandlers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polokus.Core.Execution;

namespace Polokus.Core.NodeHandlers.Special
{
    public class MessageCatchingNodeHandler<T> : NodeHandler<T> where T : tFlowNode
    {
        public MessageCatchingNodeHandler(IProcessInstance processInstance, FlowNode<T> typedNode)
            : base(processInstance, typedNode)
        {
        }

        public override Task<bool> CanProcess(INodeCaller? caller)
        {
            if (caller is NodeHandlerWaiter w)
            {
                ProcessInstance.Waiters.Remove(w);
                return Task.FromResult(true);
            }

            var waiter = new NodeHandlerWaiter(ProcessInstance, Node);
            ProcessInstance.Waiters.Add(waiter);
            ProcessInstance.Workflow.MessageManager
                .RegisterMessageListener(waiter);

            return Task.FromResult(false);
        }
    }
}
