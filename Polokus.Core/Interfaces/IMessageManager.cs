using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Interfaces
{
    public interface IMessageManager
    {
        IEnumerable<IProcessStarter> GetStarters();
        IEnumerable<INodeHandlerWaiter> GetWaiters();


        void RegisterMessageListener(INodeHandlerWaiter waiter);
        void RegisterMessageListener(IProcessStarter starter);
        Task PingListener(string listenerId);


    }
}
