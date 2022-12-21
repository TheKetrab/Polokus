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
            var polokus = LoadBpmnXmlIntoWorkflowsManager(bpmnString);

            var workflow = polokus.GetFirstWorkflow();
            var bpmnProcess = workflow.BpmnWorkflow.BpmnProcesses.First();

            return new SimpleProcessInstance($"pi{_cnt++}", workflow, bpmnProcess);
        }

        public static PolokusMaster LoadBpmnXmlIntoWorkflowsManager(string bpmnString, IHooksProvider? hooksProvider = null)
        {
            var polokus = new PolokusMaster();

            if (hooksProvider != null)
            {
                polokus.HooksManager.RegisterHooksProvider(hooksProvider);
            }

            polokus.LoadXmlString(bpmnString, "pr");
            return polokus;
        }

    }
}
