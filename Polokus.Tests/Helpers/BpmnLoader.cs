using Polokus.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Tests.Helpers
{
    internal static class BpmnLoader
    {
        public const string TestsDir = @"..\..\..\";
        public const string BpmnDir = TestsDir + @"NodeHandlersTests\Bpmn\";

        public static SimpleProcessInstance LoadBpmnXmlIntoSimpleProcessInstance(string bpmnFile)
        {
            var contextsManager = new ContextsManager();
            contextsManager.LoadXmlFile($@"{BpmnDir}\{bpmnFile}");

            var contextInstance = contextsManager.ContextInstances.First().Value;
            var bpmnProcess = contextInstance.BpmnContext.BpmnProcesses.First();

            return new SimpleProcessInstance(contextInstance, bpmnProcess);
        }

    }
}
