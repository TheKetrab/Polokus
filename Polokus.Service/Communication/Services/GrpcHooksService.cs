using Grpc.Core;
using RemoteServices;

namespace Polokus.Service.Communication.Services
{
    public class GrpcHooksService : RemoteHooksService.RemoteHooksServiceBase
    {
        public override async Task WaitForEvents(Empty request,
            IServerStreamWriter<HookReply> responseStream, ServerCallContext context)
        {
            PolokusService.Proxy(request, context);

            var streamingHooksProvider = new StreamingHooksProvider(responseStream);
            PolokusService.Master.HooksManager.RegisterHooksProvider(streamingHooksProvider);

            while (PolokusService.Master.ClientConnected)
            {
                await Task.Delay(1000);
            }

            PolokusService.Master.HooksManager.DeregisterHooksProvider(streamingHooksProvider);
        }
    }

}
