using Polokus.Core;
using Polokus.Core.Execution;
using Polokus.Core.Hooks;
using Polokus.Core.Interfaces;
using Polokus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Tests.Helpers
{
    internal static class TestHelper
    {
        public static PolokusMaster ReadBpmn(string bpmnXml,
            out IWorkflow wf, out IProcessInstance pi, out IFlowNode startNode)
        {
            var master = BpmnLoader.LoadBpmnXmlIntoMaster(bpmnXml);
            wf = master.GetFirstWorkflow();
            var bpmnProcess = wf.BpmnWorkflow.BpmnProcesses.First();
            startNode = bpmnProcess.GetManualStartNode();
            pi = wf.CreateProcessInstance(bpmnProcess);
            return master;
        }


    }
}
