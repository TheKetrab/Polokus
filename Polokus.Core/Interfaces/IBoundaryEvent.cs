using Polokus.Core.Models.BpmnObjects.Xsd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Interfaces
{
    public enum BoundaryEventType
    {
        Undefined,
        Signal,
        Message,
        Timer,
        Error
    }

    public interface IBoundaryEvent : IFlowNode
    {
        IFlowNode? AttachedTo { get; }
        BoundaryEventType Type { get; }
        bool Interrupting { get; }
    }
}
