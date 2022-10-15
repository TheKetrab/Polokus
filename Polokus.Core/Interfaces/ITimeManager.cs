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
        IContextsManager ContextsManager { get; }
        void RegisterWaiter(string timeString, INodeHandlerWaiter waiter, bool oneTime);
    }
}
