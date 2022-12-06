using Polokus.Core.Interfaces;
using Polokus.Core.Models;
using Polokus.Core.Models.BpmnObjects.Xsd;
using Polokus.Core.NodeHandlers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.NodeHandlers
{
    public class SubProcessNodeHandler : NodeHandler<tSubProcess>
    {
        public SubProcessNodeHandler(IProcessInstance processInstance, FlowNode<tSubProcess> typedNode)
            : base(processInstance, typedNode)
        {
        }

        public override async Task Action(INodeCaller? caller)
        {
            var ci = this.ProcessInstance.ContextInstance;
            var bpmnProcess = ci.BpmnContext.BpmnProcesses.First(x => x.Id == this.Node.Id);
            var manualStartNode = bpmnProcess.GetManualStartNode();

            var subProcessInstance = this.ProcessInstance.CreateSubProcessInstance(bpmnProcess);
            await ci.RunProcessAsync(subProcessInstance, manualStartNode, null);
        }
    }
}
