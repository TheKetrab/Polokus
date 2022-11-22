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
using Quartz;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Net.Http.Json;

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

        public ISettingsProvider SettingsProvider { get; }

        public ITimeManager TimeManager { get; } = new TimeManager();
        public IMessageManager MessageManager { get; }


        public ICollection<IProcessInstance> History { get; } = new List<IProcessInstance>();
        public ICollection<IProcessInstance> ProcessInstances { get; } = new List<IProcessInstance>();

        public IScriptProvider ScriptProvider { get; } = new ScriptProvider();

        public IEnumerable<IProcessStarter> CatchingStartEvents { get; } = new List<IProcessStarter>();

        public IContextsManager ContextsManager { get; }
        public IBpmnContext BpmnContext { get; }

        public INodeHandlerFactory NodeHandlerFactory { get; }
        public string Id { get; }

        protected IHooksProvider? _hooksProvider;
        public void SetHooksProvider(IHooksProvider provider)
        {
            _hooksProvider = provider;
        }

        public ContextInstance(IContextsManager contextsManager, IBpmnContext bpmnContext, string id, IHooksProvider hooksProvider = null, ISettingsProvider settingsProvider = null)
        {
            if (settingsProvider == null)
            {
                settingsProvider = new DefaultSettingsProvider();
            }

            SettingsProvider = settingsProvider;
            MessageManager = new MessageManager(SettingsProvider.MessageListenerPort);


            ContextsManager = contextsManager;
            BpmnContext = bpmnContext;
            Id = id;
            _hooksProvider = hooksProvider;

            var nhFactory = new NodeHandlerFactory();
            nhFactory.SetDefaultNodeHandlers();

            NodeHandlerFactory = nhFactory;
        }

        private bool IsTimeout(DateTime start, int? timeout)
        {
            return timeout != null && timeout >= 0
                && DateTime.Now - start >= TimeSpan.FromSeconds(timeout.Value);
        }

        public async Task<bool> RunProcessAsync(string processInstanceId, IBpmnProcess bpmnProcess, IFlowNode startNode, int? timeout)
        {            
            ProcessInstance instance = new ProcessInstance(processInstanceId, this, bpmnProcess, _hooksProvider);
            ProcessInstances.Add(instance);
            _hooksProvider?.OnStatusChanged(instance.Id);

            DateTime start = DateTime.Now;
            instance.Begin(startNode);
            while (instance.IsRunning())
            {
                await Task.Delay(100);
                if (IsTimeout(start, timeout))
                {
                    _hooksProvider?.OnTimeout(instance.Id);
                    ProcessInstances.Remove(instance);
                    return false;
                }
            }

            Logger.Global.Log($"Process finished. Time: {DateTime.Now - start}");
            instance.Finish();
            ProcessInstances.Remove(instance);
            History.Add(instance);
            _hooksProvider?.OnStatusChanged(instance.Id);
            return true;
        }

        public string StartProcessInstance(IBpmnProcess bpmnProcess, IFlowNode startNode, int? timeout)
        {
            string processId = $"pi{GetAnotherProcessId()}/{bpmnProcess.Id}";
            LoadServiceTasksNodeHandlers(bpmnProcess);

            Task.Run(async () => await RunProcessAsync(processId, bpmnProcess, startNode, timeout));
            return processId;
        }

        private void LoadServiceTasksNodeHandlers(IBpmnProcess bpmnProcess)
        {
            var serviceTasks = bpmnProcess.GetServiceTasksNames();
            if (!serviceTasks.Any())
            {
                return;
            }

            var registrator = new DynamicServiceTaskRegistrator(this);
            foreach (var task in serviceTasks)
            {
                if (!NodeHandlerFactory.IsNodeHandlerForServiceTaskRegistered(task))
                {
                    registrator.RegisterServiceTask(task);
                }

            }
        }


        public string StartProcessManually(string bpmnProcessId)
        {
            var bpmnProcess = BpmnContext.BpmnProcesses.Single(x => x.Id == bpmnProcessId);
            var startNode = bpmnProcess.GetManualStartNode();
            return StartProcessInstance(bpmnProcess, startNode, -1);
        }

        public IProcessInstance GetProcessInstanceById(string processInstanceId)
        {
            var processes = ProcessInstances.Where(x => x.Id == processInstanceId);
            if (processes.Count() > 2)
            {
                throw new Exception();
            }

            if (processes.Count() == 1)
            {
                return processes.First();
            }

            return History.Single(x => x.Id == processInstanceId);
        }
    }
}
