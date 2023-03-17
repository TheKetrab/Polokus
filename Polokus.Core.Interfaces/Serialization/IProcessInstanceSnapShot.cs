namespace Polokus.Core.Interfaces.Serialization
{
    /// <summary>
    /// Light object easy to serialize that represents ProcessInstance.
    /// </summary>
    public interface IProcessInstanceSnapShot
    {
        /// <summary>
        /// Id of process instance.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Id of workflow that process instance is working in.
        /// </summary>
        string WorkflowId { get; }

        /// <summary>
        /// Id of bpmn process.
        /// </summary>
        string BpmnProcessId { get; }

        /// <summary>
        /// Ids of FlowNodes that are currently processed by some tasks.
        /// </summary>
        string[] AciveNodes { get; }

        /// <summary>
        /// Status from StatusManager.
        /// </summary>
        string Status { get; }

        /// <summary>
        /// Id of parent process instance (if it is subprocess).
        /// </summary>
        string ParentProcessInstanceId { get; }

        /// <summary>
        /// Ids of nodes that have any active waiter waiting for it.
        /// </summary>
        string[] IdsOfNodesThatHadWaiters { get; }

        /// <summary>
        /// Ids of nodes that failed.
        /// </summary>
        string[] FailedExecutionNodeIds { get; }
    }
}
