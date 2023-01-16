using Polokus.Core.Interfaces.Communication.Models;

namespace Polokus.Core.Interfaces.Communication
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
