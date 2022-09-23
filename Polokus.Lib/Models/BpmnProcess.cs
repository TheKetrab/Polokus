using Polokus.Lib.Models.BpmnObjects.Xsd;
using Polokus.Lib.NodeHandlers;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Polokus.Lib.Models
{
    public class BpmnProcess
    {

        public tDefinitions? SourceDefinitions { get; set; }

        public FlowNode? StartNode { get; set; }



        private Dictionary<string, FlowNode> nodesDictionary
            = new Dictionary<string, FlowNode>();

        private Dictionary<string, Sequence> sequencesDictionary
            = new Dictionary<string, Sequence>();

        
        public FlowNode? GetNodeById(string id)
        {
            if (nodesDictionary.TryGetValue(id, out FlowNode? node))
            {
                return node;
            }

            return null;
        }

        public Sequence? GetSequenceById(string id)
        {
            if (sequencesDictionary.TryGetValue(id, out Sequence? sequence))
            {
                return sequence;
            }

            return null;
        }


        public void SetNodes(ICollection<FlowNode> nodes)
        {
            nodesDictionary = nodes.ToDictionary(x => x.Id);
        }

        public void SetSequences(ICollection<Sequence> sequences)
        {
            sequencesDictionary = sequences.ToDictionary(x => x.Id);
        }

        public ICollection<FlowNode> GetNodes()
        {
            return nodesDictionary.Values.ToList();
        }

        public ICollection<Sequence> GetSequences()
        {
            return sequencesDictionary.Values.ToList();
        }

        public string GetSimpleGraph()
        {
            const int xmlTypeLen = 20;
            const int idLen = 20;
            const int nameLen = 20;

            const string prefix = " -> ";
            const string sep = " | ";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{new string(' ',prefix.Length)}{"Type".PadRight(xmlTypeLen)}{sep}{"Id".PadRight(idLen)}{sep}{"Name".PadRight(nameLen)}");

            sb.Append(new string(' ', prefix.Length));
            sb.Append(new string('-', xmlTypeLen));
            sb.Append(sep);
            sb.Append(new string('-', idLen));
            sb.Append(sep);
            sb.Append(new string('-', nameLen));
            sb.Append(Environment.NewLine);

            for (FlowNode? temp = StartNode; temp != null; temp = temp.Outgoing.FirstOrDefault()?.Target)
            {
                sb.AppendLine($"{prefix}{temp.XmlType.Name?.PadRight(xmlTypeLen)}{sep}{temp.Id.PadRight(idLen)}{sep}{temp.Name?.PadRight(nameLen)}");
            }

            return sb.ToString();
        }
        

    }
}
