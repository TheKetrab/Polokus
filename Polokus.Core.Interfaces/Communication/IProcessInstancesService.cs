namespace Polokus.Core.Interfaces.Communication
{
    /// <summary>
    /// Service that allows remote control of process instance.
    /// </summary>
    public interface IProcessInstancesService
    {
        /// <summary>
        /// Returns Ids of all active nodes (nodes that process instance is currently in).
        /// </summary>
        /// <param name="wfId">Workflow Id.</param>
        /// <param name="piId">Process instance Id.</param>
        public IEnumerable<string> GetActiveNodesIds(string wfId, string piId);

        /// <summary>
        /// Returns Ids of all nodes in the process.
        /// </summary>
        /// <param name="wfId">Workflow Id.</param>
        /// <param name="piId">Process instance Id.</param>
        public IEnumerable<string> GetAllNodesIds(string wfId, string piId);

        /// <summary>
        /// Returns name of given node.
        /// </summary>
        /// <param name="wfId">Workflow Id.</param>
        /// <param name="piId">Process instance id.</param>
        /// <param name="nodeId">FlowNode Id.</param>
        public string GetNodeName(string wfId, string piId, string nodeId);

        /// <summary>
        /// Returns XML type (from bpmn schema, derived from tFlowNode) of given node.
        /// </summary>
        /// <param name="wfId">Workflow Id.</param>
        /// <param name="piId">Process instance id.</param>
        /// <param name="nodeId">FlowNode Id.</param>
        public Type GetNodeXmlType(string wfId, string piId, string nodeId);

        /// <summary>
        /// Returns total execution time of process instance.
        /// </summary>
        /// <param name="wfId">Workflow Id.</param>
        /// <param name="piId">Process instance Id.</param>
        public string GetTotalTime(string wfId, string piId);

        /// <summary>
        /// User Task Handler stores field '_userDecision'. This method sets the decision from param <paramref name="answer"/>.
        /// </summary>
        /// <param name="wfId">Workflow Id.</param>
        /// <param name="piId">Process instance Id.</param>
        /// <param name="nodeId">FlowNode Id (handled by concrete NodeHandler).</param>
        /// <param name="answer">User answer (the decision).</param>
        public void SetUserDecisionForUserTaskNH(string wfId, string piId, string nodeId, string answer);
        
        /// <summary>
        /// This method sends the request to remove awaiting token, that NodeHandler can continue execution.
        /// </summary>
        /// <param name="wfId">Workflow Id.</param>
        /// <param name="piId">Process instance Id.</param>
        /// <param name="token">Id of token to remove.</param>
        public void RemoveAwaitingToken(string wfId, string piId, string token);
    }
}
