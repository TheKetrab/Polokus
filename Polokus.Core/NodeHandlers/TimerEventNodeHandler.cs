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
        public TimerEventNodeHandler(IProcessInstance processInstance, FlowNode<tIntermediateCatchEvent> typedNode, string timeDefinitions)
            : base(processInstance, typedNode)
        {
            TimeDefinitions = timeDefinitions;
        }

        protected override async Task<ProcessResultInfo> Action(IFlowNode? caller)
        {
            int waitTime = TimeString.ParseToMiliseconds(TimeDefinitions);
            await Task.Delay(waitTime);
            return new ProcessResultInfo(ProcessResultState.Success);
        }

    }
}
