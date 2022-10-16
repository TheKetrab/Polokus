using Polokus.Core.Factories;
using Polokus.Core.Interfaces;
using Polokus.Core.Scripting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Polokus.Core
{
    public class ContextInstance : IContextInstance
    {
        public ICollection<IProcessInstance> ProcessInstances { get; } = new List<IProcessInstance>();

        public IScriptProvider ScriptProvider { get; } = new ScriptProvider();

        public IEnumerable<IProcessStarter> CatchingStartEvents { get; } = new List<IProcessStarter>();

        public IContextsManager ContextsManager { get; }
        public IBpmnContext BpmnContext { get; }

        public INodeHandlerFactory NodeHandlerFactory { get; }
        public string Id { get; }

        IHooksProvider? HooksProvider { get; }

        public ContextInstance(IContextsManager contextsManager, IBpmnContext bpmnContext, string id, IHooksProvider? hooksProvider = null)
        {
            ContextsManager = contextsManager;
            BpmnContext = bpmnContext;
            Id = id;

            var nhFactory = new NodeHandlerFactory();
            nhFactory.SetDefaultNodeHandlers();

            NodeHandlerFactory = nhFactory;
            HooksProvider = hooksProvider;
        }

        private bool IsTimeout(DateTime start, int? timeout)
        {
            return timeout != null && timeout >= 0
                && DateTime.Now - start >= TimeSpan.FromSeconds(timeout.Value);
        }

        public async Task<bool> RunProcessAsync(IBpmnProcess bpmnProcess, IFlowNode startNode, int? timeout)
        {
            ProcessInstance instance = new ProcessInstance(this, bpmnProcess, HooksProvider);
            ProcessInstances.Add(instance);

            DateTime start = DateTime.Now;
            instance.Begin(startNode);
            while (instance.IsRunning())
            {
                await Task.Delay(100);
                if (IsTimeout(start, timeout))
                {
                    HooksProvider?.OnTimeout();
                    ProcessInstances.Remove(instance);
                    return false;
                }
            }

            Logger.Log($"Process finished. Time: {DateTime.Now - start}");
            ProcessInstances.Remove(instance);
            return true;
        }

        public void StartProcessInstance(IBpmnProcess bpmnProcess, IFlowNode startNode, int? timeout)
        {
            Task.Run(async () => await RunProcessAsync(bpmnProcess, startNode, timeout));
        }
    }
}
