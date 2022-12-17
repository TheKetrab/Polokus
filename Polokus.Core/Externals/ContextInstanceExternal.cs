using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Externals
{
    public class ContextInstanceExternal
    {
        public string Name { get; set; } = string.Empty;
        public List<ServiceTaskExternal> ServiceTasks { get; set; } = new();
    }
}
