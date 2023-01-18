using Polokus.Core.Execution;
using Polokus.Core.Interfaces.Xsd;
using Polokus.Core.Models;
using Polokus.Core.NodeHandlers.Abstract;

namespace Polokus.Core.NodeHandlers
{
    public class TaskNodeHandler : NodeHandler<tTask>
    {
        public TaskNodeHandler(ProcessInstance processInstance, FlowNode<tTask> typedNode)
            : base(processInstance, typedNode)
        {
        }
    }
}
