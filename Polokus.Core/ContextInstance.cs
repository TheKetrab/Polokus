using Polokus.Core.Factories;
using Polokus.Core.Interfaces;
using Polokus.Core.Models.BpmnObjects.Xsd;
using Polokus.Core.Models;
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
        private int _counter = 0; // processInstanceId
        private object _lock = new object();
        public int GetAnotherProcessId()
        {
            lock (_lock)
            {
                return _counter++;
            }
        }


        public ICollection<IProcessInstance> History { get; } = new List<IProcessInstance>();
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

        public event EventHandler<string> ProcessInstanceChanged;
        public void NotifyProcessInstanceChanged(string id)
        {
            ProcessInstanceChanged?.Invoke(null, id);
        }


        private bool IsTimeout(DateTime start, int? timeout)
        {
            return timeout != null && timeout >= 0
                && DateTime.Now - start >= TimeSpan.FromSeconds(timeout.Value);
        }

        public async Task<bool> RunProcessAsync(IBpmnProcess bpmnProcess, IFlowNode startNode, int? timeout)
        {
            string processId = $"pi{GetAnotherProcessId()}_{bpmnProcess.Id}";
            ProcessInstance instance = new ProcessInstance(processId, this, bpmnProcess, HooksProvider);
            ProcessInstances.Add(instance);
            ProcessInstanceChanged?.Invoke(this,instance.Id);

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
            instance.Finish();
            instance.Logs.Add($"Process finished. Time: {instance.TotalTime}");
            ProcessInstances.Remove(instance);
            History.Add(instance);
            ProcessInstanceChanged?.Invoke(this, instance.Id);
            return true;
        }

        public void StartProcessInstance(IBpmnProcess bpmnProcess, IFlowNode startNode, int? timeout)
        {
            Task.Run(async () => await RunProcessAsync(bpmnProcess, startNode, timeout));
        }


        public void StartProcessManually(string bpmnProcessId)
        {
            var bpmnProcess = BpmnContext.BpmnProcesses.Single(x => x.Id == bpmnProcessId);
            var startNode = bpmnProcess.GetManualStartNode();
            StartProcessInstance(bpmnProcess, startNode, -1);
        }
    }
}
