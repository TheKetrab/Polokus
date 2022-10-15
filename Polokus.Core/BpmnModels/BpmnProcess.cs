using Polokus.Core.Helpers;
using Polokus.Core.Interfaces;
using Polokus.Core.Models.BpmnObjects.Xsd;
using Polokus.Core.NodeHandlers;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Polokus.Core.Models
{
    public class BpmnProcess : IBpmnProcess
    {
        public tDefinitions? SourceDefinitions { get; set; }

        public IBpmnContext BpmnContext { get; set; }

        public string Id { get; }

        private Dictionary<string, IFlowNode> nodesDictionary
            = new Dictionary<string, IFlowNode>();

        private Dictionary<string, ISequence> sequencesDictionary
            = new Dictionary<string, ISequence>();

        public BpmnProcess(IBpmnContext bpmnContext, string id)
        {
            BpmnContext = bpmnContext;
            Id = id;
        }

        
        public IFlowNode? GetNodeById(string id)
        {
            if (nodesDictionary.TryGetValue(id, out IFlowNode? node))
            {
                return node;
            }

            return null;
        }

        public ISequence? GetSequenceById(string id)
        {
            if (sequencesDictionary.TryGetValue(id, out ISequence? sequence))
            {
                return sequence;
            }

            return null;
        }

        public void SetNodes(IEnumerable<IFlowNode> nodes)
        {
            nodesDictionary = nodes.ToDictionary(x => x.Id);
        }

        public void SetSequences(IEnumerable<ISequence> sequences)
        {
            sequencesDictionary = sequences.ToDictionary(x => x.Id);
        }

        public IEnumerable<IFlowNode> GetNodes()
        {
            return nodesDictionary.Values.ToList();
        }

        public IEnumerable<ISequence> GetSequences()
        {
            return sequencesDictionary.Values.ToList();
        }

        public IEnumerable<IFlowNode> GetStartNodes()
        {
            return nodesDictionary.Values.Where(x => BpmnXmlHelpers.IsStartNode(x));
        }

        public bool IsReachable(IFlowNode src, IFlowNode dest)
        {
            return IsReachableDFS(src, dest, new HashSet<IFlowNode>());
        }

        private bool IsReachableDFS(IFlowNode src, IFlowNode dest, HashSet<IFlowNode> visited)
        {
            if (src == dest)
            {
                return true;
            }

            if (visited.Contains(src))
            {
                return false;
            }

            foreach (var seq in src.Outgoing)
            {
                if (seq.Target == null)
                {
                    continue;
                }

                IFlowNode target = seq.Target;
                var newHashSet = new HashSet<IFlowNode>(visited);
                newHashSet.Add(src);
                bool res = IsReachableDFS(target, dest, newHashSet);
                if (res == true)
                {
                    return true;
                }
            }

            return false;
        }

    }
}
