using Polokus.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Remote.Models
{
    public class RemoteWorkflow
    {
        string Id { get; }
        string BpmnRawString { get; }
        ICollection<IProcessInstance> ProcessInstances { get; }
        ICollection<IProcessInstance> History { get; }
    }
}
