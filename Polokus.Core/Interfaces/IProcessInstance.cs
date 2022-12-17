using Polokus.Core.Execution;

namespace Polokus.Core.Interfaces
{
    /// <summary>
    /// ProcessInstance is an object that is responsible for managing life of process defined via BpmnProcess.
    /// </summary>
    public interface IProcessInstance
    {
        public string Id { get; }
        IContextInstance ContextInstance { get; }
        IBpmnProcess BpmnProcess { get; }
        ActiveTasksManager ActiveTasksManager { get; }
        IProcessInstance? ParentProcessInstance { get; }
        IStatusManager StatusManager { get; }


        ICollection<IProcessInstance> ChildrenProcessInstances { get; }
        ICollection<INodeHandlerWaiter> Waiters { get; }
        IDictionary<string,INodeHandler> AvailableNodeHandlers { get; }


        bool ExistsAnotherTaskAbleToCallTarget(IFlowNode target, List<string> callers);
        void StartNewSequence(IFlowNode firstNode, INodeCaller? caller);
        IProcessInstance CreateSubProcessInstance(IBpmnProcess bpmnProcess);


    }

   
}
