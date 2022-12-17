using Polokus.Core;
using Polokus.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Tests.Helpers
{
    internal static class BpmnLoader
    {
        private static int _cnt = 0;

        public static SimpleProcessInstance LoadBpmnXmlIntoSimpleProcessInstance(string bpmnString)
        {
            var wfManager = LoadBpmnXmlIntoWorkflowsManager(bpmnString);

            var workflow = wfManager.Workflows.First().Value;
            var bpmnProcess = workflow.BpmnWorkflow.BpmnProcesses.First();

            return new SimpleProcessInstance($"pi{_cnt++}", workflow, bpmnProcess);
        }

        public static WorkflowsManager LoadBpmnXmlIntoWorkflowsManager(string bpmnString, IHooksProvider? hooksProvider = null)
        {
            var wfManager = new WorkflowsManager();
            wfManager.LoadXmlString(bpmnString, "pr", hooksProvider: hooksProvider);
            return wfManager;
        }

    }
}
