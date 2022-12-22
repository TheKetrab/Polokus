using Polokus.Core.Execution;
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
    internal class SignalSlottingNodeHandler<T> : NodeHandler<T> where T : tFlowNode
    {
        public SignalSlottingNodeHandler(IProcessInstance processInstance, FlowNode<T> typedNode)
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
            ProcessInstance.Workflow.SignalManager
                .RegisterSignalListener(waiter);

            return Task.FromResult(false);
        }
    }
}
