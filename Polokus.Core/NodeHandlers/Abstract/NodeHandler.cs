using Polokus.Core.Hooks;
using Polokus.Core.Interfaces;
using Polokus.Core.Models;
using Polokus.Core.Models.BpmnObjects.Xsd;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Polokus.Core.Execution;
using Polokus.Core.Helpers;

namespace Polokus.Core.NodeHandlers.Abstract
{
    public abstract class NodeHandler<T> : INodeHandler
        where T : tFlowNode
    {
        public IProcessInstance ProcessInstance { get; set; }
        public FlowNode<T> TypedNode { get; }
        public IFlowNode Node => TypedNode;
        public CancellationToken CancellationToken { get; set; }
        public bool IsJoining => Node.Incoming.Count > 1;
        public IScriptProvider ScriptProvider => ProcessInstance.Workflow.ScriptProvider;

        private Dictionary<BoundaryEventType, INodeHandlerWaiter> boundaryEventsWaiters;


        public NodeHandler(IProcessInstance processInstance, FlowNode<T> typedNode)
        {
            ProcessInstance = processInstance;
            TypedNode = typedNode;
        }

        /// <summary>
        /// Action that is invoked during processing the node.
        /// </summary>
        public virtual async Task Action(INodeCaller? caller)
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
                CancellationToken.ThrowIfCancellationRequested();
                bool canProcess = await CanProcess(caller);
                if (!canProcess)
                {
                    return new ProcessResultInfo(ProcessResultState.Suspension);
                }

                CancellationToken.ThrowIfCancellationRequested();
                AddWaitersForBoundaryEvents();
                var resultInfo = await Process(caller);
                CancellationToken.ThrowIfCancellationRequested();
                RemoveWaitersForBoundaryEvents();

                return resultInfo;
            }
            catch (OperationCanceledException)
            {
                RemoveWaitersForBoundaryEvents();
                return new ProcessResultInfo(ProcessResultState.Cancellation);
            }
            catch (Exception exc)
            {
                var boundaryEvtHandler = this.Node.BoundaryEvents
                    .FirstOrDefault(x => x.Type == BoundaryEventType.Error);
                if (boundaryEvtHandler != null)
                {
                    Logger.Global.LogError(exc.Message);
                    this.ProcessInstance.ActiveTasksManager.CancellNodeHandler(this);
                    var sequence = Sequence.CreateFakeSequence(boundaryEvtHandler);
                    return new SuccessProcessResultInfo(new Sequence[] { sequence }, "forceAllNewSequences");
                }

                Logger.Global.LogError(exc.Message);
                return new ProcessResultInfo(ProcessResultState.Failure, exc.Message);
            }
        }

        public virtual INodeHandler Clone()
        {
            INodeHandler copy = (INodeHandler)this.MemberwiseClone();
            copy.CancellationToken = new CancellationToken();

            return copy;
        }

        private void AddWaitersForBoundaryEvents()
        {
            if (this.Node.BoundaryEvents.Count > 0)
            {
                boundaryEventsWaiters = new Dictionary<BoundaryEventType,INodeHandlerWaiter>();

                foreach (var be in this.Node.BoundaryEvents)
                {
                    if (be.Type == BoundaryEventType.Error)
                    {
                        continue;
                    }

                    Action continuation = () => this.ProcessInstance.ActiveTasksManager.CancellNodeHandler(this);

                    INodeHandlerWaiter? waiter = null;
                    var manager = GetManagerByType(this.ProcessInstance.Workflow, be.Type);
                    waiter = manager.RegisterWaiter(ProcessInstance, be, true, continuation);
                    boundaryEventsWaiters.Add(be.Type, waiter);
                }

            }
        }

        private void RemoveWaitersForBoundaryEvents()
        {
            if (boundaryEventsWaiters != null)
            {
                foreach (var waiterKV in boundaryEventsWaiters)
                {
                    var manager = GetManagerByType(this.ProcessInstance.Workflow, waiterKV.Key);
                    manager.RemoveWaiter(waiterKV.Value.Id);
                }
            }
        }

        private ICallersManager GetManagerByType(IWorkflow workflow, BoundaryEventType beType)
        {
            switch (beType)
            {
                case BoundaryEventType.Timer:
                    return workflow.TimeManager;
                case BoundaryEventType.Message:
                    return workflow.MessageManager;
                case BoundaryEventType.Signal:
                    return workflow.SignalManager;
                default:
                    throw new Exception("Not defined manager for this type.");
            };
        }
    }
}

