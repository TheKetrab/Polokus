using Polokus.Lib.Models.BpmnObjects.Xsd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.NodeHandlers
{
    public class TimerEventNodeHandler : NodeHandler<tTimerEventDefinition>
    {
        public TimerEventNodeHandler(ProcessInstance process) : base(process)
        {
            tTimerEventDefinition ss;
        }
    }
}
