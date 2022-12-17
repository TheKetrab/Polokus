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
            var wf = this.ProcessInstance.Workflow;
            var bpmnProcess = wf.BpmnWorkflow.BpmnProcesses.First(x => x.Id == this.Node.Id);
            var manualStartNode = bpmnProcess.GetManualStartNode();

            var subProcessInstance = this.ProcessInstance.CreateSubProcessInstance(bpmnProcess);
            await wf.RunProcessAsync(subProcessInstance, manualStartNode, null);
        }
    }
}
