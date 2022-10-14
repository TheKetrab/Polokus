using Polokus.Lib.Helpers;
using Polokus.Lib.Models;
using Polokus.Lib.Models.BpmnObjects.Xsd;
using Polokus.Lib.NodeHandlers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Polokus.Lib.NodeHandlers
{
    public class TimerEventNodeHandler : NodeHandler<tIntermediateCatchEvent>
    {



        public string TimeDefinitions { get; set; }
        public TimerEventNodeHandler(FlowNode<tIntermediateCatchEvent> typedNode, string timeDefinitions) : base(typedNode)
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
