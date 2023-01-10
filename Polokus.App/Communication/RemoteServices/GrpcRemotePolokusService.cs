using Grpc.Net.Client;
using Polokus.Core.Interfaces;
using Polokus.Core.Services.Interfaces;
using RemoteServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Remote
{
    public class GrpcRemotePolokusService : IPolokusService
    {
        RemoteServices.RemotePolokusService.RemotePolokusServiceClient _serviceClient;

        public GrpcRemotePolokusService(GrpcChannel channel)
        {
            _serviceClient = new RemoteServices.RemotePolokusService.RemotePolokusServiceClient(channel);
        }


        public void LoadXmlString(string str, string wfName)
        {
            var request = new LoadXmlStringRequest()
            {
                Str = str,
                WfName = wfName
            };

            _serviceClient.LoadXmlString(request);
        }

        public IEnumerable<string> GetWorkflowsIds()
        {
            var request = new Empty();

            var reply = _serviceClient.GetWorkflowsIds(request);

            return reply.Ids;
        }

        public void RegisterHooksProvider(IHooksProvider hooksProvider)
        {
            // TODO
        }

        public void DeregisterHooksProvider(IHooksProvider hooksProvider)
        {
            // TODO
        }

        public void SetClientConnected()
        {
            var request = new Empty();
            _serviceClient.SetClientConnected(request);
        }
    }
}
