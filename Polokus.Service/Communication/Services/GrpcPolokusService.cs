using Grpc.Core;
using Polokus.Core;
using Polokus.Core.Services.OnPremise;
using RemoteServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.Collections;
using Polokus.Core.Helpers;

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
            var ids = _servicesProvider.PolokusService.GetWorkflowsIds();
            var reply = new WorkflowsIdsReply();
            reply.Ids.AddRange(ids);

            return Task.FromResult(reply);
        }

        public override Task<Empty> LoadXmlString(LoadXmlStringRequest request, ServerCallContext context)
        {
            _servicesProvider.PolokusService.LoadXmlString(request.Str, request.WfName);
            return Task.FromResult(new Empty());
        }

    }
}
