using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.Interfaces
{

    public interface INode
    {
        string Id { get; set; }
        IEnumerable<INode> Incoming { get; set; }
        IEnumerable<INode> Outgoing { get; set; }
    }


}
