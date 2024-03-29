﻿using Polokus.Core.BpmnModels;
using Polokus.Core.Execution.NodeHandlers.Abstract;
using Polokus.Core.Interfaces.Xsd;

namespace Polokus.Core.Execution.NodeHandlers
{
    public class StartEventHandler : NodeHandler<tStartEvent>
    {
        public StartEventHandler(ProcessInstance processInstance, FlowNode<tStartEvent> typedNode)
            : base(processInstance, typedNode)
        {
        }
    }
}
