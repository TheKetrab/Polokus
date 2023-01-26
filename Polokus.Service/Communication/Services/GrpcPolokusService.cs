using Grpc.Core;
using Polokus.Core.Communication.Services.OnPremise;
using RemoteServices;

namespace Polokus.Service.Communication.Services
{
    public class GrpcPolokusService : RemotePolokusService.RemotePolokusServiceBase
    {
        private OnPremiseServicesProvider _servicesProvider;

        public GrpcPolokusService()
        {
            _servicesProvider = PolokusService.SP;
        }

        public override Task<WorkflowsIdsReply> GetWorkflowsIds(Empty request, ServerCallContext context)
        {
            PolokusService.Proxy(request, context);

            var ids = _servicesProvider.PolokusService.GetWorkflowsIds();
            var reply = new WorkflowsIdsReply();
            reply.Ids.AddRange(ids);

            return Task.FromResult(reply);
        }

        public override Task<Empty> LoadXmlString(LoadXmlStringRequest request, ServerCallContext context)
        {
            PolokusService.Proxy(request, context);

            _servicesProvider.PolokusService.LoadXmlString(request.Str, request.WfName);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> SetClientConnected(Empty request, ServerCallContext context)
        {
            _servicesProvider.PolokusService.SetClientConnected();
            return Task.FromResult(new Empty());
        }

    }
}
