using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Interfaces
{
    public interface ITimeManager
    {
        void RegisterWaiter(string timeString, INodeHandlerWaiter waiter, bool oneTime);
        void RegisterStarter(string timeString, IProcessStarter starter);
    }
}
