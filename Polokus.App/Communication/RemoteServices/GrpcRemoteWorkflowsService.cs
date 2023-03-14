using Grpc.Net.Client;
using Polokus.Core.Interfaces.Communication;
using RemoteServices;

using RemoteModels = Polokus.Core.Interfaces.Communication.Models;

namespace Polokus.Core.Remote
{
    public class GrpcRemoteWorkflowsService : IWorkflowsService
    {
        RemoteServices.RemoteWorkflowsService.RemoteWorkflowsServiceClient _serviceClient;

        public GrpcRemoteWorkflowsService(GrpcChannel channel)
        {
            _serviceClient = new RemoteServices.RemoteWorkflowsService.RemoteWorkflowsServiceClient(channel);
        }

        public IEnumerable<string> GetBpmnProcessesIds(string wfId)
        {
            var request = new WorkflowIdRequest()
            {
                WfId = wfId
            };

            var reply = _serviceClient.GetBpmnProcessesIds(request);
            return reply.Ids;
        }

        public IEnumerable<string> GetManualBpmnProcessesIds(string wfId)
        {
            var request = new WorkflowIdRequest()
            {
                WfId = wfId
            };

            var reply = _serviceClient.GetManualBpmnProcessesIds(request);
            return reply.Ids;
        }

        public IEnumerable<RemoteModels.RawProcessInstance> GetProcessInstancesInfos(string wfId)
        {
            var request = new WorkflowIdRequest()
            {
                WfId = wfId
            };

            var reply = _serviceClient.GetProcessInstancesInfos(request);

            var result = reply.InstancesInfos.Select(x => new RemoteModels.RawProcessInstance()
            {
                Id = x.Id,
                ActiveTasks = x.ActiveTasks,
                Status = x.Status
            });

            return result;
        }

        public IEnumerable<RemoteModels.RawProcessStarter> GetProcessStarters(string wfId)
        {
            var request = new WorkflowIdRequest()
            {
                WfId = wfId
            };

            var reply = _serviceClient.GetProcessStarters(request);

            var result = reply.Starters.Select(x => new RemoteModels.RawProcessStarter()
            {
                Id = x.Id,
                StarterType = x.StarterType,
                StartNode = x.StartNodeId
            });

            return result;

        }

        public string GetRawString(string wfId)
        {
            var request = new WorkflowIdRequest()
            {
                WfId = wfId
            };

            var reply = _serviceClient.GetRawString(request);
            return reply.RawString;
        }

        public IEnumerable<RemoteModels.RawNodeHandlerWaiter> GetNodeHandlerWaiters(string wfId)
        {
            var request = new WorkflowIdRequest()
            {
                WfId = wfId
            };

            var reply = _serviceClient.GetNodeHandlerWaiters(request);

            var result = reply.Waiters.Select(x => new RemoteModels.RawNodeHandlerWaiter()
            {
                Id = x.Id,
                NodeToCall = x.NodeToCallId,
                WaiterType = x.WaiterType
            });

            return result;
        }

        public string StartProcessManually(string wfId, string bpmnProcessId)
        {
            var request = new StartProcessManuallyRequest()
            {
                WfId = wfId,
                BpmnProcessId = bpmnProcessId
            };

            var reply = _serviceClient.StartProcessManually(request);
            return reply.PiId;
        }

        public void PingListener(string wfId, string listenerId)
        {
            var request = new PingListenerRequest()
            {
                WfId = wfId,
                ListenerId = listenerId
            };

            _serviceClient.PingListener(request);
        }

        public void RaiseSignal(string wfId, string signal)
        {
            throw new NotImplementedException();
        }

        public void StopProcessInstance(string wfId, string piId)
        {
            throw new NotImplementedException();
        }

        public void PauseProcessInstance(string wfId, string piId)
        {
            throw new NotImplementedException();
        }

        public void ResumeProcessInstance(string wfId, string piId)
        {
            throw new NotImplementedException();
        }
    }
}
