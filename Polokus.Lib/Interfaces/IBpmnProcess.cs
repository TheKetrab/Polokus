using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.Interfaces
{
    public interface IBpmnProcess
    {
        IEnumerable<INode> GetNodes();
        INode? GetNodeById(string id);
        void SetNodes(IEnumerable<INode> nodes);

    }
}
