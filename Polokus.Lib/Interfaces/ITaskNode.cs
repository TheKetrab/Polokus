using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.Interfaces
{
    public interface ITaskNode : INode
    {
        IIOSpecification IOSpecification { get; set; }
        // TODO input association
        // TODO output association

    }
}
