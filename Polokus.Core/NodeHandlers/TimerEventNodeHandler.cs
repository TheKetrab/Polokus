using Polokus.Core.Execution;
using Polokus.Core.Interfaces;
using Polokus.Core.Interfaces.Xsd;
using Polokus.Core.Models;
using Polokus.Core.NodeHandlers.Abstract;


namespace Polokus.Core.NodeHandlers
{
    public class TimerEventNodeHandler : NodeHandler<tIntermediateCatchEvent>
    {
        public string TimeDefinitions { get; set; }
        public TimerEventNodeHandler(IProcessInstance processInstance,
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

            this.ProcessInstance.Workflow.TimeManager
                .RegisterWaiter(ProcessInstance, Node, true);
            
            return Task.FromResult(false);
        }

    }
}
