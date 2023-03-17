namespace Polokus.Core.Interfaces.Communication.Models
{
    /// <summary>
    /// Basic information about Workflow.
    /// </summary>
    public class RawWorkflow
    {
        /// <summary>
        /// Id of Workflow.
        /// </summary>
        string Id { get; } = string.Empty;

        /// <summary>
        /// Content of XML bpmn file, that the workflow comes from.
        /// </summary>
        string BpmnRawString { get; } = string.Empty;
    }
}
