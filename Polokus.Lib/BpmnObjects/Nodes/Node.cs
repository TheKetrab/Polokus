using Polokus.Lib.BpmnObjects;
using Polokus.Lib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Polokus.Lib.BpmnObjects.Nodes
{
    public abstract class Node : BpmnObject, INode
    {
        private IEnumerable<INode> _incoming = new List<INode>();
        public IEnumerable<INode> Incoming { get => _incoming; set => _incoming = value; }

        private IEnumerable<INode> _outgoing = new List<INode>();
        public IEnumerable<INode> Outgoing { get => _outgoing; set => _outgoing = value; }
        
        public Node(XElement element) : base(element)
        {
        }
    }
}
