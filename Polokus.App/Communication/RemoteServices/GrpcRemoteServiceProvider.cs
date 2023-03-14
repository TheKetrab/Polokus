using Grpc.Net.Client;
using Polokus.Core.Interfaces.Communication;
using Polokus.Core.Remote;

namespace Polokus.Core.Services.Remote
{
    public class GrpcRemoteServiceProvider : IServicesProvider
    {
        public GrpcRemoteServiceProvider(GrpcChannel channel)
        {
            LogsService = new GrpcRemoteLogsService(channel);

            PolokusService = new GrpcRemotePolokusService(channel);

            ProcessInstancesService = new GrpcRemoteProcessInstancesService(channel);

            WorkflowsService = new GrpcRemoteWorkflowsService(channel);

        }

        public ILogsService LogsService { get; }
        public IPolokusService PolokusService { get; }
        public IProcessInstancesService ProcessInstancesService { get; }
        public IWorkflowsService WorkflowsService { get; }
    }
}
