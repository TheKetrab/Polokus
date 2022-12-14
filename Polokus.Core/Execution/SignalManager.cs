using Polokus.Core.Hooks;
using Polokus.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Execution
{
    public class SignalManager : BaseCallersManager, ISignalManager
    {
        public override IWorkflow Workflow { get; }

        public SignalManager(IWorkflow workflow)
        {
            Workflow = workflow;
        }

        public void EmitSignal(string signal, string? parameters = null)
        {
            Workflow.PolokusMaster.EmitSignal(Workflow, signal, parameters);
        }

        private void RegisterSignalListener(INodeHandlerWaiter waiter, bool oneTime, Action? continuation = null)
        {
            AddWaiter(waiter.Id, waiter, continuation);

            // one time event only | TODO: make it not ignoring one time
            EventHandler<Signal>? action = null;
            Workflow.PolokusMaster.Signal += action = (s, e) =>
            {
                if (e.Name == waiter.NodeToCall.Name)
                {
                    Workflow.PolokusMaster.Signal -= action;

                    // TODO: passing parameters to signal?

                    if (!IsWaiterCancelled(waiter.Id))
                    {
                        waiter.Invoke();
                        continuation?.Invoke();

                        RemoveWaiter(waiter.Id);
                        waiter.HooksProvider?.OnCallerChanged(waiter.Id, nameof(CallerChangedType.WaiterRemoved));
                    }
                }
            };

        }

        private void RegisterSignalListener(IProcessStarter starter)
        {
            AddStarter(starter.Id, starter);
            Workflow.PolokusMaster.Signal += (s, e) =>
            {
                if (e.Name == starter.StartNode.Name)
                {
                    // TODO: passing parameters to signal?
                    starter.Workflow.StartProcessInstance(starter.BpmnProcess, starter.StartNode, null);
                }
            };
        }

        public override INodeHandlerWaiter RegisterWaiter(IProcessInstance pi, IFlowNode node, bool oneTime, Action? continuation = null)
        {
            var waiter = new NodeHandlerWaiter(pi, node);
            RegisterSignalListener(waiter, oneTime, continuation);
            return waiter;
        }

        public override IProcessStarter RegisterStarter(IBpmnProcess bpmnProcess, IFlowNode startNode)
        {
            var starter = new ProcessStarter(Workflow, bpmnProcess, startNode);
            RegisterSignalListener(starter);
            return starter;
        }
    }
}
