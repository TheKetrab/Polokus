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
            var contextsManager = LoadBpmnXmlIntoContextsManager(bpmnString);

            var contextInstance = contextsManager.ContextInstances.First().Value;
            var bpmnProcess = contextInstance.BpmnContext.BpmnProcesses.First();

            return new SimpleProcessInstance($"pi{_cnt++}",contextInstance, bpmnProcess);
        }

        public static ContextsManager LoadBpmnXmlIntoContextsManager(string bpmnString, IHooksProvider? hooksProvider = null)
        {
            var contextsManager = new ContextsManager();
            contextsManager.LoadXmlString(bpmnString, "pr", hooksProvider: hooksProvider);
            return contextsManager;
        }

    }
}
