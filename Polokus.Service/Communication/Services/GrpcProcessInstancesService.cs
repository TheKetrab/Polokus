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
    public class GrpcProcessInstanceService : RemoteProcessInstanceService.RemoteProcessInstanceServiceBase
    {
        private OnPremiseServicesProvider _servicesProvider;

        public GrpcProcessInstanceService()
        {
            _servicesProvider = PolokusService.SP;
        }

        public override Task<GetActiveNodesIdsReply> GetActiveNodesIds(GetActiveNodesIdsRequest request, ServerCallContext context)
        {
            PolokusService.Proxy(request, context);

            var ids = _servicesProvider.ProcessInstancesService.GetActiveNodesIds(request.WfId, request.PiId);

            var reply = new GetActiveNodesIdsReply();
            reply.ActiveNodesIds.AddRange(ids);

            return Task.FromResult(reply);
        }

        public override Task<GetAllNodesIdsReply> GetAllNodesIds(GetAllNodesIdsRequest request, ServerCallContext context)
        {
            PolokusService.Proxy(request, context);

            var ids = _servicesProvider.ProcessInstancesService.GetAllNodesIds(request.WfId, request.PiId);

            var reply = new GetAllNodesIdsReply();
            reply.AllNodesIds.AddRange(ids);

            return Task.FromResult(reply);
        }

        public override Task<GetNodeNameReply> GetNodeName(GetNodeNameRequest request, ServerCallContext context)
        {
            PolokusService.Proxy(request, context);

            var name = _servicesProvider.ProcessInstancesService.GetNodeName(request.WfId, request.PiId, request.NodeId);

            var reply = new GetNodeNameReply()
            {
                NodeName = name
            };

            return Task.FromResult(reply);
        }

        public override Task<GetNodeXmlTypeReply> GetNodeXmlType(GetNodeXmlTypeRequest request, ServerCallContext context)
        {
            PolokusService.Proxy(request, context);

            var type = _servicesProvider.ProcessInstancesService.GetNodeXmlType(request.WfId, request.PiId, request.NodeId);

            var reply = new GetNodeXmlTypeReply()
            {
                TypeName = type.ToString()
            };

            return Task.FromResult(reply);
        }

        public override Task<GetTotalTimeReply> GetTotalTime(GetTotalTimeRequest request, ServerCallContext context)
        {
            PolokusService.Proxy(request, context);

            var time = _servicesProvider.ProcessInstancesService.GetTotalTime(request.WfId, request.PiId);

            var reply = new GetTotalTimeReply()
            {
                Time = time
            };

            return Task.FromResult(reply);
        }

        public override Task<Empty> SetUserDecisionForUserTaskNH(SetUserDecisionRequest request, ServerCallContext context)
        {
            PolokusService.Proxy(request, context);

            _servicesProvider.ProcessInstancesService.SetUserDecisionForUserTaskNH(request.WfId, request.PiId, request.NodeId, request.Answer);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> RemoveAwaitingToken(RemoveAwaitingTokenRequest request, ServerCallContext context)
        {
            PolokusService.Proxy(request, context);

            _servicesProvider.ProcessInstancesService.RemoveAwaitingToken(request.WfId, request.PiId, request.Token);
            return Task.FromResult(new Empty());
        }

    }
}
