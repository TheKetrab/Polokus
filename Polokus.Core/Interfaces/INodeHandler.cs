using Polokus.Core.Models.BpmnObjects.Xsd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Interfaces
{
    public interface INodeHandler
    {
        /// <summary>
        /// Handle request to be processed by some node
        /// </summary>
        /// <param name="caller">Node which invoked execution of this node handler</param>
        /// <returns>Info about state how processing finished and what to do with node</returns>
        Task<ProcessResultInfo> Execute(INodeCaller? caller, int taskId);

        /// <summary>
        /// FlowNode which is processed by this node handler
        /// </summary>
        IFlowNode Node { get; }

        IProcessInstance ProcessInstance { get; }

    }
}
