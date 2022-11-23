using Polokus.Core.Hooks;
using Polokus.Core.Interfaces;
using Polokus.Core.Models;
using Polokus.Core.Models.BpmnObjects.Xsd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.NodeHandlers.Abstract
{
    public abstract class NodeHandler<T> : INodeHandler
        where T : tFlowNode
    {
        public IScriptProvider ScriptProvider => ProcessInstance.ContextInstance.ScriptProvider;
        public IFlowNode Node => TypedNode;
        public FlowNode<T> TypedNode { get; }
        public bool IsJoining => Node.Incoming.Count > 1;
        public IProcessInstance ProcessInstance { get; set; }

        public CancellationToken CancellationToken { get; set; }

        public NodeHandler(IProcessInstance processInstance, FlowNode<T> typedNode)
        {
            ProcessInstance = processInstance;
            TypedNode = typedNode;
        }

        /// <summary>
        /// Action that is invoked during processing the node.
        /// </summary>
        protected virtual async Task Action(INodeCaller? caller)
        {
            await Task.CompletedTask;
        }

        /// <summary>
        /// Process should do an action and returns state (success/failure) and return sequences that should be invoked later.
        /// </summary>
        protected virtual async Task<ProcessResultInfo> Process(INodeCaller? caller)
        {
            await Action(caller);
            return new SuccessProcessResultInfo(Node.Outgoing);
        }

        /// <summary>
        /// Function determines if everything is done before start processing.
        /// If it returns false, execution should stop as 'Suspension'.
        /// Main goal is to save state of nodehandler even if it is not yet ready to process action.
        /// </summary>
        public virtual async Task<bool> CanProcess(INodeCaller? caller)
        {
            return await Task.FromResult(true);
        }

        /// <summary>
        /// Main execution of nodehandler. It provides mechanism to handle exceptions.
        /// Note that JUST BEFORE real execution (after 'can process') active task manager switch worker for concrete taskId.
        /// </summary>
        public virtual async Task<ProcessResultInfo> Execute(INodeCaller? caller, int taskId)
        {
            try
            {
                bool canProcess = await CanProcess(caller);
                if (!canProcess)
                {
                    return new ProcessResultInfo(ProcessResultState.Suspension);
                }

                CancellationToken.ThrowIfCancellationRequested();
                var resultInfo = await Process(caller);
                CancellationToken.ThrowIfCancellationRequested();
                return resultInfo;
            }
            catch (OperationCanceledException)
            {
                return new ProcessResultInfo(ProcessResultState.Cancellation);
            }
            catch (Exception exc)
            {
                Logger.Global.LogError(exc.Message);
                return new ProcessResultInfo(ProcessResultState.Failure, exc.Message);
            }
        }


    }
}
