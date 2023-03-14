using Polokus.Core;

namespace Polokus.Tests.Helpers
{
    internal static class BpmnLoader
    {
        //private static int _cnt = 0;

        //public static SimpleProcessInstance LoadBpmnXmlIntoSimpleProcessInstance(string bpmnString)
        //{
        //    var polokus = LoadBpmnXmlIntoWorkflowsManager(bpmnString);

        //    var workflow = polokus.GetFirstWorkflow();
        //    var bpmnProcess = workflow.BpmnWorkflow.BpmnProcesses.First();

        //    return new SimpleProcessInstance($"pi{_cnt++}", workflow, bpmnProcess);
        //}

        public static PolokusMaster LoadBpmnXmlIntoMaster(string bpmnString)
        {
            var polokus = new PolokusMaster();
            polokus.LoadXmlString(bpmnString, "WF");
            return polokus;
        }

    }
}
