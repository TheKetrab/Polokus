using Polokus.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core
{
    public class MessageManager : IMessageManager
    {
        public MessageManager()
        {
            if (!HttpListener.IsSupported)
            {
                throw new Exception("HttpListener is not supported");
            }
        }

        public ICollection<string> ActiveListeners { get; } = new HashSet<string>();

        public void RegisterMessageListener(INodeHandlerWaiter waiter)
        {
            Task t = new Task(async () => { await WaitForMessage(waiter); });
            t.Start();
        }

        private async Task WaitForMessage(INodeHandlerWaiter waiter)
        {
            using (var listener = new HttpListener())
            {
                ActiveListeners.Add(waiter.Id);
                listener.Prefixes.Add($"http://localhost:8080/{waiter.Id}/");
                listener.Start();

                var context = await listener.GetContextAsync();
                ActiveListeners.Remove(waiter.Id);
                waiter.Invoke();
            }
        }

    }
}
