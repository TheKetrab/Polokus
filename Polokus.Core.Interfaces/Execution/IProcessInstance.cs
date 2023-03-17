using Polokus.Core.Interfaces.BpmnModels;
using Polokus.Core.Interfaces.Execution.NodeHandlers;
using Polokus.Core.Interfaces.Extensibility;
using Polokus.Core.Interfaces.Managers;

namespace Polokus.Core.Interfaces.Execution
{
    /// <summary>
    /// ProcessInstance is an object that is responsible for managing life of process defined via BpmnProcess.
    /// </summary>
    public interface IProcessInstance
    {
        /// <summary>
        /// Id of this instance of process.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Workflow of the process instance.
        /// </summary>
        IWorkflow Workflow { get; }

        /// <summary>
        /// Parsed BPMN definition of this instance. 
        /// </summary>
        IBpmnProcess BpmnProcess { get; }

        /// <summary>
        /// Object that stores information about running tasks (separate sequences).
        /// </summary>
        IActiveTasksManager ActiveTasksManager { get; }

        /// <summary>
        /// If the instance is a subprocess, this points to its parent.
        /// </summary>
        IProcessInstance? ParentProcessInstance { get; }

        /// <summary>
        /// Provides functions to control state of the instance (start, stop, pause, ...).
        /// </summary>
        IStatusManager StatusManager { get; }

        /// <summary>
        /// Hooks provider of the engine.
        /// </summary>
        IHooksProvider? HooksProvider { get; set; }

        /// <summary>
        /// Object with ability to execute scripts and evaluate conditions in choosen language.
        /// Contains global variables of the process instance.
        /// </summary>
        IScriptProvider ScriptProvider { get; }

        /// <summary>
        /// Collection of instances that are subprocesses of the instance.
        /// </summary>
        ICollection<IProcessInstance> ChildrenProcessInstances { get; }

        /// <summary>
        /// List of all active waiters in this process instance (time waiters, signal waiters, ...).
        /// </summary>
        IEnumerable<INodeHandlerWaiter> Waiters { get; }

        /// <summary>
        /// Collection of all created NodeHandlers that are working or suspended.
        /// </summary>
        IDictionary<string, INodeHandler> AvailableNodeHandlers { get; }

        /// <summary>
        /// List of nodes that finished with errors.
        /// </summary>
        ICollection<string> FailedExecutionNodeIds { get; }

        /// <summary>
        /// Collection of awaiting tokens (objects that can suspend invocation until they exist).
        /// </summary>
        ICollection<string> AwaitingTokens { get; }

        /// <summary>
        /// Returns true iff in this process instance exists any 'flow' (active task or a waiter)
        /// that can call <paramref name="target"/> node in the future. It means that there must be
        /// path from a node to the target in directed graph of the BPMN process.
        /// </summary>
        /// <param name="target">FlowNode to achieve in graph.</param>
        /// <param name="callers">Ids of nodes that already called target.</param>
        /// <returns></returns>
        bool ExistsAnotherTaskAbleToCallTarget(IFlowNode target, List<string> callers);

        /// <summary>
        /// Starts an execution on a new task (new flow-path) from a given node on another thread.
        /// </summary>
        /// <param name="firstNode">FlowNode to start the execution from.</param>
        /// <param name="caller">Optional caller that called the node.</param>
        void StartNewSequence(IFlowNode firstNode, INodeCaller? caller);

        /// <summary>
        /// Creates a new subprocess instance. Connects the process instance
        /// with subprocess instance with parent-child relation. Returns created instance.
        /// </summary>
        /// <param name="bpmnProcess">Definition of subprocess.</param>
        IProcessInstance CreateSubProcessInstance(IBpmnProcess bpmnProcess);

        /// <summary>
        /// Removes awaiting token from the collection.
        /// </summary>
        /// <param name="token">Id of token to be removed.</param>
        void RemoveAwaitingToken(string token);

        /// <summary>
        /// Logs info for the process instance.
        /// </summary>
        /// <param name="info">Message.</param>
        /// <param name="type">Importance of message.</param>
        void Log(string info, MsgType type);
    }


}
