using Polokus.Core.Interfaces.Execution;

namespace Polokus.Core.Interfaces.BpmnModels
{
    /// <summary>
    /// FlowNode is a Polokus's representation of tNode stored in XML.
    /// </summary>
    public interface IFlowNode : INodeCaller
    {
        /// <summary>
        /// Name of FlowNode. Points to "name" attribute of XmlElement.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Bpmn process the FlowNode is in.
        /// </summary>
        IBpmnProcess BpmnProcess { get; }

        /// <summary>
        /// Type of base node that correspond to this FlowNode in XML definition file.
        /// </summary>
        Type XmlType { get; }

        /// <summary>
        /// Sequences that are incoming edges in directed graph of process.
        /// </summary>
        ICollection<ISequence> Incoming { get; }

        /// <summary>
        /// Sequences that are outgoing edges in directed graph of process.
        /// </summary>
        ICollection<ISequence> Outgoing { get; }

        /// <summary>
        /// All boundary events attached to this node.
        /// </summary>
        ICollection<IBoundaryEvent> BoundaryEvents { get; }

        /// <summary>
        /// Concrete type of waiter that will be created if necessary to wait.
        /// </summary>
        WaiterType RequireWaiter { get; }
    }

}
