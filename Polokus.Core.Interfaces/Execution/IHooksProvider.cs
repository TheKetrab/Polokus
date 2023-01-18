namespace Polokus.Core.Interfaces.Execution
{
    /// <summary>
    /// HooksProvider is an object that allows to inject hooks for some actions during processing a BPMN process.
    /// </summary>
    public interface IHooksProvider
    {
        void AfterExecuteNodeSuccess(string wfId, string piId, string nodeId, int taskId);
        void AfterExecuteNodeFailure(string wfId, string piId, string nodeId, int taskId);
        void AfterExecuteNodeSuspension(string wfId, string piId, string nodeId, int taskId);
        void BeforeStartNewSequence(string wfId, string piId, string nodeId, string? callerNodeId);
        void BeforeExecuteNode(string wfId, string piId, string nodeId, int taskId, string? callerNodeId);
        void OnStatusChanged(string wfId, string piId);
        void OnTasksChanged(string wfId, string piId);
        void OnTimeout(string wfId, string piId);
        void OnProcessFinished(string wfId, string piId, string result);
        void OnCallerChanged(string callerId, string callerChangedType);
        void OnAwaitingTokenCreated(string wfId, string piId, string nodeId, string token);
    }
}
