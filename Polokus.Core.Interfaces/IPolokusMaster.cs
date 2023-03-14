using Polokus.Core.Interfaces.BpmnModels;
using Polokus.Core.Interfaces.Communication;
using Polokus.Core.Interfaces.Execution;
using Polokus.Core.Interfaces.Extensibility;
using Polokus.Core.Interfaces.Managers;

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
        /// Object that provides dynamic injection of many hooks and invokes all of them.
        /// </summary>
        IHooksManager HooksManager { get; set; }

        /// <summary>
        /// An object to manage client connection. Monitors if client is connected to server via GUI App.
        /// </summary>
        IConnectionManager ConnectionManager { get; }

        /// <summary>
        /// List of objects that monitors some actions that raise events.
        /// </summary>
        ICollection<IMonitor> Monitors { get; }

        /// <summary>
        /// Parses BPMN XML string and adds workflow to collecion.
        /// </summary>
        void LoadXmlString(string xmlString, string bpmnWorkflowName);

        /// <summary>
        /// Signal that can be emited.
        /// </summary>
        event EventHandler<ISignal>? Signal;

        /// <summary>
        /// This method invokes signal event with given parameters.
        /// </summary>
        /// <param name="signal">Name of signal.</param>
        /// <param name="parameters">Parameters of signal.</param>
        void EmitSignal(object? sender, string signal, params string[] parameters);

        /// <summary>
        /// Logs an info.
        /// </summary>
        /// <param name="piId">Workflow id.</param>
        /// <param name="piId">Process instance id.</param>
        /// <param name="info">Message to log.</param>
        /// <param name="type">Importance of message.</param>
        void Log(string wfId, string piId, string info, MsgType type);

        /// <summary>
        /// Logs an info.
        /// </summary>
        /// <param name="globalPiId">Global process instane id (wfId/piId).</param>
        /// <param name="info">Message to log.</param>
        /// <param name="type">Importance of message.</param>
        void Log(string globalPiId, string info, MsgType type);

        /// <summary>
        /// This property returns true iff Polokus.App client is connected.
        /// </summary>
        bool ClientConnected { get; }

        /// <summary>
        /// Returns instance of FlowNode searched by id.
        /// </summary>
        /// <param name="wfId">Id of workflow.</param>
        /// <param name="piId">Id of process instance.</param>
        /// <param name="nodeId">Id of node.</param>
        IFlowNode? GetFlowNode(string wfId, string piId, string nodeId);

    }
}
