using Polokus.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Services.Interfaces
{
    public interface IPolokusService
    {
        public void LoadXmlString(string str, string wfName);
        public IEnumerable<string> GetWorkflowsIds();
        public void RegisterHooksProvider(IHooksProvider hooksProvider);
        public void DeregisterHooksProvider(IHooksProvider hooksProvider);
    }
}
