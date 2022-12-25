using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Externals.Models
{
    public class ExternalWorkflow
    {
        public string Name { get; set; } = string.Empty;
        public List<ExternalServiceTask> ServiceTasks { get; set; } = new();
    }
}
