using Polokus.Core.Interfaces.BpmnModels;
using Polokus.Core.Interfaces.Execution.NodeHandlers;
using Polokus.Core.Interfaces.Extensibility;
using Polokus.Core.Interfaces.Managers;
using Polokus.Core.Interfaces.Serialization;

namespace Polokus.Core.Interfaces.Execution
{
    /// <summary>
    /// IWorkflow is a running instance of its definition: IBpmnWorkflow.
    /// </summary>
    public interface IWorkflow
    {
        string Id { get; }
        IBpmnWorkflow BpmnWorkflow { get; }
        ITimeManager TimeManager { get; }
        IMessageManager MessageManager { get; }
        ISignalManager SignalManager { get; }
        ISettingsProvider SettingsProvider { get; }
        IPolokusMaster PolokusMaster { get; }
        INodeHandlerFactory NodeHandlerFactory { get; }
        IHooksProvider? HooksProvider { get; }

        /// <summary>
        /// List of active instances of processes, defined in BpmnWorkflow.
        /// </summary>
        IConcurencyList<IProcessInstance> ProcessInstances { get; }

        /// <summary>
        /// List of instancess of processes, defined in BpmnWorkflow, which are already finished.
        /// </summary>
        IConcurencyList<IProcessInstance> History { get; }

        /// <summary>
        /// Collection of paused processes, with information to restore them.
        /// </summary>
        IDictionary<IProcessInstance, IProcessInstanceSnapShot> Paused { get; }

        /// <summary>
        /// This method returns instance of BPMN process by id (active or inactive) or null if not found.
        /// </summary>
        /// <param name="processInstanceId">Id of process instance.</param>
        IProcessInstance GetProcessInstanceById(string processInstanceId);

        /// <summary>
        /// This method creates an instance of BPMN process and sets id for them.
        /// </summary>
        /// <param name="bpmnProcess"></param>
        /// <returns></returns>
        IProcessInstance CreateProcessInstance(IBpmnProcess bpmnProcess, IProcessInstance? parent = null);
        
        /// <summary>
        /// This method manually runs instance given as argument <paramref name="processInstance"/>
        /// starting from <paramref name="startNode"/> and asynchronously wait for process to be finished.
        /// Returns true iff process instance finished successfully.
        /// </summary>
        /// <param name="processInstance">Instance of process to run.</param>
        /// <param name="startNode">Node to start process instance in.</param>
        /// <param name="timeout">Optional timeout (in seconds) to break process instance if it takes more seconds.</param>
        Task<bool> RunProcessAsync(IProcessInstance processInstance, IFlowNode startNode, int? timeout = null);

        /// <summary>
        /// This method starts process instance for given BPMN process.
        /// Instance is created and immidiately returned (it is run on a new thread).
        /// </summary>
        /// <param name="bpmnProcess">Definition of BPMN process to run.</param>
        /// <param name="startNode">Node to start process instance in.</param>
        /// <param name="timeout">Optional timeout (in seconds) to break process instance if it takes more seconds.</param>
        IProcessInstance StartProcessInstance(IBpmnProcess bpmnProcess, IFlowNode startNode, int? timeout = null);

        /// <summary>
        /// This method starts process instance given as argument <paramref name="processInstance"/>.
        /// Instance is created and immidiately returned (it is run on a new thread).
        /// </summary>
        /// <param name="bpmnProcess">Definition of BPMN process to run.</param>
        /// <param name="startNode">Node to start process instance in.</param>
        /// <param name="timeout">Optional timeout (in seconds) to break process instance if it takes more seconds.</param>

        void StartProcessInstance(IProcessInstance processInstance, IFlowNode startNode, int? timeout = null);

        /// <summary>
        /// This method starts manual process by finding single manual start node.
        /// Instance is created and immidiately returned (it is run on a new thread).
        /// </summary>
        /// <param name="bpmnProcessId">Id of BPMN process.</param>
        IProcessInstance StartProcessManually(string bpmnProcessId);

        /// <summary>
        /// This method retrieves all waiters now available with their type.
        /// </summary>
        IEnumerable<Tuple<string,INodeHandlerWaiter>> GetAllWaiters();

        /// <summary>
        /// This method retrieves all process starters for current workflow with their type.
        /// </summary>
        IEnumerable<Tuple<string,IProcessStarter>> GetAllProcessStarters();

        /// <summary>
        /// Logs an info.
        /// </summary>
        /// <param name="piId">Process instance id.</param>
        /// <param name="info">Message to log.</param>
        /// <param name="type">Importance of message.</param>
        void Log(string piId, string info, MsgType type);
    }
}
