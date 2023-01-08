﻿using Grpc.Net.Client;
using Polokus.Core.Models.BpmnObjects.Xsd;
using Polokus.Core.Services.Interfaces;
using RemoteServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Remote
{
    public class GrpcRemoteProcessInstancesService : IProcessInstancesService
    {
        RemoteServices.RemoteProcessInstanceService.RemoteProcessInstanceServiceClient _serviceClient;

        public GrpcRemoteProcessInstancesService(GrpcChannel channel)
        {
            _serviceClient = new RemoteServices.RemoteProcessInstanceService.RemoteProcessInstanceServiceClient(channel);
        }

        public IEnumerable<string> GetActiveNodesIds(string wfId, string piId)
        {
            var request = new GetActiveNodesIdsRequest()
            {
                WfId = wfId,
                PiId = piId
            };

            var reply = _serviceClient.GetActiveNodesIds(request);
            return reply.ActiveNodesIds;
        }

        public IEnumerable<string> GetAllNodesIds(string wfId, string piId)
        {
            var request = new GetAllNodesIdsRequest()
            {
                WfId = wfId,
                PiId = piId
            };

            var reply = _serviceClient.GetAllNodesIds(request);
            return reply.AllNodesIds;
        }

        public string GetNodeName(string wfId, string piId, string nodeId)
        {
            var request = new GetNodeNameRequest()
            {
                WfId = wfId,
                PiId = piId,
                NodeId = nodeId
            };

            var reply = _serviceClient.GetNodeName(request);
            return reply.NodeName;
        }

        public Type GetNodeXmlType(string wfId, string piId, string nodeId)
        {
            var request = new GetNodeXmlTypeRequest()
            {
                WfId = wfId,
                PiId = piId,
                NodeId = nodeId
            };

            var reply = _serviceClient.GetNodeXmlType(request);
            // TODO: type from reply.TypeName
            return typeof(string);
        }

        public string GetTotalTime(string wfId, string piId)
        {
            var request = new GetTotalTimeRequest()
            {
                WfId = wfId,
                PiId = piId
            };

            var reply = _serviceClient.GetTotalTime(request);
            return reply.Time;
        }

        public void RemoveAwaitingToken(string wfId, string piId, string token)
        {
            var request = new RemoveAwaitingTokenRequest()
            {
                WfId = wfId,
                PiId = piId,
                Token = token
            };

            _serviceClient.RemoveAwaitingToken(request);
        }

        public void SetUserDecisionForUserTaskNH(string wfId, string piId, string nodeId, string answer)
        {
            var request = new SetUserDecisionRequest()
            {
                WfId = wfId,
                PiId = piId,
                NodeId = nodeId,
                Answer = answer
            };

            _serviceClient.SetUserDecisionForUserTaskNH(request);
        }


    }
}
