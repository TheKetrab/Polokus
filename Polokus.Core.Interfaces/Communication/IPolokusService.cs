using Polokus.Core.Interfaces.Extensibility;

namespace Polokus.Core.Interfaces.Communication
{
    public interface IPolokusService
    {
        public void LoadXmlString(string str, string wfName);
        public IEnumerable<string> GetWorkflowsIds();
        public void RegisterHooksProvider(IHooksProvider hooksProvider);
        public void DeregisterHooksProvider(IHooksProvider hooksProvider);
        public void SetClientConnected();
    }
}
