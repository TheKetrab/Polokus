namespace Polokus.Core.Interfaces.BpmnModels
{
    public interface IBoundaryEvent : IFlowNode
    {
        IFlowNode? AttachedTo { get; }
        BoundaryEventType Type { get; }
        bool Interrupting { get; }
    }
}
