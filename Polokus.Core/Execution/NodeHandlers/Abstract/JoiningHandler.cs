using Polokus.Core.BpmnModels;
using Polokus.Core.Interfaces.Xsd;

namespace Polokus.Core.Execution.NodeHandlers.Abstract
{
    /// <summary>
    /// Joining Node Handler is handler that provides mechanism to handle situation
    /// that more than one sequence is comming into the flow node.
    /// </summary>
    public abstract class JoiningHandler<T> : NodeHandler<T> where T : tFlowNode
    {
        private object _mutex = new object();
        private bool _everybodyInvokedOneTime = false;
        private bool _firstTime = true;

        protected List<string> invokedBy = new();

        protected JoiningHandler(ProcessInstance processInstance, FlowNode<T> typedNode)
            : base(processInstance, typedNode)
        {
        }

        public override Task<bool> CanProcess(INodeCaller? caller)
        {
            if (!IsJoining)
            {
                return Task.FromResult(true);
            }

            lock (_mutex)
            {
                if (_firstTime)
                {
                    // on first visit create a cycle time waiter that will try to invoke this gate
                    // (because maybe this gate will never be reached by all sequences!)
                    _firstTime = false;
                    var waiter = new NodeHandlerWaiter(ProcessInstance, Node);
                    Workflow.TimeManager.RegisterWaiterNotCrone("1s", waiter, false, () =>
                    {
                        if (_everybodyInvokedOneTime)
                        {
                            Workflow.TimeManager.RemoveWaiter(waiter.Id);
                        }
                    });
                }

                if (caller != null)
                {
                    invokedBy.Add(caller.Id);
                }

                bool everybodyInvoked = !ProcessInstance
                    .ExistsAnotherTaskAbleToCallTarget(this.Node, invokedBy);

                if (_everybodyInvokedOneTime)
                {
                    // cancel this execution of nodehandler, because it comes
                    // from separate thread and was already done by other thread
                    CancellationTokenSource.Cancel();
                    return Task.FromResult(true);
                }

                if (everybodyInvoked)
                {
                    _everybodyInvokedOneTime = true;
                }

                return Task.FromResult(everybodyInvoked);
            }
        }
    }
}
