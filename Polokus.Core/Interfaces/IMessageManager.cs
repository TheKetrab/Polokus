﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Interfaces
{
    public interface IMessageManager
    {
        ICollection<string> ActiveListeners { get; }
        void RegisterMessageListener(INodeHandlerWaiter waiter);
        void RegisterMessageListener(IProcessStarter starter);
        Task PingListener(string listenerId);

    }
}
