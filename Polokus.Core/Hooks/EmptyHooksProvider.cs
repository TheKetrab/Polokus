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
        public virtual void AfterExecuteNodeFailure(string processInstanceId, IFlowNode node, int taskId) { }
        public virtual void AfterExecuteNodeSuccess(string processInstanceId, IFlowNode node, int taskId) { }
        public virtual void AfterExecuteNodeSuspension(string processInstanceId, IFlowNode node, int taskId) { }
        public virtual void BeforeExecuteNode(string processInstanceId, IFlowNode node, int taskId, INodeCaller? caller) { }
        public virtual void BeforeStartNewSequence(string processInstanceId, IFlowNode firstNode, INodeCaller? caller) { }
        public virtual void OnStatusChanged(string processInstanceId) { }
        public virtual void OnTasksChanged(string processInstanceId) { }
        public virtual void OnTimeout(string processInstanceId) { }
    }
}
