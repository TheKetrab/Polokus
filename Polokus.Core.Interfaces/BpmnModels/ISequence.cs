namespace Polokus.Core.Interfaces.BpmnModels
{
    /// <summary>
    /// Sequence is a Polokus representation of tSequence element, which describes a connection between nodes.
    /// </summary>
    public interface ISequence
    {
        /// <summary>
        /// Name of Sequence. Points to "name" attribute of XmlElement.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Id of Sequence. Points to "id" attribute of XmlElement.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Source FlowNode element of the edge.
        /// </summary>
        public IFlowNode? Source { get; }

        /// <summary>
        /// Target FlowNode element of the edge.
        /// </summary>
        public IFlowNode? Target { get; }

        /// <summary>
        /// Bpmn process of the sequence.
        /// </summary>
        public IBpmnProcess BpmnProcess { get; }
    }
}
