using Polokus.Core.Extensibility;
using Polokus.Core.Factories;
using Polokus.Core.Helpers;
using Polokus.Core.Interfaces.Exceptions;
using Polokus.Core.Interfaces.Execution.NodeHandlers;
using Polokus.Core.Interfaces.Managers;
using Polokus.Core.Interfaces.Serialization;
using Polokus.Core.Managers;

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

        public ITimeManager TimeManager { get; }
        public IMessageManager MessageManager { get; }
        public ISignalManager SignalManager { get; }


        public IConcurencyList<IProcessInstance> History { get; } = new ConcurencyList<IProcessInstance>();
        public IConcurencyList<IProcessInstance> ProcessInstances { get; } = new ConcurencyList<IProcessInstance>();
        public IDictionary<IProcessInstance, IProcessInstanceSnapShot> Paused { get; }
            = new Dictionary<IProcessInstance, IProcessInstanceSnapShot>();

        public IPolokusMaster PolokusMaster { get; }
        public IBpmnWorkflow BpmnWorkflow { get; }

        public INodeHandlerFactory NodeHandlerFactory { get; }
        public string Id { get; }

        public IHooksProvider? HooksProvider { get; }


        public Workflow(IPolokusMaster polokus, IBpmnWorkflow bpmnWorkflow,
            string id, INodeHandlerFactory nhFactory, IHooksProvider? hooksProvider = null)
        {
            TimeManager = new TimeManager(this);
            MessageManager = new MessageManager(this, Settings.MessageListenerPort);
            SignalManager = new SignalManager(this);


            PolokusMaster = polokus;
            BpmnWorkflow = bpmnWorkflow;
            Id = id;

            HooksProvider = hooksProvider;

            NodeHandlerFactory = nhFactory;
        }

        public void Log(string piId, string info, MsgType type)
        {
            PolokusMaster.Log(Id, piId, info, type);
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
                    HooksProvider?.OnTimeout(instance.Workflow.Id, instance.Id);
                    ProcessInstances.Remove(x => x.Id == instance.Id);
                    History.Add(instance);

                    return false;
                }
            }

            Logger.Global.Log($"Process {instance.Id} finished. Time: {DateTime.Now - start}");
            instance.StatusManager.Finish();
            ProcessInstances.Remove(x => x.Id == instance.Id);
            History.Add(instance);
            HooksProvider?.OnProcessFinished(instance.Workflow.Id, instance.Id, instance.StatusManager.Status.ToString());

            bool processSucceed = !instance.FailedExecutionNodeIds.Any();

            return processSucceed;
        }

        public IProcessInstance CreateProcessInstance(IBpmnProcess bpmnProcess, IProcessInstance? parent = null)
        {
            string processId = $"pi{GetAnotherProcessId()}/{bpmnProcess.Id}";
            var instance = new ProcessInstance(processId, this, bpmnProcess, parent);
            instance.HooksProvider = this.HooksProvider;

            if (parent != null)
            {
                instance.HooksProvider = parent.HooksProvider;
            }

            ProcessInstances.Add(instance);
            HooksProvider?.OnStatusChanged(instance.Workflow.Id, instance.Id);
            return instance;
        }

        public IProcessInstance GetProcessInstanceById(string processInstanceId)
        {
            return ProcessInstances.GetAll().FirstOrDefault(x => x.Id == processInstanceId)
                ?? History.GetAll().FirstOrDefault(x => x.Id == processInstanceId)
                ?? throw new ProcessInstanceNotFoundException(processInstanceId);
        }

        public void StartProcessInstance(IProcessInstance processInstance, IFlowNode startNode, int? timeout)
        {
            Task.Run(async () => await RunProcessAsync(processInstance, startNode, timeout));
        }

        public IProcessInstance StartProcessInstance(IBpmnProcess bpmnProcess, IFlowNode startNode, int? timeout)
        {
            var processInstance = CreateProcessInstance(bpmnProcess);
            StartProcessInstance(processInstance, startNode, timeout);
            return processInstance;
        }

        public IProcessInstance StartProcessManually(string bpmnProcessId)
        {
            var bpmnProcess = BpmnWorkflow.BpmnProcesses.Single(x => x.Id == bpmnProcessId);
            var startNode = bpmnProcess.GetManualStartNode();
            return StartProcessInstance(bpmnProcess, startNode, -1);
        }

        public IEnumerable<Tuple<string, INodeHandlerWaiter>> GetAllWaiters()
        {
            var result = new List<Tuple<string, INodeHandlerWaiter>>();

            result.AddRange(TimeManager.GetWaiters().Select(x => Tuple.Create("Time", x)));
            result.AddRange(MessageManager.GetWaiters().Select(x => Tuple.Create("Message", x)));
            result.AddRange(SignalManager.GetWaiters().Select(x => Tuple.Create("Signal", x)));

            return result;
        }

        public IEnumerable<Tuple<string, IProcessStarter>> GetAllProcessStarters()
        {
            var result = new List<Tuple<string, IProcessStarter>>();

            result.AddRange(TimeManager.GetStarters().Select(x => Tuple.Create("Time", x)));
            result.AddRange(MessageManager.GetStarters().Select(x => Tuple.Create("Message", x)));
            result.AddRange(SignalManager.GetStarters().Select(x => Tuple.Create("Signal", x)));

            return result;
        }
    }
}
