namespace Polokus.Core.Interfaces
{
    /// <summary>
    /// WorkflowsManager is an object that manages all loaded XML definitions.
    /// </summary>
    public interface IWorkflowsManager
    {
        /// <summary>
        /// Collection of loaded Workflow instances accessible by their ids.
        /// </summary>
        IDictionary<string,IWorkflow> Workflows { get; }
    }
}
