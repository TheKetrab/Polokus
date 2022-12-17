namespace Polokus.Core.Interfaces
{
    /// <summary>
    /// BpmnWorkflow is an object that represents parsed XML definitions file.
    /// </summary>
    public interface IBpmnWorkflow
    {
        /// <summary>
        /// XML string where Workflow is parsed from.
        /// </summary>
        string? RawString { get; }

        /// <summary>
        /// Instance of Workflow.
        /// </summary>
        IWorkflow? Workflow { get; }

        /// <summary>
        /// List of BPMN processes defined within Workflow.
        /// </summary>
        IEnumerable<IBpmnProcess> BpmnProcesses { get; }
    }
}
