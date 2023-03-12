using Polokus.Core.Interfaces.Serialization;

namespace Polokus.Core.Serialization
{
    public class ProcessInstanceSnapShot : IProcessInstanceSnapShot
    {
        public string Id { get; set; } = string.Empty;
        public string WorkflowId { get; set; } = string.Empty;
        public string BpmnProcessId { get; set; } = string.Empty;
        public string[]? AciveNodes { get; set; }
        public string Status { get; set; } = string.Empty;
        public string ParentProcessInstanceId { get; set; } = string.Empty;
        public string[]? IdsOfNodesThatHadWaiters { get; set; }
        public string[]? FailedExecutionNodeIds { get; set; }

    }
}
