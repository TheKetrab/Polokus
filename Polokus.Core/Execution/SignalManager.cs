using Polokus.Core.Hooks;
using Polokus.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Execution
{
    public class SignalManager : ISignalManager
    {
        private Dictionary<string, IProcessStarter> _starters = new();
        private Dictionary<string, INodeHandlerWaiter> _waiters = new();

        public IWorkflow Workflow { get; }

        public SignalManager(IWorkflow workflow)
        {
            Workflow = workflow;
        }

        public void EmitSignal(string signal)
        {
            Workflow.PolokusMaster.EmitSignal(Workflow, signal);
        }

        public IEnumerable<IProcessStarter> GetStarters()
        {
            return _starters.Values;
        }

        public IEnumerable<INodeHandlerWaiter> GetWaiters()
        {
            return _waiters.Values;
        }

        public bool IsAnyWaiting()
        {
            return _waiters.Any() || _starters.Any();
        }

        public bool IsWaiting(string listenerId)
        {
            return _waiters.Where(x => string.Equals(listenerId, x.Key)).Any()
                || _starters.Where(x => string.Equals(listenerId, x.Key)).Any();
        }

        public void RegisterSignalListener(INodeHandlerWaiter waiter)
        {
            _waiters.Add(waiter.Id, waiter);

            // one time event only
            EventHandler<string>? action = null;
            Workflow.PolokusMaster.Signal += action = (s, e) =>
            {
                Workflow.PolokusMaster.Signal -= action;

                _waiters.Remove(waiter.Id);
                waiter.HooksProvider?.OnCallerChanged(waiter.Id, CallerChangedType.WaiterRemoved);
                waiter.Invoke();
            };

        }

        public void RegisterSignalListener(IProcessStarter starter)
        {
            _starters.Add(starter.Id, starter);
            Workflow.PolokusMaster.Signal += (s, e) =>
            {
                if (e == starter.StartNode.Name)
                {
                    starter.Workflow.StartProcessInstance(starter.BpmnProcess, starter.StartNode, null);
                }
            };
        }
    }
}
