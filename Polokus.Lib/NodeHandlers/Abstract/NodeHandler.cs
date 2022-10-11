using Polokus.Lib.Hooks;
using Polokus.Lib.Models;
using Polokus.Lib.Models.BpmnObjects.Xsd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.NodeHandlers.Abstract
{
    public abstract class NodeHandler<T> : INodeHandler
        where T : tFlowNode
    {
        public ScriptProvider ScriptProvider => Node.Process.Context?.ScriptProvider ?? new ScriptProvider();
        public IFlowNode Node => TypedNode;
        public FlowNode<T> TypedNode { get; }
        public bool IsJoining => Node.Incoming.Count > 1;
        public ProcessInstance? ProcessInstance { get; set; }

        public NodeHandler(FlowNode<T> typedNode)
        {
            TypedNode = typedNode;
        }

        /// <summary>
        /// Action that is invoked during processing the node.
        /// </summary>
        protected virtual async Task Action(IFlowNode? caller)
        {
            await Task.CompletedTask;
        }

        /// <summary>
        /// Process should do an action and returns state (success/failure) and return sequences that should be invoked later.
        /// </summary>
        protected virtual async Task<ProcessResultInfo> Process(IFlowNode? caller)
        {
            await Action(caller);
            return new ProcessResultInfo(ProcessResultState.Success, Node.Outgoing);
        }

        /// <summary>
        /// Function determines if everything is done before start processing.
        /// If it returns false, execution should stop as 'Suspension'.
        /// Main goal is to save state of nodehandler even if it is not yet ready to process action.
        /// </summary>
        protected virtual async Task<bool> CanProcess(IFlowNode? caller)
        {
            return await Task.FromResult(true);
        }

        /// <summary>
        /// Main execution of nodehandler. It provides mechanism to handle exceptions.
        /// Note that JUST BEFORE real execution (after 'can process') active task manager switch worker for concrete taskId.
        /// </summary>
        public async Task<ProcessResultInfo> Execute(IFlowNode? caller, int taskId)
        {
            try
            {
                bool canProcess = await CanProcess(caller);
                if (!canProcess)
                {
                    return new ProcessResultInfo(ProcessResultState.Suspension);
                }

                this.ProcessInstance.ActiveTasksManager.ActiveTasks[taskId] = this;

                var resultInfo = await Process(caller);
                return resultInfo;
            }
            catch (Exception exc)
            {
                Logger.LogError(exc.Message);
                return new ProcessResultInfo(ProcessResultState.Failure, exc.Message);
            }
        }

    }
}
