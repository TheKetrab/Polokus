using Polokus.Core.Interfaces.Communication;
using Polokus.Core.Interfaces.Communication.Models;
using Polokus.Core.Interfaces.Exceptions;

namespace Polokus.Core.Communication.Services.OnPremise
{
    public class OnPremiseWorkflowsService : IWorkflowsService
    {
        private PolokusMaster _polokus;

        public OnPremiseWorkflowsService(PolokusMaster polokus)
        {
            _polokus = polokus;
        }

        public IEnumerable<string> GetBpmnProcessesIds(string wfId)
        {
            return _polokus.GetWorkflow(wfId).BpmnWorkflow.BpmnProcesses.Select(x => x.Id);
        }

        public IEnumerable<string> GetManualBpmnProcessesIds(string wfId)
        {
            return _polokus.GetWorkflow(wfId).BpmnWorkflow.BpmnProcesses
                .Where(x => x.IsManualProcess())
                .Select(x => x.Id);
        }

        public IEnumerable<RawNodeHandlerWaiter> GetNodeHandlerWaiters(string wfId)
        {
            return _polokus.GetWorkflow(wfId).GetAllWaiters().Select(x => new RawNodeHandlerWaiter()
            {
                Id = x.Item2.Id,
                NodeToCall = x.Item2.NodeToCall.Id,
                WaiterType = x.Item1
            });
        }

        public IEnumerable<RawProcessInstance> GetProcessInstancesInfos(string wfId)
        {
            var result = new List<RawProcessInstance>();

            result.AddRange(
                _polokus.GetWorkflow(wfId).ProcessInstances.GetAll().Select(x => new RawProcessInstance()
                {
                    Id = x.Id,
                    ActiveTasks = x.ActiveTasksManager.Count().ToString(),
                    Status = x.StatusManager.Status.ToString()
                }));

            result.AddRange(
                _polokus.GetWorkflow(wfId).History.GetAll().Select(x => new RawProcessInstance()
                {
                    Id = x.Id,
                    ActiveTasks = x.ActiveTasksManager.Count().ToString(),
                    Status = x.StatusManager.Status.ToString()
                }));

            return result;
        }

        public IEnumerable<RawProcessStarter> GetProcessStarters(string wfId)
        {
            return _polokus.GetWorkflow(wfId).GetAllProcessStarters().Select(x => new RawProcessStarter()
            {
                Id = x.Item2.Id,
                StartNode = x.Item2.StartNode.Id,
                StarterType = x.Item1
            });
        }

        public string GetRawString(string wfId)
        {
            return _polokus.GetWorkflow(wfId).BpmnWorkflow.RawString
                ?? throw new PolokusException($"Workflow {wfId} does not have raw string.");
        }

        public void PauseProcessInstance(string wfId, string piId)
        {
            _polokus.GetWorkflow(wfId)
                .GetProcessInstanceById(piId)
                .StatusManager.Pause();
        }

        public void PingListener(string wfId, string listenerId)
        {
            _polokus.GetWorkflow(wfId).MessageManager.PingListener(listenerId);
        }

        public void ResumeProcessInstance(string wfId, string piId)
        {
            _polokus.GetWorkflow(wfId)
                .GetProcessInstanceById(piId)
                .StatusManager.Resume();
        }

        public void RaiseSignal(string wfId, string signal)
        {
            _polokus.GetWorkflow(wfId).SignalManager.EmitSignal(signal);
        }

        public string StartProcessManually(string wfId, string bpmnProcessId)
        {
            var pi = _polokus.GetWorkflow(wfId).StartProcessManually(bpmnProcessId);
            return pi.Id;
        }

        public void StopProcessInstance(string wfId, string piId)
        {
            _polokus.GetWorkflow(wfId)
                .GetProcessInstanceById(piId)
                .StatusManager.Stop();
        }
    }
}
