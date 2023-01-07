using Polokus.Core.Execution;
using Polokus.Core.Helpers;

namespace Polokus.Core.Interfaces
{
    /// <summary>
    /// ProcessInstance is an object that is responsible for managing life of process defined via BpmnProcess.
    /// </summary>
    public interface IProcessInstance
    {
        string Id { get; }
        IWorkflow Workflow { get; }
        IBpmnProcess BpmnProcess { get; }
        ActiveTasksManager ActiveTasksManager { get; }
        IProcessInstance? ParentProcessInstance { get; }
        IStatusManager StatusManager { get; }
        IHooksProvider? HooksProvider { get; set; }


        ICollection<IProcessInstance> ChildrenProcessInstances { get; }
        IEnumerable<INodeHandlerWaiter> Waiters { get; }
        IDictionary<string,INodeHandler> AvailableNodeHandlers { get; }
        ICollection<string> FailedExecutionNodeIds { get; }


        bool ExistsAnotherTaskAbleToCallTarget(IFlowNode target, List<string> callers);
        void StartNewSequence(IFlowNode firstNode, INodeCaller? caller);
        IProcessInstance CreateSubProcessInstance(IBpmnProcess bpmnProcess);

        void Log(string info, Logger.MsgType type);
    }

   
}
