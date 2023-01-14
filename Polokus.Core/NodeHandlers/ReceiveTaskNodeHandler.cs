using Polokus.Core.Interfaces;
using Polokus.Core.Interfaces.Xsd;
using Polokus.Core.Models;
using Polokus.Core.NodeHandlers.Abstract;
using Polokus.Core.NodeHandlers.Special;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.NodeHandlers
{
    public class ReceiveTaskNodeHandler : MessageCatchingNodeHandler<tReceiveTask>
    {
        public ReceiveTaskNodeHandler(IProcessInstance processInstance, FlowNode<tReceiveTask> typedNode)
            : base(processInstance, typedNode)
        {
        }
    }
}
