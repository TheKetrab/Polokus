using Polokus.Core.Execution;
using Polokus.Core.Extensibility.Hooks;
using Polokus.Core.Interfaces.Managers;

namespace Polokus.Core.Managers
{
    public class SignalManager : BaseCallersManager, ISignalManager
    {
        public override IWorkflow Workflow { get; }

        public SignalManager(IWorkflow workflow)
        {
            Workflow = workflow;
        }

        public void EmitSignal(string signal, params string[] parameters)
        {
            Workflow.PolokusMaster.EmitSignal(Workflow, signal, parameters);
        }

        private void RegisterSignalListener(INodeHandlerWaiter waiter, bool oneTime, Action? continuation = null)
        {
            AddWaiter(waiter.Id, waiter, continuation);

            // one time event only | TODO: make it not ignoring one time
            EventHandler<ISignal>? action = null;

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
                    var pi = starter.Workflow.CreateProcessInstance(starter.BpmnProcess);
                    if (e.Params != null)
                    {
                        int i = 0;
                        foreach (var p in e.Params)
                        {
                            pi.ScriptProvider.Globals.SetValue($"arg{i++}", p);
                        }
                    }

                    starter.Workflow.StartProcessInstance(pi, starter.StartNode);
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
