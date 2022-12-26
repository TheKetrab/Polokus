using Polokus.Core.Models.BpmnObjects.Xsd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Remote
{
    public class RemoteProcessInstancesService
    {
        public IEnumerable<string> GetActiveNodesIds(string wfId, string piId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetAllNodesIds(string wfId, string piId)
        {
            throw new NotImplementedException();
        }

        public string GetNodeName(string wfId, string piId, string nodeId)
        {
            throw new NotImplementedException();
        }

        public Type GetNodeXmlType(string wfId, string piId, string nodeId)
        {
            throw new NotImplementedException();
        }

        public string GetTotalTime(string wfId, string piId)
        {
            //            string time = instance.StatusManager.TotalTime.ToString(@"hh\:mm\:ss\.ff");
            throw new NotImplementedException();
        }

        public string SetUserDecisionForUserTaskNH(string wfId, string piId, string nodeId, string answer)
        {
            throw new NotImplementedException();
        }

    }
}
