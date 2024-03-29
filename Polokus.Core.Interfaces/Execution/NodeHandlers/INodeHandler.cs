﻿using Polokus.Core.Interfaces.BpmnModels;

namespace Polokus.Core.Interfaces.Execution.NodeHandlers
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

        /// <summary>
        /// Instance of process that this NodeHandler is attached to.
        /// </summary>
        IProcessInstance ProcessInstance { get; }

        /// <summary>
        /// Workflow of process that this NodeHandler is attached to.
        /// </summary>
        IWorkflow Workflow { get; }

        /// <summary>
        /// Master object of the engine.
        /// </summary>
        IPolokusMaster Master { get; }

        /// <summary>
        /// Cancellation object allows node cancelling itself.
        /// </summary>
        CancellationTokenSource CancellationTokenSource { get; set; }

    }
}
