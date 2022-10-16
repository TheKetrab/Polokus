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

        public ICollection<string> ActiveListeners { get; } = new HashSet<string>();

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
                ActiveListeners.Add(waiter.Id);
                listener.Prefixes.Add($"http://localhost:8080/{waiter.Id}/");
                listener.Start();

                var context = await listener.GetContextAsync();
                ActiveListeners.Remove(waiter.Id);
                waiter.Invoke();
            }
        }

        private async Task WaitForMessage(IProcessStarter starter)
        {
            using (var listener = new HttpListener())
            {
                ActiveListeners.Add(starter.Id);
                listener.Prefixes.Add($"http://localhost:8080/{starter.Id}/");
                listener.Start();

                var context = await listener.GetContextAsync();
                ActiveListeners.Remove(starter.Id);

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


    }
}
