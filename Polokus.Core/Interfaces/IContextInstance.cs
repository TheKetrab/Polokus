using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Interfaces
{
    public interface IContextInstance
    {
        ICollection<IProcessInstance> ProcessInstances { get; }
        IScriptProvider ScriptProvider { get; }
        IEnumerable<ICatchingStartEvent> CatchingStartEvents { get; }
        IContextsManager ContextsManager { get; }
        IBpmnContext BpmnContext { get; }
        INodeHandlerFactory NodeHandlerFactory { get; }

    }
}
