namespace Polokus.Core.Interfaces
{
    /// <summary>
    /// HooksProvider is an object that allows to inject hooks for some actions during processing a BPMN process.
    /// </summary>
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
        void OnProcessFinished(string processInstanceId, string result);
    }
}
