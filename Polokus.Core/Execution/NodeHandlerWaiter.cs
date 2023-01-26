using Polokus.Core.Helpers;
using Polokus.Core.Interfaces;
using Polokus.Core.Interfaces.BpmnModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polokus.Core.Interfaces.Extensibility;
using Polokus.Core.Interfaces.Execution;

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
            Id = EncodingIds.GetWaiterId(
                ProcessInstance.Workflow.Id, ProcessInstance.Id,
                ProcessInstance.BpmnProcess.Id, NodeToCall.Id);

        }

    }
}
