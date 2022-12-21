using Polokus.Core.Externals;
using Polokus.Monitors.FileMonitor;

namespace Polokus.Core.Interfaces
{
    /// <summary>
    /// PolokusMaster is an object that manages all loaded XML definitions.
    /// </summary>
    public interface IPolokusMaster
    {
        /// <summary>
        /// Collection of all loaded Workflow instances.
        /// </summary>
        IEnumerable<IWorkflow> GetWorkflows();

        /// <summary>
        /// Method returns workflow by their id.
        /// </summary>
        /// <param name="id">Id (name) of workflow.</param>
        IWorkflow GetWorkflow(string id);

        /// <summary>
        /// Method adds workflow to collection and initalizes everything what it needs (eg. starters).
        /// </summary>
        /// <param name="id"></param>
        /// <param name="workflow"></param>
        void AddWorkflow(string id, IWorkflow workflow);


        /// <summary>
        /// Object that provides dynami injection of settings.
        /// </summary>
        ISettingsProvider SettingsProvider { get; set; }

        /// <summary>
        /// Object that provides dynamic injection of many hooks and invokes all of them.
        /// </summary>
        IHooksManager? HooksManager { get; set; }

        /// <summary>
        /// Object that reprezents externals and manages them. Can be null if externals.json not found.
        /// </summary>
        Externals.Externals? Externals { get; }

        /// <summary>
        /// List of objects that monitors some directories.
        /// </summary>
        ICollection<FileMonitor> FileMonitors { get; }

        /// <summary>
        /// Parses BPMN XML string and adds workflow to collecion.
        /// </summary>
        public void LoadXmlString(string xmlString, string bpmnWorkflowName);

    }
}
