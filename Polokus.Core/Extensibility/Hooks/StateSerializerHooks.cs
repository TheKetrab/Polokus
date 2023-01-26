using Polokus.Core.Interfaces.Extensibility;

namespace Polokus.Core.Extensibility.Hooks
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
