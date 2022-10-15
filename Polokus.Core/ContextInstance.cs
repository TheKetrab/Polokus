using Polokus.Core.Factories;
using Polokus.Core.Interfaces;
using Polokus.Core.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core
{
    public class ContextInstance : IContextInstance
    {
        public ICollection<IProcessInstance> ProcessInstances { get; } = new List<IProcessInstance>();

        public IScriptProvider ScriptProvider { get; } = new ScriptProvider();

        public IEnumerable<ICatchingStartEvent> CatchingStartEvents { get; } = new List<ICatchingStartEvent>();

        public IContextsManager ContextsManager { get; }
        public IBpmnContext BpmnContext { get; }

        public INodeHandlerFactory NodeHandlerFactory { get; }

        public ContextInstance(IContextsManager contextsManager, IBpmnContext bpmnContext)
        {
            ContextsManager = contextsManager;
            BpmnContext = bpmnContext;

            var nhFactory = new NodeHandlerFactory();
            nhFactory.SetDefaultNodeHandlers();

            NodeHandlerFactory = nhFactory;

        }
    }
}
