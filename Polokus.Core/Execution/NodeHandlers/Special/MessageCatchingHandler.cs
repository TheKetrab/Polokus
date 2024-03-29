﻿using Polokus.Core.BpmnModels;
using Polokus.Core.Execution.NodeHandlers.Abstract;
using Polokus.Core.Interfaces.Xsd;

namespace Polokus.Core.Execution.NodeHandlers.Special
{
    public class MessageCatchingHandler<T> : NodeHandler<T> where T : tFlowNode
    {
        public MessageCatchingHandler(IProcessInstance processInstance, FlowNode<T> typedNode)
            : base(processInstance, typedNode)
        {
        }

        public override Task<bool> CanProcess(INodeCaller? caller)
        {
            if (caller is NodeHandlerWaiter w)
            {
                return Task.FromResult(true);
            }

            ProcessInstance.Workflow.MessageManager.RegisterWaiter(
                this.ProcessInstance, this.Node, true);

            return Task.FromResult(false);
        }
    }
}
