namespace Polokus.Core.Interfaces
{
    /// <summary>
    /// IContextInstance is a running instance of its definition: IBpmnContext.
    /// </summary>
    public interface IContextInstance
    {
        string Id { get; }
        IBpmnContext BpmnContext { get; }
        ITimeManager TimeManager { get; }
        IMessageManager MessageManager { get; }
        ISettingsProvider SettingsProvider { get; }
        IScriptProvider ScriptProvider { get; }
        IContextsManager ContextsManager { get; }
        INodeHandlerFactory NodeHandlerFactory { get; }

        /// <summary>
        /// List of active instances of processes, defined in BpmnContext.
        /// </summary>
        ICollection<IProcessInstance> ProcessInstances { get; }

        /// <summary>
        /// List of instancess of processes, defined in Bpmncontext, which are already finished.
        /// </summary>
        ICollection<IProcessInstance> History { get; }



        /// <summary>
        /// This method returns instance of BPMN process by id (active or inactive) or null if not found.
        /// </summary>
        /// <param name="processInstanceId">Id of process instance.</param>
        IProcessInstance? GetProcessInstanceById(string processInstanceId);

        /// <summary>
        /// This method creates an instance of BPMN process and sets id for them.
        /// </summary>
        /// <param name="bpmnProcess"></param>
        /// <returns></returns>
        IProcessInstance CreateProcessInstance(IBpmnProcess bpmnProcess);
        
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

    }
}
