using Polokus.Core.Execution;
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
    public class SubProcessNodeHandler : SubprocessingNodeHandler<tSubProcess>
    {        
        public SubProcessNodeHandler(IProcessInstance processInstance, FlowNode<tSubProcess> typedNode)
            : base(processInstance, typedNode)
        {
        }

        protected override IBpmnProcess GetBpmnProcess()
        {
            return Workflow.BpmnWorkflow.BpmnProcesses.First(x => x.Id == this.Node.Id);
        }

        protected override IWorkflow GetWorkflow()
        {
            return this.Workflow;
        }
    }
}
