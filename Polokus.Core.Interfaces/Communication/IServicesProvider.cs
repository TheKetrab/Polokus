namespace Polokus.Core.Interfaces.Communication
{
    public interface IServicesProvider
    {
        public ILogsService LogsService { get; }
        public IPolokusService PolokusService { get; }
        public IProcessInstancesService ProcessInstancesService { get; }
        public IWorkflowsService WorkflowsService { get; }
    }
}
