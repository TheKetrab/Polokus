namespace Polokus.Core.Interfaces.BpmnModels
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
