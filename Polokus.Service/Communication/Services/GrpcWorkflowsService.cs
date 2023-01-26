using Grpc.Core;
using Polokus.Core.Communication.Services.OnPremise;
using RemoteServices;

namespace Polokus.Service.Communication.Services
{
    public class GrpcWorkflowsService : RemoteWorkflowsService.RemoteWorkflowsServiceBase
    {
        private OnPremiseServicesProvider _servicesProvider;

        public GrpcWorkflowsService()
        {
            _servicesProvider = PolokusService.SP;
        }

        public override Task<GetBpmnProcessesIdsReply> GetBpmnProcessesIds(WorkflowIdRequest request, ServerCallContext context)
        {
            PolokusService.Proxy(request, context);

            var ids = _servicesProvider.WorkflowsService.GetBpmnProcessesIds(request.WfId);

            var reply = new GetBpmnProcessesIdsReply();
            reply.Ids.AddRange(ids);

            return Task.FromResult(reply);
        }

        public override Task<GetManualBpmnProcessesIdsReply> GetManualBpmnProcessesIds(WorkflowIdRequest request, ServerCallContext context)
        {
            PolokusService.Proxy(request, context);

            var ids = _servicesProvider.WorkflowsService.GetManualBpmnProcessesIds(request.WfId);

            var reply = new GetManualBpmnProcessesIdsReply();
            reply.Ids.AddRange(ids);

            return Task.FromResult(reply);
        }

        public override Task<GetNodeHandlerWaitersReply> GetNodeHandlerWaiters(WorkflowIdRequest request, ServerCallContext context)
        {
            PolokusService.Proxy(request, context);

            var waiters = _servicesProvider.WorkflowsService.GetNodeHandlerWaiters(request.WfId);

            var waiters2 = waiters.Select(x => new RawNodeHandlerWaiter()
            {
                Id = x.Id,
                NodeToCallId = x.NodeToCall,
                WaiterType = x.WaiterType
            });

            var reply = new GetNodeHandlerWaitersReply();
            reply.Waiters.AddRange(waiters2);

            return Task.FromResult(reply);
        }

        public override Task<GetProcessInstancesInfosReply> GetProcessInstancesInfos(WorkflowIdRequest request, ServerCallContext context)
        {
            PolokusService.Proxy(request, context);

            var piInfo = _servicesProvider.WorkflowsService.GetProcessInstancesInfos(request.WfId);

            var piInfo2 = piInfo.Select(x => new RawProcessInstance()
            {
                Id = x.Id,
                ActiveTasks = x.ActiveTasks,
                Status = x.Status
            });

            var reply = new GetProcessInstancesInfosReply();
            reply.InstancesInfos.AddRange(piInfo2);

            return Task.FromResult(reply);
        }

        public override Task<GetProcessStartersReply> GetProcessStarters(WorkflowIdRequest request, ServerCallContext context)
        {
            PolokusService.Proxy(request, context);

            var starters = _servicesProvider.WorkflowsService.GetProcessStarters(request.WfId);

            var starters2 = starters.Select(x => new RawProcessStarter()
            {
                Id = x.Id,
                StarterType = x.StarterType,
                StartNodeId = x.StartNode
            });

            var reply = new GetProcessStartersReply();
            reply.Starters.AddRange(starters2);

            return Task.FromResult(reply);
        }

        public override Task<GetRawStringReply> GetRawString(WorkflowIdRequest request, ServerCallContext context)
        {
            PolokusService.Proxy(request, context);

            var rawString = _servicesProvider.WorkflowsService.GetRawString(request.WfId);

            var reply = new GetRawStringReply()
            {
                RawString = rawString
            };

            return Task.FromResult(reply);
        }

        public override Task<Empty> PingListener(PingListenerRequest request, ServerCallContext context)
        {
            PolokusService.Proxy(request, context);

            _servicesProvider.WorkflowsService.PingListener(request.WfId, request.ListenerId);
            return Task.FromResult(new Empty());
        }

        public override Task<StartProcessManuallyReply> StartProcessManually(StartProcessManuallyRequest request, ServerCallContext context)
        {
            PolokusService.Proxy(request, context);

            string piId = _servicesProvider.WorkflowsService.StartProcessManually(request.WfId, request.BpmnProcessId);

            var reply = new StartProcessManuallyReply()
            {
                PiId = piId
            };

            return Task.FromResult(reply);
        }


    }
}
