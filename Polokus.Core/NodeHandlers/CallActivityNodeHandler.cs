using Polokus.Core.Execution;
using Polokus.Core.Interfaces;
using Polokus.Core.Interfaces.BpmnModels;
using Polokus.Core.Interfaces.Xsd;
using Polokus.Core.Models;
using Polokus.Core.NodeHandlers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.NodeHandlers
{
    public class CallActivityNodeHandler : SubprocessingNodeHandler<tCallActivity>
    {
        private IWorkflow? _wf;
        private IWorkflow Wf
        {
            get
            {
                if (_wf == null)
                {
                    ObtainWorkflowAndProcess();
                }
                return _wf!;
            }
        }

        private IBpmnProcess? _pr;
        private IBpmnProcess Pr
        {
            get
            {
                if (_pr == null)
                {
                    ObtainWorkflowAndProcess();
                }
                return _pr!;
            }
        }
        
        public CallActivityNodeHandler(IProcessInstance processInstance, FlowNode<tCallActivity> typedNode)
            : base(processInstance, typedNode)
        {
        }

        protected override IBpmnProcess GetBpmnProcess()
        {
            return Pr;
        }

        protected override IWorkflow GetWorkflow()
        {
            return Wf;
        }

        private void ObtainWorkflowAndProcess()
        {
            string name = this.Node.Name;
            int separator = name.IndexOf('/');

            string workflowId = name.Substring(0, separator).Trim();
            string processId = name.Substring(separator + 1).Trim();

            _wf = Master.GetWorkflow(workflowId);
            _pr = _wf.BpmnWorkflow.BpmnProcesses.First(x => x.Id == processId);
        }
    }
}
