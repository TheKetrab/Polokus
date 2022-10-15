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

        protected override Task<bool> CanProcess(IFlowNode? caller)
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

                bool everybodyInvoked = !ProcessInstance?.ExistsAnotherTaskAbleToCallTarget(this.Node, invokedBy) ?? false;

                if (everybodyInvoked)
                {
                    return Task.FromResult(true);
                }
                else
                {
                    return Task.FromResult(false);
                }

            }


        }
    }
}
