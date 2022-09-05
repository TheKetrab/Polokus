using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Serialization;
using System.Xml.Linq;
using System.Xml;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Reflection;
using Polokus.Lib.BpmnObjects;
using Polokus.Lib.BpmnObjects.Nodes;
using Polokus.Lib.Interfaces;
using Polokus.Lib.BpmnObjects.Nodes.Tasks;

namespace Polokus.Lib.BpmnParser
{
    public static class BpmnParser
    {
        private static string GetBpmnNSValByAttr(XElement root)
        {
            string[] bpmns = new string[] { "bpmn2", "bpmn" };

            string? bpmnAttributeVal = null;
            for (int i = 0; i < bpmns.Length && bpmnAttributeVal == null; i++)
            {
                bpmnAttributeVal = root.Attribute(XNamespace.Xmlns + bpmns[i])?.Value; 
            }

            return bpmnAttributeVal ?? throw new XmlException("Unable to find bpmn namespace attribute in root.");
        }

        private static XNamespace GetBpmnNamespace(XElement root)
        {
            string bpmnNs = GetBpmnNSValByAttr(root);
            return XNamespace.Get(bpmnNs);
        }


        public static List<BpmnProcess> ParseFile(string file)
        {
            if (!File.Exists(file))
            {
                throw new FileNotFoundException(file);
            }

            XDocument document = XDocument.Load(file);
            var root = document.Root;
            if (root == null)
            {
                throw new XmlException("No root element.");
            }

            XNamespace ns = GetBpmnNamespace(root);
            var processNodes = root.Elements(ns + "process");

            if (!processNodes.Any())
            {
                throw new Exception($"None process found. Probably wrong namespace: {ns.NamespaceName}");
            }

            List<BpmnProcess> bpmnProcesses = new();
            foreach(var processNode in processNodes)
            {
                BpmnProcess process = ParseBpmnProcess(ns,processNode);
                bpmnProcesses.Add(process);
            }

            return bpmnProcesses;
        }

        public static BpmnProcess ParseBpmnProcess(XNamespace ns, XElement process)
        {
            BpmnProcess bpmnProcess = new BpmnProcess(process);

            List<SequenceFlow> sequenceFlows = new List<SequenceFlow>();
            var sequences = process.Elements(ns + "sequenceFlow");
            sequences.ForEach(x => sequenceFlows.Add(new SequenceFlow(x)));

            List<Node> nodes = new List<Node>();
            var nodesElements = process.Elements().Where(x => x.Name != ns + "sequenceFlow");
            nodesElements.ForEach(x => { var n = ParseNode(x); if (n != null) nodes.Add(ParseNode(x)); });
            // TODO connect nodes

            bpmnProcess.SetNodes(nodes);


            return bpmnProcess;
        }

        public static Node ParseNode(XElement element)
        {
            switch (element.Name.LocalName)
            {
                case Constants.Task: return new TaskNode(element);

            }

            return null; // TODO
        }


    }
}
