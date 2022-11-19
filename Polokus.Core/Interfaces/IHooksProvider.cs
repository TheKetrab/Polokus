using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Interfaces
{
    public interface IHooksProvider
    {
        void AfterExecuteNodeSuccess(string processInstanceId, IFlowNode node, int taskId);
        void AfterExecuteNodeFailure(string processInstanceId, IFlowNode node, int taskId);
        void AfterExecuteNodeSuspension(string processInstanceId, IFlowNode node, int taskId);
        void BeforeStartNewSequence(string processInstanceId, IFlowNode firstNode, INodeCaller? caller);
        void BeforeExecuteNode(string processInstanceId, IFlowNode node, int taskId, INodeCaller? caller);
        void OnStatusChanged(string processInstanceId);
        void OnTasksChanged(string processInstanceId);
        void OnTimeout(string processInstanceId);
    }
}
