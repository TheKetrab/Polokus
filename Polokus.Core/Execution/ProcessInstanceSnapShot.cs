using Polokus.Core.Interfaces.Serialization;

namespace Polokus.Core.Execution
{
    public class ProcessInstanceSnapShot : IProcessInstanceSnapShot
    {
        public string Id { get; set; }
        public string WorkflowId { get; set; }
        public string BpmnProcessId { get; set; }
        public string[] AciveNodes { get; set; }
        public string Status { get; set; }
        public string ParentProcessInstanceId { get; set; }
        public string[] IdsOfNodesThatHadWaiters { get; set; }
        public string[] FailedExecutionNodeIds { get; set; }

    }
}
