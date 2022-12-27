using Polokus.Core.Interfaces;
using Polokus.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Remote
{
    public class RemotePolokusService : IPolokusService
    {
        public void LoadXmlString(string str, string wfName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetWorkflowsIds()
        {
            throw new NotImplementedException();
        }

        public void RegisterHooksProvider(IHooksProvider hooksProvider)
        {
            throw new NotImplementedException();
        }

        public void DeregisterHooksProvider(IHooksProvider hooksProvider)
        {
            throw new NotImplementedException();
        }
    }
}
