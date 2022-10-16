using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Interfaces
{
    public interface IProcessStarter
    {
        IContextInstance ContextInstance { get; }
        IBpmnProcess BpmnProcess { get; }
        IFlowNode StartNode { get; }
        string Id { get; }
    }
}
