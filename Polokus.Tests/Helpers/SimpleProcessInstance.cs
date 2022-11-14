using Polokus.Core;
using Polokus.Core.Interfaces;
using Polokus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Tests.Helpers
{
    internal class SimpleProcessInstance : ProcessInstance
    {
        public SimpleProcessInstance(string id, IContextInstance contextInstance, IBpmnProcess bpmnProcess, IHooksProvider? hooksProvider = null)
            : base(id, contextInstance, bpmnProcess, hooksProvider)
        {
        }

        public async Task<bool> RunSimple(IHooksProvider? hooksProvider = null, IFlowNode? startNode = null, int? timeout = null)
        {
            startNode ??= BpmnProcess.GetStartNodes().First();
            if (hooksProvider != null)
            {
                this._hooksProvider = hooksProvider;
            }

            Begin(startNode);

            var start = DateTime.Now;
            while (IsRunning())
            {
                await Task.Delay(100);

                if (timeout.HasValue && DateTime.Now - start > TimeSpan.FromSeconds(timeout.Value))
                {
                    _hooksProvider?.OnTimeout("");
                    return false;
                }
            }

            return true;

        }


    }
}
