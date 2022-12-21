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
        public virtual void AfterExecuteNodeFailure(string wfId, string piId, IFlowNode node, int taskId) { }
        public virtual void AfterExecuteNodeSuccess(string wfId, string piId, IFlowNode node, int taskId) { }
        public virtual void AfterExecuteNodeSuspension(string wfId, string piId, IFlowNode node, int taskId) { }
        public virtual void BeforeExecuteNode(string wfId, string piId, IFlowNode node, int taskId, INodeCaller? caller) { }
        public virtual void BeforeStartNewSequence(string wfId, string piId, IFlowNode firstNode, INodeCaller? caller) { }
        public virtual void OnStatusChanged(string wfId, string piId) { }
        public virtual void OnTasksChanged(string wfId, string piId) { }
        public virtual void OnTimeout(string wfId, string piId) { }
        public virtual void OnProcessFinished(string wfId, string piId, string result) { }
    }
}
