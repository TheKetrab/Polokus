using Polokus.Core.BpmnModels;
using Polokus.Core.Helpers;
using Polokus.Core.Interfaces.Execution.NodeHandlers;
using Polokus.Core.Interfaces.Managers;
using Polokus.Core.Interfaces.Xsd;

namespace Polokus.Core.Execution.NodeHandlers.Abstract
{
    public abstract class NodeHandler<T> : INodeHandler
        where T : tFlowNode
    {
        public IProcessInstance ProcessInstance { get; set; }
        public IWorkflow Workflow => ProcessInstance.Workflow;
        public IPolokusMaster Master => Workflow.PolokusMaster;
        public FlowNode<T> TypedNode { get; }
        public IFlowNode Node => TypedNode;
        public CancellationTokenSource CancellationTokenSource { get; set; }
        public bool IsJoining => Node.Incoming.Count > 1;
        public IScriptProvider ScriptProvider => ProcessInstance.ScriptProvider;

        private Dictionary<BoundaryEventType, INodeHandlerWaiter>? _boundaryEventsWaiters;


        public NodeHandler(IProcessInstance processInstance, FlowNode<T> typedNode)
        {
            ProcessInstance = processInstance;
            TypedNode = typedNode;
            CancellationTokenSource = new CancellationTokenSource();
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
        public virtual async Task<IProcessResultInfo> Execute(INodeCaller? caller, int taskId)
        {
            try
            {
                CancellationTokenSource.Token.ThrowIfCancellationRequested();
                bool canProcess = await CanProcess(caller);
                if (!canProcess)
                {
                    return new ProcessResultInfo(ProcessResultState.Suspension);
                }

                CancellationTokenSource.Token.ThrowIfCancellationRequested();
                AddWaitersForBoundaryEvents();
                var resultInfo = await Process(caller);
                CancellationTokenSource.Token.ThrowIfCancellationRequested();
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

        private void AddWaitersForBoundaryEvents()
        {
            if (this.Node.BoundaryEvents.Count > 0)
            {
                _boundaryEventsWaiters = new Dictionary<BoundaryEventType, INodeHandlerWaiter>();

                foreach (var be in this.Node.BoundaryEvents)
                {
                    if (be.Type == BoundaryEventType.Error)
                    {
                        continue;
                    }

                    Action continuation = () =>
                    {
                        this.ProcessInstance.ActiveTasksManager.CancellNodeHandler(this);
                    };

                    INodeHandlerWaiter? waiter = null;
                    var manager = GetManagerByType(this.ProcessInstance.Workflow, be.Type);
                    waiter = manager.RegisterWaiter(ProcessInstance, be, true, continuation);
                    _boundaryEventsWaiters.Add(be.Type, waiter);
                }

            }
        }

        private void RemoveWaitersForBoundaryEvents()
        {
            if (_boundaryEventsWaiters != null)
            {
                foreach (var waiterKV in _boundaryEventsWaiters)
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

