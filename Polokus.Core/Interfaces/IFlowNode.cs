namespace Polokus.Core.Interfaces
{
    /// <summary>
    /// FlowNode is a Polokus's representation of tNode stored in XML.
    /// </summary>
    public interface IFlowNode : INodeCaller
    {
        string Name { get; }
        IBpmnProcess BpmnProcess { get; }

        /// <summary>
        /// Type of base node that correspond to this FlowNode in XML definition file.
        /// </summary>
        Type XmlType { get; }

        ICollection<ISequence> Incoming { get; }
        ICollection<ISequence> Outgoing { get; }

        ICollection<IBoundaryEvent> BoundaryEvents { get; }

        WaiterType RequireWaiter { get; }
    }

}
