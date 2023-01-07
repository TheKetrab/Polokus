using Polokus.Core.Hooks;
using Polokus.Core.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Execution
{
    public abstract class BaseCallersManager : ICallersManager
    {
        private IDictionary<string, IProcessStarter> _starters
            = new ConcurrentDictionary<string, IProcessStarter>();
        private IDictionary<string, Tuple<INodeHandlerWaiter,Action?>> _waiters
            = new ConcurrentDictionary<string, Tuple<INodeHandlerWaiter,Action?>>();

        public abstract IWorkflow Workflow { get; }

        protected void AddStarter(string starterId, IProcessStarter starter)
        {
            _starters.Add(starterId, starter);
            Workflow.HooksProvider?.OnCallerChanged(
                starterId, nameof(CallerChangedType.StarterRegistered));
        }

        protected void AddWaiter(string waiterId, INodeHandlerWaiter waiter, Action? continuation = null)
        {
            _waiters.Add(waiterId, Tuple.Create(waiter, continuation));
            Workflow.HooksProvider?.OnCallerChanged(
                waiterId, nameof(CallerChangedType.WaiterInserted));
        }

        public void RemoveWaiter(string waiterId)
        {
            CancellWaiter(waiterId);
            _waiters.Remove(waiterId);
            Workflow.HooksProvider?.OnCallerChanged(
                waiterId, nameof(CallerChangedType.WaiterRemoved));
        }

        public IEnumerable<IProcessStarter> GetStarters()
        {
            return _starters.Values;
        }

        public IEnumerable<INodeHandlerWaiter> GetWaiters()
        {
            return _waiters.Values.Select(x => x.Item1);
        }

        public IEnumerable<INodeHandlerWaiter> GetWaiters(string piId)
        {
            return _waiters.Values.Select(x => x.Item1)
                .Where(x => x.ProcessInstance.Id == piId);
        }

        public bool IsWaiterCancelled(string waiterId)
        {
            if (_waiters.ContainsKey(waiterId))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void CancellWaiter(string waiterId)
        {
            _waiters.Remove(waiterId);
        }

        public bool IsAnyWaiting()
        {
            return _waiters.Any() || _starters.Any();
        }

        public abstract INodeHandlerWaiter RegisterWaiter(IProcessInstance pi, IFlowNode node, bool oneTime, Action? continuation = null);

        public abstract IProcessStarter RegisterStarter(IBpmnProcess bpmnProcess, IFlowNode startNode);

    }
}
