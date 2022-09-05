using Polokus.Lib.BpmnObjects.Nodes;
using Polokus.Lib.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Polokus.Lib.BpmnObjects
{
    public class BpmnProcess : BpmnObject, IBpmnProcess
    {
        public bool IsExecutable { get; set; }

        private ImmutableDictionary<string, INode> nodesDictionary
            = new Dictionary<string, INode>().ToImmutableDictionary();

        public BpmnProcess(XElement element) : base(element)
        {
            
        }



        public INode? GetNodeById(string id)
        {
            if (nodesDictionary.TryGetValue(id, out INode? node))
            {
                return node;
            }

            return null;
        }

        IEnumerable<INode> IBpmnProcess.GetNodes()
        {
            return nodesDictionary.Values.ToList();
        }

        public void SetNodes(IEnumerable<INode> nodes)
        {
            nodesDictionary = nodes.ToImmutableDictionary(x => x.Id);
        }
    }
}
