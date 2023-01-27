namespace Polokus.Core.Interfaces.BpmnModels
{
    /// <summary>
    /// Boundary Event represents intermediate event on the border of a task.
    /// </summary>
    public interface IBoundaryEvent : IFlowNode
    {
        /// <summary>
        /// FlowNode to which it is connected
        /// </summary>
        IFlowNode? AttachedTo { get; }

        /// <summary>
        /// Concrete type of the event
        /// </summary>
        BoundaryEventType Type { get; }

        /// <summary>
        /// If boundary event is interrupting and is being invoked,
        /// it immediately breaks execution of AttachedTo task.
        /// </summary>
        bool Interrupting { get; }
    }
}
