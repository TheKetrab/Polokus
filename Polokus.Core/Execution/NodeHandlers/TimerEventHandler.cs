using Polokus.Core.BpmnModels;
using Polokus.Core.Execution.NodeHandlers.Abstract;
using Polokus.Core.Interfaces.Xsd;

namespace Polokus.Core.Execution.NodeHandlers
{
    public class TimerEventHandler : NodeHandler<tIntermediateCatchEvent>
    {
        public string TimeDefinitions { get; set; }
        public TimerEventHandler(IProcessInstance processInstance,
            FlowNode<tIntermediateCatchEvent> typedNode, string timeDefinitions)
            : base(processInstance, typedNode)
        {
            TimeDefinitions = timeDefinitions;
        }

        public override Task<bool> CanProcess(INodeCaller? caller)
        {
            if (caller is NodeHandlerWaiter w)
            {
                return Task.FromResult(true);
            }

            Workflow.TimeManager.RegisterWaiter(ProcessInstance, Node, true);

            return Task.FromResult(false);
        }

    }
}
