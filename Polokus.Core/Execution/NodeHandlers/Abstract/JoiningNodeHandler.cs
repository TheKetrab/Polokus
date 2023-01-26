using Polokus.Core.Execution;
using Polokus.Core.Interfaces;
using Polokus.Core.Interfaces.Xsd;
using Polokus.Core.BpmnModels;
using Polokus.Core.Interfaces.Execution;

namespace Polokus.Core.Execution.NodeHandlers.Abstract
{
    /// <summary>
    /// Joining Node Handler is handler that provides mechanism to handle situation
    /// that more than one sequence is comming into the flow node.
    /// </summary>
    public abstract class JoiningNodeHandler<T> : NodeHandler<T> where T : tFlowNode
    {
        private object _mutex = new object();

        protected List<string> invokedBy = new();

        protected JoiningNodeHandler(ProcessInstance processInstance, FlowNode<T> typedNode)
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

                bool everybodyInvoked = ! ProcessInstance
                    .ExistsAnotherTaskAbleToCallTarget(this.Node, invokedBy);

                return Task.FromResult(everybodyInvoked);
            }
        }
    }
}
