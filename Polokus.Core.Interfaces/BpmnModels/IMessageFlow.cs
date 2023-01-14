namespace Polokus.Core.Interfaces.BpmnModels
{
    public interface IMessageFlow
    {
        public string Name { get; }
        public string Id { get; }
        public IFlowNode? Source { get; }
        public IFlowNode? Target { get; }
        public IBpmnProcess SourceProcess { get; }
        public IBpmnProcess TargetProcess { get; }
    }
}
