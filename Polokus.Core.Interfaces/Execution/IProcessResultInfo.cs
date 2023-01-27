using Polokus.Core.Interfaces.BpmnModels;

namespace Polokus.Core.Interfaces.Execution
{
    /// <summary>
    /// Result of NodeHandler execution.
    /// </summary>
    public interface IProcessResultInfo
    {
        /// <summary>
        /// State of result (success/failure/...)
        /// </summary>
        ProcessResultState State { get; }

        /// <summary>
        /// Sequences to invoke in further processing the proces.
        /// </summary>
        IEnumerable<ISequence>? SequencesToInvoke { get; }

        /// <summary>
        /// Additional info.
        /// </summary>
        string? Message { get; }
    }
}
