using Polokus.Core.Models;
using Polokus.Core.Models.BpmnObjects.Xsd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Interfaces
{
    public interface IFlowNode
    {
        string Name { get; }
        string Id { get; }

        ICollection<ISequence> Incoming { get; }
        ICollection<ISequence> Outgoing { get; }

        IBpmnProcess BpmnProcess { get; }

        Type XmlType { get; }
    }


}
