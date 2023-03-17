namespace Polokus.Core.Interfaces.BpmnModels
{
    /// <summary>
    /// Message Flow represents line between node and target process or node.
    /// </summary>
    public interface IMessageFlow
    {
        /// <summary>
        /// Name of MessageFlow. Points to "name" attribute of XmlElement.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Id of MessageFlow. Points to "id" attribute of XmlElement.
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
        /// Bpmn process of source element.
        /// </summary>
        public IBpmnProcess SourceProcess { get; }

        /// <summary>
        /// Bpmn process of target element.
        /// </summary>
        public IBpmnProcess TargetProcess { get; }
    }
}
