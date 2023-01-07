using Polokus.Core.Execution;
using Polokus.Core.Helpers;
using Polokus.Core.Interfaces;
using Polokus.Core.Models;
using Polokus.Core.Models.BpmnObjects.Xsd;
using Polokus.Core.NodeHandlers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


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

        public override async Task<bool> CanProcess(INodeCaller? caller)
        {
            if (caller is NodeHandlerWaiter w)
            {
                return true;
            }

            this.ProcessInstance.Workflow.TimeManager
                .RegisterWaiter(ProcessInstance, Node, true);
            
            return false;
        }

    }
}
