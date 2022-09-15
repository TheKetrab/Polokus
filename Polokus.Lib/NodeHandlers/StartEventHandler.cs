using Polokus.Lib.Models;
using Polokus.Lib.Models.BpmnObjects.Xsd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.NodeHandlers
{
    public class StartEventHandler : NodeHandler<tStartEvent>
    {
        public StartEventHandler(ProcessInstance process) : base(process)
        {
            Finished += (s, e) =>
            {
                
            };
        }

    }
}
