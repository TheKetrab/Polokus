namespace Polokus.Core.Interfaces
{
    /// <summary>
    /// BpmnContext is an object that represents parsed XML definitions file.
    /// </summary>
    public interface IBpmnContext
    {
        /// <summary>
        /// XML string where context is parsed from.
        /// </summary>
        string? RawString { get; }

        /// <summary>
        /// Instance of context.
        /// </summary>
        IContextInstance? ContextInstance { get; }

        /// <summary>
        /// List of BPMN processes defined within context.
        /// </summary>
        IEnumerable<IBpmnProcess> BpmnProcesses { get; }
    }
}
