using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Remote.Models
{
    public class RemoteNodeHandlerWaiter
    {
        public string Id { get; }
        public string NodeToCall { get; }
        public string WaiterType { get; }
    }
}
