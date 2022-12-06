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
        public MessageManager(int port)
        {
            if (!HttpListener.IsSupported)
            {
                throw new Exception("HttpListener is not supported");
            }

            ListeningPort = port;
        }

        public int ListeningPort { get; }


        Dictionary<string, IProcessStarter> _starters = new();
        Dictionary<string, INodeHandlerWaiter> _waiters = new();
        public event EventHandler? CallersChanged;

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

                listener.Prefixes.Add($"http://localhost:{ListeningPort}/{waiter.Id}/");
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

                listener.Prefixes.Add($"http://localhost:{ListeningPort}/{starter.Id}/");
                listener.Start();

                var context = await listener.GetContextAsync();
                CallersChanged?.Invoke(null, EventArgs.Empty);

                string? parentProcessId = context.Request.QueryString["parent"];
                if (parentProcessId != null)
                {
                    var processInstance = starter.ContextInstance.GetProcessInstanceById(parentProcessId);
                    var subProcessInstance = processInstance.CreateSubProcessInstance(starter.BpmnProcess);
                    starter.ContextInstance.StartProcessInstance(subProcessInstance, starter.StartNode, null);
                }
                else
                {
                    starter.ContextInstance.StartProcessInstance(starter.BpmnProcess, starter.StartNode, null);

                }

            }
        }

        public async Task PingListener(string listenerId, string? queryString = null)
        {
            HttpClient client = new HttpClient();
            string uri = $"http://localhost:{ListeningPort}/{listenerId}";
            if (!string.IsNullOrEmpty(queryString))
            {
                uri += $"?{queryString}";
            }
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

        public bool IsAnyWaiting()
        {
            return _waiters.Any() || _starters.Any();
        }
    }
}
