using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Remote.Models
{
    public class RawNodeHandlerWaiter
    {
        public string Id { get; set;  }
        public string NodeToCall { get; set; }
        public string WaiterType { get; set; }
    }
}
