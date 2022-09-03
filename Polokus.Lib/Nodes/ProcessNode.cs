using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polokus.Lib.Interfaces;

namespace Polokus.Lib.Nodes
{

    public class ProcessNode : INode
    {
        public string Id { get; set; }
        public bool IsExecutable { get; set; }
        public IEnumerable<INode> Incoming { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IEnumerable<INode> Outgoing { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
