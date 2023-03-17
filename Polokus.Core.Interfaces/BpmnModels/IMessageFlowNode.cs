namespace Polokus.Core.Interfaces.BpmnModels
{
    /// <summary>
    /// Object that stores information about outgoing and incoming MessageFlows objects.
    /// </summary>
    public interface IMessageFlowNode : IFlowNode, IMessageCallerNode, IMessageReceiverNode
    {
    }

    public interface IMessageCallerNode
    {
        public ICollection<IMessageFlow> OutgoingMessages { get; }
    }

    public interface IMessageReceiverNode
    {
        public ICollection<IMessageFlow> IncommingMessages { get; }
    }
}
