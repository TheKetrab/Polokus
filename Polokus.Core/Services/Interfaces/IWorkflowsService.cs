using Polokus.Core.Remote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Services.Interfaces
{
    public interface IWorkflowsService
    {
        public IEnumerable<string> GetBpmnProcessesIds(string wfId);
        public IEnumerable<string> GetManualBpmnProcessesIds(string wfId);
        public IEnumerable<RawProcessInstance> GetProcessInstancesInfos(string wfId);
        public IEnumerable<RawProcessStarter> GetProcessStarters(string wfId);
        public string GetRawString(string wfId);
        public IEnumerable<RawNodeHandlerWaiter> GetNodeHandlerWaiters(string wfId);
        public string StartProcessManually(string wfId, string bpmnProcessId);
        public void StopProcessInstance(string wfId, string piId);
        public void PauseProcessInstance(string wfId, string piId);
        public void ResumeProcessInstance(string wfId, string piId);
        public void PingListener(string wfId, string listenerId);
        public void RaiseSignal(string wfId, string signal);
    }
}
