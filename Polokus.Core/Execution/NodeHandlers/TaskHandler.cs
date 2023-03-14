using Polokus.Core.BpmnModels;
using Polokus.Core.Execution.NodeHandlers.Abstract;
using Polokus.Core.Interfaces.Xsd;

namespace Polokus.Core.Execution.NodeHandlers
{
    public class TaskHandler : NodeHandler<tTask>
    {
        public TaskHandler(ProcessInstance processInstance, FlowNode<tTask> typedNode)
            : base(processInstance, typedNode)
        {
        }
    }
}
