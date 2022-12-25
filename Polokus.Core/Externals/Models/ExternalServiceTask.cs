using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Externals.Models
{
    public class ExternalServiceTask
    {
        public string Assembly { get; set; } = string.Empty;
        public string ClassName { get; set; } = string.Empty;
        public string ServiceTaskName { get; set; } = string.Empty;
    }
}
