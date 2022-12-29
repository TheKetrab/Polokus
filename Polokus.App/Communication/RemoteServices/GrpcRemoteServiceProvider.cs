using Grpc.Net.Client;
using Polokus.Core.Remote;
using Polokus.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Services.Remote
{
    public class GrpcRemoteServiceProvider : IServicesProvider
    {
        GrpcChannel _channel;

        public GrpcRemoteServiceProvider(string uri)
        {
            _channel = GrpcChannel.ForAddress(uri);

            LogsService = new GrpcRemoteLogsService(_channel);

            PolokusService = new GrpcRemotePolokusService(_channel);

            ProcessInstancesService = new GrpcRemoteProcessInstancesService(_channel);

            WorkflowsService = new GrpcRemoteWorkflowsService(_channel);

        }

        public ILogsService LogsService { get; }
        public IPolokusService PolokusService { get; }
        public IProcessInstancesService ProcessInstancesService { get; }
        public IWorkflowsService WorkflowsService { get; }
    }
}
