namespace Polokus.Core.Interfaces
{
    /// <summary>
    /// Waiter is an object that is responsible for calling FlowNode, when an event occures.
    /// </summary>
    public interface INodeHandlerWaiter : INodeCaller
    {
        IFlowNode NodeToCall { get; }
        IProcessInstance ProcessInstance { get; }
        IHooksProvider? HooksProvider { get; }
        void Invoke();
    }

    public enum WaiterType
    {
        None,
        Timer,
        Message,
        Signal
    }

}
