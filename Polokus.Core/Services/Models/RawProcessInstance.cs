using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Remote.Models
{
    public class RawProcessInstance
    {
        public string Id { get; set;  }
        public string Status { get; set;  }
        public string ActiveTasks { get; set; }
    }
}
