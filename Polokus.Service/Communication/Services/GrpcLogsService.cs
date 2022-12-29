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
    public class GrpcLogsService : RemoteLogsService.RemoteLogsServiceBase
    {
        private OnPremiseServicesProvider _servicesProvider;

        public GrpcLogsService()
        {
            _servicesProvider = PolokusService.SP;
        }


        public override Task<GetMessagesReply> GetMessages(GetMessagesRequest request, ServerCallContext context)
        {
            var messages = _servicesProvider.LogsService.GetMessages(request.WfId, request.PiId);

            var msgreply = messages.Select(x => new MsgReply()
            {
                MsgType = x.Item1.ToString(),
                MsgInfo = x.Item2,
            });

            var reply = new GetMessagesReply();
            reply.Messages.Add(msgreply);

            return Task.FromResult(reply);
        }

        public override Task<Empty> Log(LogRequest request, ServerCallContext context)
        {
            var msgType = (Logger.MsgType)Enum.Parse(typeof(Logger.MsgType), request.MsgType);
            _servicesProvider.LogsService.Log(request.GlobalPiId, msgType, request.MsgInfo);
            return Task.FromResult(new Empty());
        }
    }
}
