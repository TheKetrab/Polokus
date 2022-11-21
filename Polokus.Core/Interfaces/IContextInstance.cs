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
        ITimeManager TimeManager { get; }
        IMessageManager MessageManager { get; }

        ISettingsProvider SettingsProvider { get; }

        ICollection<IProcessInstance> ProcessInstances { get; }
        ICollection<IProcessInstance> History { get; }
        IScriptProvider ScriptProvider { get; }
        IEnumerable<IProcessStarter> CatchingStartEvents { get; }
        IContextsManager ContextsManager { get; }
        IBpmnContext BpmnContext { get; }
        INodeHandlerFactory NodeHandlerFactory { get; }
        string Id { get; }

        Task<bool> RunProcessAsync(string processInstanceId, IBpmnProcess bpmnProcess, IFlowNode startNode, int? timeout);
        string StartProcessInstance(IBpmnProcess bpmnProcess, IFlowNode startNode, int? timeout);
        string StartProcessManually(string bpmnProcessId);

    }
}
