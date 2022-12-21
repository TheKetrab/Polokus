using Polokus.Core.Hooks;
using Polokus.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Execution
{
    public class MessageManager : IMessageManager
    {
        public int ListeningPort { get; }

        private Dictionary<string, IProcessStarter> _starters = new();
        private Dictionary<string, INodeHandlerWaiter> _waiters = new();

        public MessageManager(int port)
        {
            if (!HttpListener.IsSupported)
            {
                throw new Exception("HttpListener is not supported");
            }

            ListeningPort = port;
        }

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
                waiter.HooksProvider?.OnCallerChanged(waiter.Id, CallerChangedType.WaiterInserted);

                listener.Prefixes.Add($"http://localhost:{ListeningPort}/{waiter.Id}/");
                listener.Start();

                var context = await listener.GetContextAsync();

                _waiters.Remove(waiter.Id);
                waiter.HooksProvider?.OnCallerChanged(waiter.Id, CallerChangedType.WaiterRemoved);

                waiter.Invoke();
            }
        }

        private async Task WaitForMessage(IProcessStarter starter)
        {
            using (var listener = new HttpListener())
            {
                _starters.Add(starter.Id, starter);
                starter.HooksProvider?.OnCallerChanged(starter.Id, CallerChangedType.StarterRegistered);

                listener.Prefixes.Add($"http://localhost:{ListeningPort}/{starter.Id}/");
                listener.Start();

                var context = await listener.GetContextAsync();
                starter.HooksProvider?.OnCallerChanged(starter.Id, CallerChangedType.StarterStartedProcess);

                // TODO: czy to nie jest tak, ze jak raz starter wystartuje proces, to juz drugi raz nie umie?

                string? parentProcessId = context.Request.QueryString["parent"];
                if (parentProcessId != null)
                {
                    var processInstance = starter.Workflow.GetProcessInstanceById(parentProcessId)
                        ?? throw new Exception($"Process instance with id {parentProcessId} doesn't exist.");
                    var subProcessInstance = processInstance.CreateSubProcessInstance(starter.BpmnProcess);
                    starter.Workflow.StartProcessInstance(subProcessInstance, starter.StartNode, null);
                }
                else
                {
                    starter.Workflow.StartProcessInstance(starter.BpmnProcess, starter.StartNode, null);
                }

            }
        }

        public async Task PingListener(string listenerId, string? queryString = null)
        {
            HttpClient client = new HttpClient();
            string uri = $"http://localhost:{ListeningPort}/{listenerId}";

            while (!IsWaiting(listenerId))
            {
                await Task.Delay(100);
            }

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

        public bool IsWaiting(string listenerId)
        {
            return _waiters.Where(x => string.Equals(listenerId, x.Key)).Any()
                || _starters.Where(x => string.Equals(listenerId, x.Key)).Any();
        }
    }
}
