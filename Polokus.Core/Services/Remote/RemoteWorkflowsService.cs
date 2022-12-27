using Polokus.Core.Execution;
using Polokus.Core.Interfaces;
using Polokus.Core.Remote.Models;
using Polokus.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Remote
{
    public class RemoteWorkflowsService : IWorkflowsService
    {
        public IEnumerable<string> GetBpmnProcessesIds(string wfId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetManualBpmnProcessesIds(string wfId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RawProcessInstance> GetProcessInstancesInfos(string wfId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RawProcessStarter> GetProcessStarters(string wfId)
        {
            throw new NotImplementedException();
            //waiters.AddRange(workflow.TimeManager.GetWaiters().Select(x => Tuple.Create("Time", x)));
            //waiters.AddRange(workflow.MessageManager.GetWaiters().Select(x => Tuple.Create("Message", x)));
            //waiters.AddRange(workflow.SignalManager.GetWaiters().Select(x => Tuple.Create("Signal", x)));
        }

        public string GetRawString(string wfId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RawNodeHandlerWaiter> GetNodeHandlerWaiters(string wfId)
        {
            throw new NotImplementedException();
        }

        public string StartProcessManually(string wfId, string bpmnProcessId)
        {
            throw new NotImplementedException();
        }

        public void PingListener(string wfId, string listenerId)
        {
            throw new NotImplementedException();
        }
    }
}
