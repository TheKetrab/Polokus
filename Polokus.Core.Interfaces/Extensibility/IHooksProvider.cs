namespace Polokus.Core.Interfaces.Extensibility
{
    /// <summary>
    /// HooksProvider is an object that allows to inject hooks for some actions during processing a BPMN process.
    /// </summary>
    public interface IHooksProvider
    {
        /// <summary>
        /// Event raised after execution of node with success.
        /// </summary>
        /// <param name="wfId">Id of workflow with node.</param>
        /// <param name="piId">Id of process instance with node.</param>
        /// <param name="nodeId">Id of node.</param>
        /// <param name="taskId">Task number on which node was executed.</param>
        void AfterExecuteNodeSuccess(string wfId, string piId, string nodeId, int taskId);

        /// <summary>
        /// Event raised after execution of node with failure.
        /// </summary>
        /// <param name="wfId">Id of workflow with node.</param>
        /// <param name="piId">Id of process instance with node.</param>
        /// <param name="nodeId">Id of node.</param>
        /// <param name="taskId">Task number on which node was executed.</param>
        void AfterExecuteNodeFailure(string wfId, string piId, string nodeId, int taskId);

        /// <summary>
        /// Event raised after execution of node with suspension status.
        /// </summary>
        /// <param name="wfId">Id of workflow with node.</param>
        /// <param name="piId">Id of process instance with node.</param>
        /// <param name="nodeId">Id of node.</param>
        /// <param name="taskId">Task number on which node was executed.</param>
        void AfterExecuteNodeSuspension(string wfId, string piId, string nodeId, int taskId);

        /// <summary>
        /// Event raised before a new sequence (a new flow-path, task) is created.
        /// </summary>
        /// <param name="wfId">Id of workflow with node.</param>
        /// <param name="piId">Id of process instance with node.</param>
        /// <param name="nodeId">Id of node.</param>
        /// <param name="callerNodeId">Optional id of caller.</param>
        void BeforeStartNewSequence(string wfId, string piId, string nodeId, string? callerNodeId);

        /// <summary>
        /// Event raised before a node is executed.
        /// </summary>
        /// <param name="wfId">Id of workflow with node.</param>
        /// <param name="piId">Id of process instance with node.</param>
        /// <param name="nodeId">Id of node.</param>
        /// <param name="taskId">Task number on which node was executed.</param>
        /// <param name="callerNodeId">Optional id of caller.</param>
        void BeforeExecuteNode(string wfId, string piId, string nodeId, int taskId, string? callerNodeId);

        /// <summary>
        /// Event raised when status of porcess instance has changed.
        /// </summary>
        /// <param name="wfId">Id of workflow with the process instance.</param>
        /// <param name="piId">Id of process instance which status changed.</param>
        void OnStatusChanged(string wfId, string piId);

        /// <summary>
        /// Event raised when somethind changed in ActiveTasksManager of the process instance.
        /// </summary>
        /// <param name="wfId">Id of workflow with the process instance.</param>
        /// <param name="piId">Id of process instance where tasks have changed.</param>
        void OnTasksChanged(string wfId, string piId);

        /// <summary>
        /// Event raised when the instance of process exceeds allowed time.
        /// </summary>
        /// <param name="wfId">Id of workflow with the process instance.</param>
        /// <param name="piId">Id of process instance.</param>
        void OnTimeout(string wfId, string piId);

        /// <summary>
        /// Event raised when the instance of process finished execution.
        /// </summary>
        /// <param name="wfId">Id of workflow with the process instance.</param>
        /// <param name="piId">Id of process instance</param>
        /// <param name="result">Status of execution: success? error? ...</param>
        void OnProcessFinished(string wfId, string piId, string result);

        /// <summary>
        /// Event raised when something changed with lists of waiters and starter (added a new one or removed).
        /// </summary>
        /// <param name="callerId">Id of caller that changed.</param>
        /// <param name="callerChangedType">Type of caller (timer, message, signal, ...)</param>
        void OnCallerChanged(string callerId, string callerChangedType);

        /// <summary>
        /// Event raised when a new awaiting token was created (and waits to be removed).
        /// </summary>
        /// <param name="wfId">Id of workflow with the process instance.</param>
        /// <param name="piId">Id of process instance</param>
        /// <param name="nodeId">Id of node that while its execution awaiting token has been created.</param>
        /// <param name="token">Id of token.</param>
        void OnAwaitingTokenCreated(string wfId, string piId, string nodeId, string token);
    }
}
