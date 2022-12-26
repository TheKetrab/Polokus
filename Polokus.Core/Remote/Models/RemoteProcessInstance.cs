using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Remote.Models
{
    public class RemoteProcessInstance
    {
        public string Id { get; }
        public string Status { get; }
        public string ActiveTasks { get; }
    }
}
