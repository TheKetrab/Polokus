using Grpc.Net.Client;
using Polokus.Core.Helpers;
using Polokus.Core.Services.Interfaces;
using RemoteServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Remote
{
    public class GrpcRemoteLogsService : ILogsService
    {
        RemoteServices.RemoteLogsService.RemoteLogsServiceClient _serviceClient;

        public GrpcRemoteLogsService(GrpcChannel channel)
        {
            _serviceClient = new RemoteServices.RemoteLogsService.RemoteLogsServiceClient(channel);
        }

        public List<Tuple<Logger.MsgType,string>> GetMessages(string wfId, string piId)
        {
            var request = new GetMessagesRequest()
            {
                WfId = wfId,
                PiId = piId,
            };

            var reply = _serviceClient.GetMessages(request);

            var result = reply.Messages.Select(x => Tuple.Create(
                Enum.Parse<Logger.MsgType>(x.MsgType),
                x.MsgInfo));

            return result.ToList();
        }

        public void Log(string globalPiId, Logger.MsgType type, string info)
        {
            var request = new LogRequest()
            {
                GlobalPiId = globalPiId,
                MsgType = type.ToString(),
                MsgInfo = info
            };

            _serviceClient.Log(request);
        }
    }
}
