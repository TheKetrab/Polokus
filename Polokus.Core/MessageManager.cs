using Polokus.Core.Hooks;
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

        Dictionary<string, IProcessStarter> _starters = new();
        Dictionary<string, INodeHandlerWaiter> _waiters = new();
        public event EventHandler CallersChanged;

        public void RegisterMessageListener(INodeHandlerWaiter waiter)
        {
            Task t = new Task(async () => { await WaitForMessage(waiter); });
            t.Start();
        }

        public void RegisterMessageListener(IProcessStarter starter)
        {
            Task t = new Task(async () => { await WaitForMessage(starter); });
            t.Start();
        }


        private async Task WaitForMessage(INodeHandlerWaiter waiter)
        {
            using (var listener = new HttpListener())
            {
                _waiters.Add(waiter.Id, waiter);
                CallersChanged?.Invoke(null, EventArgs.Empty);

                listener.Prefixes.Add($"http://localhost:8080/{waiter.Id}/");
                listener.Start();

                var context = await listener.GetContextAsync();

                _waiters.Remove(waiter.Id);
                CallersChanged?.Invoke(null, EventArgs.Empty);

                waiter.Invoke();
            }
        }

        private async Task WaitForMessage(IProcessStarter starter)
        {
            using (var listener = new HttpListener())
            {
                _starters.Add(starter.Id, starter);
                CallersChanged?.Invoke(null, EventArgs.Empty);

                listener.Prefixes.Add($"http://localhost:8080/{starter.Id}/");
                listener.Start();

                var context = await listener.GetContextAsync();
                _starters.Remove(starter.Id);
                CallersChanged?.Invoke(null, EventArgs.Empty);

                // invoke
                starter.ContextInstance.StartProcessInstance(starter.BpmnProcess, starter.StartNode, null);
            }
        }

        public async Task PingListener(string listenerId)
        {
            HttpClient client = new HttpClient();
            string uri = $"http://localhost:8080/{listenerId}";
            var msg = new HttpRequestMessage(new HttpMethod("GET"), uri);

            try
            {
                await client.GetAsync(uri);
            }
            catch (Exception exc)
            {
                if (exc.InnerException?.InnerException?.GetType() != typeof(System.Net.Sockets.SocketException))
                {
                    throw;
                }
            }

        }

        public IEnumerable<IProcessStarter> GetStarters()
        {
            return _starters.Values;
        }

        public IEnumerable<INodeHandlerWaiter> GetWaiters()
        {
            return _waiters.Values;
        }
    }
}
