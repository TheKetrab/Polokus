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
                if (caller != null)
                {
                    invokedBy.Add(caller.Id);
                }

                bool everybodyInvoked = !ProcessInstance
                    .ExistsAnotherTaskAbleToCallTarget(this.Node, invokedBy);

                return Task.FromResult(everybodyInvoked);
            }
        }
    }
}
