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
        string Id { get; }
        IWorkflow Workflow { get; }
        IBpmnProcess BpmnProcess { get; }
        IActiveTasksManager ActiveTasksManager { get; }
        IProcessInstance? ParentProcessInstance { get; }
        IStatusManager StatusManager { get; }
        IHooksProvider? HooksProvider { get; set; }
        IScriptProvider ScriptProvider { get; }


        ICollection<IProcessInstance> ChildrenProcessInstances { get; }
        IEnumerable<INodeHandlerWaiter> Waiters { get; }
        IDictionary<string, INodeHandler> AvailableNodeHandlers { get; }
        ICollection<string> FailedExecutionNodeIds { get; }
        ICollection<string> AwaitingTokens { get; }


        bool ExistsAnotherTaskAbleToCallTarget(IFlowNode target, List<string> callers);
        void StartNewSequence(IFlowNode firstNode, INodeCaller? caller);
        IProcessInstance CreateSubProcessInstance(IBpmnProcess bpmnProcess);
        void RemoveAwaitingToken(string token);

        void Log(string info, MsgType type);
    }


}
