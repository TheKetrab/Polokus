using Polokus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Polokus.Core.Interfaces
{
    public interface ISequence
    {
        public string Name { get; }
        public string Id { get; }
        public IFlowNode? Source { get; }
        public IFlowNode? Target { get; }
        public IBpmnProcess BpmnProcess { get; }
    }
}
