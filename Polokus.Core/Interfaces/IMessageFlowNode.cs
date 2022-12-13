namespace Polokus.Core.Interfaces
{
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
