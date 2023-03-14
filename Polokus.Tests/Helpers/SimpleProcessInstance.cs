using Polokus.Core;
using Polokus.Core.Interfaces.BpmnModels;
using Polokus.Core.Interfaces.Execution;

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

        public static PolokusMaster ReadBpmnWithoutPi(string bpmnXml, out IWorkflow wf)
        {
            var master = BpmnLoader.LoadBpmnXmlIntoMaster(bpmnXml);
            wf = master.GetFirstWorkflow();
            return master;
        }


    }
}
