using Polokus.Core.Helpers;
using Polokus.Core.Interfaces.BpmnModels;
using Polokus.Core.Interfaces.Xsd;
using System.Linq;

namespace Polokus.Core.Models
{
    public class BpmnProcess : IBpmnProcess
    {
        public string Id { get; }
        public IBpmnWorkflow BpmnWorkflow { get; set; }
        public tDefinitions? SourceDefinitions { get; set; }


        private Dictionary<string, IFlowNode> nodesDictionary
            = new Dictionary<string, IFlowNode>();

        private Dictionary<string, ISequence> sequencesDictionary
            = new Dictionary<string, ISequence>();


        public BpmnProcess(IBpmnWorkflow bpmnWorkflow, string id)
        {
            BpmnWorkflow = bpmnWorkflow;
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

        public IFlowNode GetManualStartNode()
        {
            return nodesDictionary.Values.Single(BpmnXmlHelpers.IsManualStartNode);
        }

        public bool IsManualProcess()
        {
            return nodesDictionary.Values.Any(BpmnXmlHelpers.IsManualStartNode);
        }

        public IEnumerable<string> GetNodesIds()
        {
            return nodesDictionary.Values.Select(x => x.Id);
        }

        public IEnumerable<string> GetServiceTasksNames()
        {
            return nodesDictionary.Values
                .Where(x => x.XmlType == typeof(tServiceTask))
                .Select(x => x.Name);
        }

        public bool ContainsNode(string id)
        {
            return nodesDictionary.ContainsKey(id);
        }

        public IMessageCallerNode GetMessageCallerNode(string id)
        {
            IFlowNode node = GetNodeById(id)
                ?? throw new Exception($"Node with id {id} not found.");

            return node as IMessageCallerNode
                ?? throw new Exception($"Node with id {id} is {node.GetType()}, not IMessageCaller.");
        }
        
        public IMessageReceiverNode GetMessageReceiverNode(string id)
        {
            IFlowNode node = GetNodeById(id)
                ?? throw new Exception($"Node with id {id} not found.");

            return node as IMessageReceiverNode
                ?? throw new Exception($"Node with id {id} is {node.GetType()}, not IMessageReceiver.");
        }

    }
}
