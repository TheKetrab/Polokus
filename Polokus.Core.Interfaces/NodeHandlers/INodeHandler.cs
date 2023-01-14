using Polokus.Core.Interfaces.BpmnModels;
using Polokus.Core.Interfaces.Execution;

namespace Polokus.Core.Interfaces.NodeHandlers
{
    /// <summary>
    /// NodeHandler is an object that provides methods to invoke during processing concrete FlowNode.
    /// </summary>
    public interface INodeHandler
    {
        /// <summary>
        /// Handle request to be processed by some node. Returns info about
        /// state how processing finished and what to do with node.
        /// </summary>
        /// <param name="caller">Node which invoked execution of this node handler.</param>
        Task<IProcessResultInfo> Execute(INodeCaller? caller, int taskId);

        /// <summary>
        /// FlowNode which is processed by this node handler
        /// </summary>
        IFlowNode Node { get; }

        IProcessInstance ProcessInstance { get; }
        IWorkflow Workflow { get; }
        IPolokusMaster Master { get; }

        CancellationToken CancellationToken { get; set; }

        /// <summary>
        /// Creates copy of object. It is necessary because of CancellationTokens
        /// </summary>
        INodeHandler Clone();

    }
}
