namespace Polokus.Core.Interfaces
{
    /// <summary>
    /// BpmnProcess is an object that represents single definition of process or subprocess.
    /// </summary>
    public interface IBpmnProcess
    {
        /// <summary>
        /// Id of BpmnProcess stored in XML.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// BpmnContext of this process - parsed representation of XML file.
        /// </summary>
        IBpmnContext BpmnContext { get; }

        /// <summary>
        /// This method gets node of given <paramref name="id"/>. Returns null if node not found.
        /// </summary>
        /// <param name="id">Id of node.</param>
        IFlowNode? GetNodeById(string id);

        /// <summary>
        /// This method gets sequence of given <paramref name="id">. Returns null if not found.
        /// </summary>
        /// <param name="id">Id of node.</param>
        ISequence? GetSequenceById(string id);

        /// <summary>
        /// This method sets nodes collection for BPMN process.
        /// </summary>
        void SetNodes(IEnumerable<IFlowNode> nodes);

        /// <summary>
        /// This method sets sequences collection for BPMN process.
        /// </summary>
        /// <param name="sequences">Sequences to set for BPMN process.</param>
        void SetSequences(IEnumerable<ISequence> sequences);

        /// <summary>
        /// This method gets collection of nodes that are tStartNode.
        /// </summary>
        IEnumerable<IFlowNode> GetStartNodes();

        /// <summary>
        /// This method returns true iif. there is a path in graph of BPMN process'
        /// nodes from <paramref name="src"/> to <paramref name="dest"/>.
        /// </summary>
        /// <param name="src">Source node.</param>
        /// <param name="dest">Destination node.</param>
        bool IsReachable(IFlowNode src, IFlowNode dest);

        /// <summary>
        /// This method gets start node which is manual. If there is none
        /// or more than one manual start node it will throw exception.
        /// </summary>
        IFlowNode GetManualStartNode();

        /// <summary>
        /// This method retrieve collection of all IDs from all nodes in BPMN process.
        /// </summary>
        IEnumerable<string> GetNodesIds();

        /// <summary>
        /// This method retrieves collection of all service tasks nodes that are used in BPMN process.
        /// </summary>
        IEnumerable<string> GetServiceTasksNames();

        /// <summary>
        /// This method is used to check if exists node with given id.
        /// </summary>
        /// <param name="id">Id of node.</param>
        bool ContainsNode(string id);

        /// <summary>
        /// This method gets node of given id as IMessageCallerNode.
        /// If node is not IMessageCallerNode, it throws exception.
        /// </summary>
        /// <param name="id">Id of node.</param>
        IMessageCallerNode GetMessageCallerNode(string id);

        /// <summary>
        /// This method gets node of given id as IMessageReceiverNode.
        /// If node is not IMessageReceiverNode, it throws exception.
        /// </summary>
        /// <param name="id">Id of node.</param>
        IMessageReceiverNode GetMessageReceiverNode(string id);
    }
}
