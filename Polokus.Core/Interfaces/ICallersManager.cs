using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Interfaces
{
    public interface ICallersManager
    {
        /// <summary>
        /// Workflow of this manager.
        /// </summary>
        IWorkflow Workflow { get; }

        /// <summary>
        /// This method gets all starters registered for this manager.
        /// </summary>
        IEnumerable<IProcessStarter> GetStarters();

        /// <summary>
        /// This method gets all waiters registered for this manager.
        /// </summary>
        IEnumerable<INodeHandlerWaiter> GetWaiters();

        /// <summary>
        /// This method gets all waiters for concrete process instance.
        /// </summary>
        /// <param name="piId">Id of process instance.</param>
        IEnumerable<INodeHandlerWaiter> GetWaiters(string piId);

        /// <summary>
        /// This method returns true iif. exists any waiter or starter.
        /// </summary>
        bool IsAnyWaiting();

        /// <summary>
        /// This method registers waiter for concrete node and returns created one.
        /// </summary>
        /// <param name="pi">Process instance for this waiter.</param>
        /// <param name="node">Node to register a waiter for.</param>
        /// <param name="oneTime">Should it be cyclic or not?</param>
        /// <param name="continuation">Possible continuation to invoke after node was invoked by waiter. For cleanups.</param>
        INodeHandlerWaiter RegisterWaiter(IProcessInstance pi, IFlowNode node, bool oneTime, Action? continuation = null);

        /// <summary>
        /// This method registers starter for concrete node and returns created one.
        /// </summary>
        /// <param name="bpmnProcess">Definition of BPMN process.</param>
        /// <param name="startNode">Node to register a starter for.</param>
        IProcessStarter RegisterStarter(IBpmnProcess bpmnProcess, IFlowNode startNode);

        /// <summary>
        /// This method cancels waiter and removes it from the collection.
        /// </summary>
        /// <param name="waiterId">Id of waiter.</param>
        void RemoveWaiter(string waiterId);

        /// <summary>
        /// This method returns true iff waiter is removed (cancelled) or not exists.
        /// </summary>
        /// <param name="waiterId">Id of waiter.</param>
        bool IsWaiterCancelled(string waiterId);
    }
}
