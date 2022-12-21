using Polokus.Core.Hooks;

namespace Polokus.Core.Interfaces
{
    /// <summary>
    /// HooksProvider is an object that allows to inject hooks for some actions during processing a BPMN process.
    /// </summary>
    public interface IHooksProvider
    {
        void AfterExecuteNodeSuccess(string wfId, string piId, IFlowNode node, int taskId);
        void AfterExecuteNodeFailure(string wfId, string piId, IFlowNode node, int taskId);
        void AfterExecuteNodeSuspension(string wfId, string piId, IFlowNode node, int taskId);
        void BeforeStartNewSequence(string wfId, string piId, IFlowNode firstNode, INodeCaller? caller);
        void BeforeExecuteNode(string wfId, string piId, IFlowNode node, int taskId, INodeCaller? caller);
        void OnStatusChanged(string wfId, string piId);
        void OnTasksChanged(string wfId, string piId);
        void OnTimeout(string wfId, string piId);
        void OnProcessFinished(string wfId, string piId, string result);
        void OnCallerChanged(string callerId, CallerChangedType type);
    }
}
