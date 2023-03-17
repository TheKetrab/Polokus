namespace Polokus.Core.Interfaces.Communication
{
    /// <summary>
    /// Object with available services.
    /// </summary>
    public interface IServicesProvider
    {
        /// <summary>
        /// Service to control logs.
        /// </summary>
        public ILogsService LogsService { get; }

        /// <summary>
        /// Service to control Polokus Master object.
        /// </summary>
        public IPolokusService PolokusService { get; }

        /// <summary>
        /// Service to control process instance.
        /// </summary>
        public IProcessInstancesService ProcessInstancesService { get; }

        /// <summary>
        /// Service to control Workflow.
        /// </summary>
        public IWorkflowsService WorkflowsService { get; }
    }
}
