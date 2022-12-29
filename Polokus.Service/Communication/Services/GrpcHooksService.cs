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
using Polokus.Core.Interfaces;
using Polokus.Service.Communication;

namespace Polokus.Service.Communication.Services
{
    public class GrpcHooksService : RemoteHooksService.RemoteHooksServiceBase
    {
        public override async Task WaitForEvents(Empty request,
            IServerStreamWriter<HookReply> responseStream, ServerCallContext context)
        {
            var streamingHooksProvider = new StreamingHooksProvider(responseStream);
            PolokusService.Master.HooksManager.RegisterHooksProvider(streamingHooksProvider);

            while(true)
            //while (PolokusService.Master.ClientConnected)
            {
                await Task.Delay(1000);
            }

            PolokusService.Master.HooksManager.DeregisterHooksProvider(streamingHooksProvider);
        }
    }

}
