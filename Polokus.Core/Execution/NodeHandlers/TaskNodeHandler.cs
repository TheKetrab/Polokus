﻿using Polokus.Core.BpmnModels;
using Polokus.Core.Execution.NodeHandlers.Abstract;
using Polokus.Core.Interfaces.Xsd;

namespace Polokus.Core.Execution.NodeHandlers
{
    public class TaskNodeHandler : NodeHandler<tTask>
    {
        public TaskNodeHandler(ProcessInstance processInstance, FlowNode<tTask> typedNode)
            : base(processInstance, typedNode)
        {
        }
    }
}
