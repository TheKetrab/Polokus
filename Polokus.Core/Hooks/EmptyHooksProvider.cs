using Polokus.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Hooks
{
    public class EmptyHooksProvider : IHooksProvider
    {
        public virtual void AfterExecuteNodeFailure(IFlowNode node, int taskId) { }
        public virtual void AfterExecuteNodeSuccess(IFlowNode node, int taskId) { }
        public virtual void AfterExecuteNodeSuspension(IFlowNode node, int taskId) { }
        public virtual void BeforeExecuteNode(IFlowNode node, int taskId, IFlowNode? caller) { }
        public virtual void BeforeStartNewSequence(IFlowNode firstNode, IFlowNode? caller) { }
        public virtual void OnTimeout() { }
    }
}
