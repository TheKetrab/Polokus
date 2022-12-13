namespace Polokus.Core.Interfaces
{
    /// <summary>
    /// Sequence is a Polokus representation of tSequence element, which describes a connection between nodes.
    /// </summary>
    public interface ISequence
    {
        public string Name { get; }
        public string Id { get; }
        public IFlowNode? Source { get; }
        public IFlowNode? Target { get; }
        public IBpmnProcess BpmnProcess { get; }
    }
}
