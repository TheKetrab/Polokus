using Polokus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Interfaces
{
    public interface IContextInstance
    {
        event EventHandler<string> ProcessInstanceChanged;
        void NotifyProcessInstanceChanged(string id);

        ICollection<IProcessInstance> ProcessInstances { get; }
        ICollection<IProcessInstance> History { get; }
        IScriptProvider ScriptProvider { get; }
        IEnumerable<IProcessStarter> CatchingStartEvents { get; }
        IContextsManager ContextsManager { get; }
        IBpmnContext BpmnContext { get; }
        INodeHandlerFactory NodeHandlerFactory { get; }
        string Id { get; }

        Task<bool> RunProcessAsync(IBpmnProcess bpmnProcess, IFlowNode startNode, int? timeout);
        void StartProcessInstance(IBpmnProcess bpmnProcess, IFlowNode startNode, int? timeout);

        void StartProcessManually(string bpmnProcessId);

    }
}
