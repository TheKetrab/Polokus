using Polokus.Core.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polokus.Core.Interfaces;
using System.Text.Json;
using Polokus.Core.Interfaces.Execution;

namespace Polokus.Core.Hooks
{
    public class StateSerializerHooks : EmptyHooksProvider
    {
        private PolokusMaster _master;

        public StateSerializerHooks(PolokusMaster master)
        {
            _master = master;
        }

        public override void BeforeExecuteNode(string wfId, string piId, string nodeId, int taskId, string? nodeCallerId)
        {
            _master.StateSerializerManager.SerializeState(wfId, piId);
        }

        public override void OnProcessFinished(string wfId, string piId, string result)
        {
            _master.StateSerializerManager.ClearStateFor(wfId, piId);
        }
    }
}
