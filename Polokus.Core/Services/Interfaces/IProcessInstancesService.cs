using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Services.Interfaces
{
    public interface IProcessInstancesService
    {
        public IEnumerable<string> GetActiveNodesIds(string wfId, string piId);
        public IEnumerable<string> GetAllNodesIds(string wfId, string piId);
        public string GetNodeName(string wfId, string piId, string nodeId);
        public Type GetNodeXmlType(string wfId, string piId, string nodeId);
        public string GetTotalTime(string wfId, string piId);
        public void SetUserDecisionForUserTaskNH(string wfId, string piId, string nodeId, string answer);
        public void RemoveAwaitingToken(string wfId, string piId, string token);
    }
}
