using Polokus.Core.Interfaces.Extensibility;

namespace Polokus.Core.Interfaces.Communication
{
    /// <summary>
    /// Service that allows remote control of Polokus Master object.
    /// </summary>
    public interface IPolokusService
    {
        /// <summary>
        /// This method loads content of bpmn file (given as string <paramref name="str"/>),
        /// parses it and creates new Workflow with name <paramref name="wfName"/>. 
        /// </summary>
        /// <param name="str">Bpmn XML given as string.</param>
        /// <param name="wfName">Name of Workflow to create.</param>
        public void LoadXmlString(string str, string wfName);

        /// <summary>
        /// Returns Ids of all registered workflows.
        /// </summary>
        public IEnumerable<string> GetWorkflowsIds();

        /// <summary>
        /// Registers hooks provider object.
        /// </summary>
        /// <param name="hooksProvider">Hooks provider to be registered.</param>
        public void RegisterHooksProvider(IHooksProvider hooksProvider);

        /// <summary>
        /// Deregisters hooks provider object.
        /// </summary>
        /// <param name="hooksProvider">Hooks provider to be deregistered.</param>
        public void DeregisterHooksProvider(IHooksProvider hooksProvider);

        /// <summary>
        /// Sends info to master, that application is connected.
        /// </summary>
        public void SetClientConnected();
    }
}
