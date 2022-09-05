using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Polokus.Lib.Interfaces;

namespace Polokus.Lib.BpmnObjects.Nodes.Tasks
{
    public class TaskNode : Node, ITaskNode
    {
        public TaskNode(XElement element) : base(element)
        {
        }

        public IIOSpecification IOSpecification { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
