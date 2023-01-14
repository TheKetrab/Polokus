using Polokus.Core.Interfaces;
using Polokus.Core.Interfaces.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
