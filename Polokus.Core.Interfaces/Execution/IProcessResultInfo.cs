using Polokus.Core.Interfaces.BpmnModels;

namespace Polokus.Core.Interfaces.Execution
{
    public interface IProcessResultInfo
    {
        ProcessResultState State { get; }
        IEnumerable<ISequence>? SequencesToInvoke { get; }
        string? Message { get; }
    }
}
