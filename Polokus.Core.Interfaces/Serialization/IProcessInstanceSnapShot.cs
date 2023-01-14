namespace Polokus.Core.Interfaces.Serialization
{
    public interface IProcessInstanceSnapShot
    {
        string Id { get; }
        string WorkflowId { get; }
        string BpmnProcessId { get; }
        string[] AciveNodes { get; }
        string Status { get; }
        string ParentProcessInstanceId { get; }
        string[] IdsOfNodesThatHadWaiters { get; }
        string[] FailedExecutionNodeIds { get; }
    }
}
