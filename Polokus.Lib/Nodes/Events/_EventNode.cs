using Polokus.Lib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.Nodes.Events
{
    public abstract class EventNode : IEventNode
    {
        public string Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IEnumerable<INode> Incoming { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IEnumerable<INode> Outgoing { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
