using Polokus.Core.Execution;
using Polokus.Core.Execution.NodeHandlers;
using Polokus.Core.Interfaces.Communication;

namespace Polokus.Core.Communication.Services.OnPremise
{
    public class OnPremiseProcessInstancesService : IProcessInstancesService
    {
        private PolokusMaster _polokus;

        public OnPremiseProcessInstancesService(PolokusMaster polokus)
        {
            _polokus = polokus;
        }

        public IEnumerable<string> GetActiveNodesIds(string wfId, string piId)
        {
            var results = new List<string>();

            var pi = GetProcessInstance(wfId, piId);

            results.AddRange(
                pi.ChildrenProcessInstances.SelectMany(x => GetActiveNodesIds(wfId, x.Id)));

            results.AddRange(pi.ActiveTasksManager.GetNodeHandlers().Select(x => x.Node.Id));
            results.AddRange(pi.Waiters.Select(x => x.NodeToCall.Id));

            return results;
        }

        public IEnumerable<string> GetAllNodesIds(string wfId, string piId)
        {
            return GetProcessInstance(wfId, piId).BpmnProcess.GetNodesIds();
        }

        public string GetNodeName(string wfId, string piId, string nodeId)
        {
            return GetProcessInstance(wfId, piId).BpmnProcess.GetNodeById(nodeId).Name;
        }

        public Type GetNodeXmlType(string wfId, string piId, string nodeId)
        {
            return GetProcessInstance(wfId, piId).BpmnProcess.GetNodeById(nodeId).XmlType;
        }

        public string GetTotalTime(string wfId, string piId)
        {
            return GetProcessInstance(wfId, piId).StatusManager.TotalTime.ToString(@"hh\:mm\:ss\.ff");
        }

        public void SetUserDecisionForUserTaskNH(string wfId, string piId, string nodeId, string answer)
        {
            var pi = GetProcessInstance(wfId, piId);
            var node = pi.BpmnProcess.GetNodeById(nodeId);
            var nh = pi.GetNodeHandlerForNodeIfExists(node);

            if (nh is UserTaskNodeHandler userTaskNh)
            {
                userTaskNh.SetUserDecision(answer);
            }
        }

        public void RemoveAwaitingToken(string wfId, string piId, string token)
        {
            var pi = GetProcessInstance(wfId, piId);
            pi.RemoveAwaitingToken(token);
        }

        private ProcessInstance GetProcessInstance(string wfId, string piId)
        {
            return (ProcessInstance)_polokus.GetWorkflow(wfId).GetProcessInstanceById(piId);
        }
    }
}
