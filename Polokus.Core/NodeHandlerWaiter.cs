using Polokus.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core
{
    public class NodeHandlerWaiter : INodeHandlerWaiter
    {
        public IFlowNode NodeToCall { get; }
        public IProcessInstance ProcessInstance { get; }

        public string Id { get; }

        public void Invoke()
        {
            ProcessInstance.StartNewSequence(NodeToCall, this);

        }

        public NodeHandlerWaiter(IProcessInstance processInstance, IFlowNode nodeToCall, string id)
        {
            ProcessInstance = processInstance;
            NodeToCall = nodeToCall;
            Id = id;
        }

    }
}
