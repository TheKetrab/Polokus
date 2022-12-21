using Polokus.Core.Helpers;
using Polokus.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Execution
{
    public class NodeHandlerWaiter : INodeHandlerWaiter
    {
        public IFlowNode NodeToCall { get; }
        public IProcessInstance ProcessInstance { get; }

        public string Id { get; }
        public IHooksProvider? HooksProvider => ProcessInstance.HooksProvider;

        public void Invoke()
        {
            ProcessInstance.StartNewSequence(NodeToCall, this);
        }

        public NodeHandlerWaiter(IProcessInstance processInstance, IFlowNode nodeToCall)
        {
            ProcessInstance = processInstance;
            NodeToCall = nodeToCall;
            Id = CallersIds.GetWaiterId(
                ProcessInstance.Workflow.Id, ProcessInstance.Id,
                ProcessInstance.BpmnProcess.Id, NodeToCall.Id);

        }

    }
}
