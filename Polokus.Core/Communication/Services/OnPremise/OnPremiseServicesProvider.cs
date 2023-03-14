using Polokus.Core.Interfaces.Communication;

namespace Polokus.Core.Communication.Services.OnPremise
{
    public class OnPremiseServicesProvider : IServicesProvider
    {
        private PolokusMaster _polokus;

        public OnPremiseServicesProvider(PolokusMaster polokus)
        {
            _polokus = polokus;
        }

        public ILogsService LogsService => new OnPremiseLogsService(_polokus);

        public IPolokusService PolokusService => new OnPremisePolokusService(_polokus);

        public IProcessInstancesService ProcessInstancesService => new OnPremiseProcessInstancesService(_polokus);

        public IWorkflowsService WorkflowsService => new OnPremiseWorkflowsService(_polokus);
    }
}
