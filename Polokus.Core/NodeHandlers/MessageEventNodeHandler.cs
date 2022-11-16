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

namespace Polokus.Core.NodeHandlers
{
    public class MessageEventNodeHandler : NodeHandler<tIntermediateCatchEvent>
    {
        public MessageEventNodeHandler(IProcessInstance processInstance, FlowNode<tIntermediateCatchEvent> typedNode)
            : base(processInstance, typedNode)
        {

        }

        public override async Task<bool> CanProcess(INodeCaller? caller)
        {
            if (caller is NodeHandlerWaiter w)
            {
                ProcessInstance.Waiters.Remove(w);
                return true;
            }

            var waiter = new NodeHandlerWaiter(ProcessInstance, this.Node);
            ProcessInstance.Waiters.Add(waiter);
            ProcessInstance.ContextInstance.MessageManager
                .RegisterMessageListener(waiter);

            return false;
        }
    }
}
