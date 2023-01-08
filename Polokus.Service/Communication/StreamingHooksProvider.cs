using Grpc.Core;
using Polokus.Core.Interfaces;
using Polokus.Core.Models.BpmnObjects.Xsd;
using RemoteServices;
using System;
using System.Threading.Tasks;

namespace Polokus.Service.Communication
{
    public class StreamingHooksProvider : IHooksProvider
    {
        private IServerStreamWriter<HookReply> _stream;

        public StreamingHooksProvider(IServerStreamWriter<HookReply> stream)
        {
            _stream = stream;
        }

        public void AfterExecuteNodeFailure(string wfId, string piId, string nodeId, int taskId)
        {
            var reply = new HookReply()
            {
                Type = HookType.AfterExecuteNodeFailure,
                WfId = wfId,
                PiId = piId,
                NodeId = nodeId,
            };
            reply.Args.Add(taskId.ToString());

            _stream.WriteAsync(reply);
        }

        public void AfterExecuteNodeSuccess(string wfId, string piId, string nodeId, int taskId)
        {
            var reply = new HookReply()
            {
                Type = HookType.AfterExecuteNodeSuccess,
                WfId = wfId,
                PiId = piId,
                NodeId = nodeId,
            };
            reply.Args.Add(taskId.ToString());

            _stream.WriteAsync(reply);
        }

        public void AfterExecuteNodeSuspension(string wfId, string piId, string nodeId, int taskId)
        {
            var reply = new HookReply()
            {
                Type = HookType.AfterExecuteNodeSuspension,
                WfId = wfId,
                PiId = piId,
                NodeId = nodeId,
            };
            reply.Args.Add(taskId.ToString());

            _stream.WriteAsync(reply);
        }

        public void BeforeExecuteNode(string wfId, string piId, string nodeId, int taskId, string? callerNodeId)
        {
            var reply = new HookReply()
            {
                Type = HookType.BeforeExecuteNode,
                WfId = wfId,
                PiId = piId,
                NodeId = nodeId,
            };
            reply.Args.Add(taskId.ToString());
            reply.Args.Add(SaveString(callerNodeId));

            _stream.WriteAsync(reply);
        }

        public void BeforeStartNewSequence(string wfId, string piId, string nodeId, string? callerNodeId)
        {
            var reply = new HookReply()
            {
                Type = HookType.BeforeStartNewSequence,
                WfId = wfId,
                PiId = piId,
                NodeId = nodeId,
            };
            reply.Args.Add(SaveString(callerNodeId));

            _stream.WriteAsync(reply);
        }

        public void OnCallerChanged(string callerId, string callerChangedType)
        {
            var reply = new HookReply()
            {
                Type = HookType.OnCallerChanged,
            };
            reply.Args.Add(callerId);
            reply.Args.Add(callerChangedType);

            _stream.WriteAsync(reply);
        }

        public void OnProcessFinished(string wfId, string piId, string result)
        {
            var reply = new HookReply()
            {
                Type = HookType.OnProcessFinished,
                WfId = wfId,
                PiId = piId,
            };
            reply.Args.Add(result);

            _stream.WriteAsync(reply);
        }

        public void OnStatusChanged(string wfId, string piId)
        {
            var reply = new HookReply()
            {
                Type = HookType.OnStatusChanged,
                WfId = wfId,
                PiId = piId,
            };

            _stream.WriteAsync(reply);
        }

        public void OnTasksChanged(string wfId, string piId)
        {
            var reply = new HookReply()
            {
                Type = HookType.OnTasksChanged,
                WfId = wfId,
                PiId = piId,
            };

            _stream.WriteAsync(reply);
        }

        public void OnTimeout(string wfId, string piId)
        {
            var reply = new HookReply()
            {
                Type = HookType.OnTimeout,
                WfId = wfId,
                PiId = piId,
            };

            _stream.WriteAsync(reply);
        }

        public void OnAwaitingTokenCreated(string wfId, string piId, string nodeId, string token)
        {
            var reply = new HookReply()
            {
                Type = HookType.OnAwaitingTokenCreated,
                WfId = wfId,
                PiId = piId,
                NodeId = nodeId,
            };

            reply.Args.Add(token);

            _stream.WriteAsync(reply);
        }

        private string SaveString(object? o)
        {
            return o?.ToString() ?? "null";
        }
    }

}