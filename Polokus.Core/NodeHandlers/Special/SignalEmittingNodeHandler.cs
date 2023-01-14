using Polokus.Core.Execution;
using Polokus.Core.Helpers;
using Polokus.Core.Interfaces;
using Polokus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polokus.Core.NodeHandlers.Abstract;
using Polokus.Core.Interfaces.Xsd;

namespace Polokus.Core.NodeHandlers.Special
{
    public class SignalEmittingNodeHandler<T> : NodeHandler<T> where T : tFlowNode
    {
        public SignalEmittingNodeHandler(IProcessInstance processInstance, FlowNode<T> typedNode)
            : base(processInstance, typedNode)
        {
        }

        public override Task Action(INodeCaller? caller)
        {
            string signal = this.Node.Name; // TODO get signal from properties
            this.ProcessInstance.Workflow.SignalManager.EmitSignal(signal);
            return Task.CompletedTask;
        }
    }
}
