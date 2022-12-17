using Polokus.Core.Factories;
using Polokus.Core.Helpers;
using Polokus.Core.Interfaces;
using Polokus.Core.Scripting;

namespace Polokus.Core.Execution
{
    public class Workflow : IWorkflow
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

        public ISettingsProvider SettingsProvider { get; set; }
        public ITimeManager TimeManager { get; }
        public IMessageManager MessageManager { get; }
        public IScriptProvider ScriptProvider { get; }


        public ICollection<IProcessInstance> History { get; } = new List<IProcessInstance>();
        public ICollection<IProcessInstance> ProcessInstances { get; } = new List<IProcessInstance>();



        public IWorkflowsManager WfManager { get; }
        public IBpmnWorkflow BpmnWorkflow { get; }

        public INodeHandlerFactory NodeHandlerFactory { get; }
        public string Id { get; }

        protected IHooksProvider? _hooksProvider;
        public void SetHooksProvider(IHooksProvider provider)
        {
            _hooksProvider = provider;
        }

        public void SetSettingsProvider(ISettingsProvider provider)
        {
            SettingsProvider = provider;
        }

        public Workflow(IWorkflowsManager wfManager, IBpmnWorkflow bpmnWorkflow, string id,
            IHooksProvider? hooksProvider = null, ISettingsProvider? settingsProvider = null)
        {
            if (settingsProvider == null)
            {
                settingsProvider = new DefaultSettingsProvider();
            }

            TimeManager = new TimeManager();
            ScriptProvider = new ScriptProvider();
            SettingsProvider = settingsProvider;
            MessageManager = new MessageManager(SettingsProvider.MessageListenerPort);


            WfManager = wfManager;
            BpmnWorkflow = bpmnWorkflow;
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

        public async Task<bool> RunProcessAsync(IProcessInstance instance, IFlowNode startNode, int? timeout)
        {
            DateTime start = DateTime.Now;
            instance.StatusManager.Begin(startNode);
            while (instance.StatusManager.IsRunning())
            {
                await Task.Delay(100);
                if (IsTimeout(start, timeout))
                {
                    _hooksProvider?.OnTimeout(instance.Id);
                    ProcessInstances.Remove(instance);
                    History.Add(instance);

                    return false;
                }
            }

            Logger.Global.Log($"Process finished. Time: {DateTime.Now - start}");
            instance.StatusManager.Finish();
            ProcessInstances.Remove(instance);
            History.Add(instance);
            _hooksProvider?.OnProcessFinished(instance.Id, "success");
            return true;
        }

        public IProcessInstance CreateProcessInstance(IBpmnProcess bpmnProcess)
        {
            string processId = $"pi{GetAnotherProcessId()}/{bpmnProcess.Id}";
            var instance = new ProcessInstance(processId, this, bpmnProcess, _hooksProvider);
            ProcessInstances.Add(instance);
            _hooksProvider?.OnStatusChanged(instance.Id);
            return instance;
        }

        public IProcessInstance? GetProcessInstanceById(string processInstanceId)
        {
            return ProcessInstances.FirstOrDefault(x => x.Id == processInstanceId)
                ?? History.FirstOrDefault(x => x.Id == processInstanceId);
        }

        public void StartProcessInstance(IProcessInstance processInstance, IFlowNode startNode, int? timeout)
        {
            LoadServiceTasksNodeHandlers(processInstance.BpmnProcess);
            Task.Run(async () => await RunProcessAsync(processInstance, startNode, timeout));
        }

        public IProcessInstance StartProcessInstance(IBpmnProcess bpmnProcess, IFlowNode startNode, int? timeout)
        {
            var processInstance = CreateProcessInstance(bpmnProcess);
            StartProcessInstance(processInstance, startNode, timeout);
            return processInstance;
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


        public IProcessInstance StartProcessManually(string bpmnProcessId)
        {
            var bpmnProcess = BpmnWorkflow.BpmnProcesses.Single(x => x.Id == bpmnProcessId);
            var startNode = bpmnProcess.GetManualStartNode();
            return StartProcessInstance(bpmnProcess, startNode, -1);
        }

    }
}
