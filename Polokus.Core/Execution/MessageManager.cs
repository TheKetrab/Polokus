using Polokus.Core.Hooks;
using Polokus.Core.Interfaces;
using Polokus.Core.Interfaces.BpmnModels;
using Polokus.Core.Interfaces.Managers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Execution
{
    public class MessageManager : BaseCallersManager, IMessageManager
    {
        public int ListeningPort { get; }

        public override IWorkflow Workflow { get; }

        public MessageManager(IWorkflow workflow, int port)
        {
            Workflow = workflow;

            if (!HttpListener.IsSupported)
            {
                throw new Exception("HttpListener is not supported");
            }

            ListeningPort = port;
        }

        private async Task WaitForMessage(INodeHandlerWaiter waiter, bool oneTime, Action? continuation = null)
        {
            bool waiting = true;
            while (waiting)
            {
                using (var listener = new HttpListener())
                {
                    AddWaiter(waiter.Id, waiter);
                    waiter.HooksProvider?.OnCallerChanged(waiter.Id, nameof(CallerChangedType.WaiterInserted));

                    listener.Prefixes.Add($"http://localhost:{ListeningPort}/{waiter.Id}/");
                    listener.Start();

                    var context = await listener.GetContextAsync();

                    if (!IsWaiterCancelled(waiter.Id))
                    {
                        waiter.Invoke();
                        continuation?.Invoke();

                        if (oneTime)
                        {
                            RemoveWaiter(waiter.Id);
                            waiter.HooksProvider?.OnCallerChanged(waiter.Id, nameof(CallerChangedType.WaiterRemoved));
                            waiting = false;
                        }
                    }
                    else
                    {
                        waiting = false;
                    }
                }
            }
        }

        private async Task WaitForMessage(IProcessStarter starter)
        {
            AddStarter(starter.Id, starter);

            bool waiting = true;
            while (waiting)
            {
                using (var listener = new HttpListener())
                {
                    starter.HooksProvider?.OnCallerChanged(starter.Id, nameof(CallerChangedType.StarterRegistered));

                    listener.Prefixes.Add($"http://localhost:{ListeningPort}/{starter.Id}/");
                    listener.Start();

                    var context = await listener.GetContextAsync();
                    starter.HooksProvider?.OnCallerChanged(starter.Id, nameof(CallerChangedType.StarterStartedProcess));

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

        public bool IsWaiting(string listenerId)
        {
            return GetWaiters().Where(x => string.Equals(listenerId, x.Id)).Any()
                || GetStarters().Where(x => string.Equals(listenerId, x.Id)).Any();
        }

        public override INodeHandlerWaiter RegisterWaiter(IProcessInstance pi, IFlowNode node, bool oneTime, Action? continuation = null)
        {
            var waiter = new NodeHandlerWaiter(pi, node);
            Task t = new Task(async () => 
            {
                await WaitForMessage(waiter, oneTime, continuation);
            });
            t.Start();

            return waiter;
        }

        public override IProcessStarter RegisterStarter(IBpmnProcess bpmnProcess, IFlowNode startNode)
        {
            var starter = new ProcessStarter(Workflow, bpmnProcess, startNode);
            Task t = new Task(async () => 
            {
                await WaitForMessage(starter);
            });
            t.Start();

            return starter;
        }

    }
}
