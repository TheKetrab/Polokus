using Polokus.Lib.Models;
using Polokus.Lib.Models.BpmnObjects.Xsd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using Polokus.Lib.Factories;

namespace Polokus.Lib
{
    public class BpmnParser
    {
        public static BpmnContext ParseFile(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException(filename);
            }

            var definitions = DeserializeXml<tDefinitions>(filename);
            if (definitions == null)
            {
                throw new XmlException($"Failed to parse file {filename}");
            }

            var processes = LoadDefinitions(definitions);

            BpmnContext context = new BpmnContext()
            {
                Definitions = definitions,
                Processes = processes
            };

            return context;
        }

        private static BpmnProcess CreateProcess(IEnumerable<tSequenceFlow> xmlSequences, IEnumerable<tFlowNode> xmlFlowNodes)
        {
            var process = (BpmnProcess?)Activator.CreateInstance(typeof(BpmnProcess), true);
            if (process == null)
            {
                throw new Exception("Failed to create process via Activator class.");
            }

            var nodes = xmlFlowNodes.Select(x => FlowNodesFactory.CreateFlowNode(process, x)).ToList();
            var sequences = xmlSequences.Select(x => new Sequence(process, x)).ToList();
            process.SetNodes(nodes);
            process.SetSequences(sequences);

            foreach (var x in xmlSequences)
            {
                Sequence? seq = process.GetSequenceById(x.id);
                if (seq == null)
                {
                    Logger.LogError($"Unable to find object for {x.id} sequence.");
                }

                IFlowNode? src = process.GetNodeById(x.sourceRef);
                IFlowNode? dest = process.GetNodeById(x.targetRef);

                if (src == null)
                {
                    Logger.LogWarning($"Not found sourceRef {x.sourceRef} node for {x.id} sequence.");
                }
                if (dest == null)
                {
                    Logger.LogWarning($"Not found targetRef {x.targetRef} node for {x.id} sequence.");
                }

                if (src != null && dest != null && seq != null)
                {
                    src.Outgoing.Add(seq);
                    dest.Incoming.Add(seq);
                }

            }

            var startNodes = nodes.Where(x => x.GetType() == typeof(FlowNode<tStartEvent>));
            if (!startNodes.Any())
            {
                Logger.LogError("None start event found!");
            }
            if (startNodes.Count() > 1)
            {
                Logger.LogWarning($"Found more than one start event! ({string.Join(' ', startNodes)})");
            }

            process.StartNode = startNodes.FirstOrDefault();

            return process;
        }

        public static List<BpmnProcess> LoadDefinitions(tDefinitions definitions)
        {
            List<BpmnProcess> processes = new();

            foreach (var item in definitions.Items)
            {
                if (item is tProcess xmlProcess)
                {
                    var sequences = xmlProcess.Items.Where(x => x is tSequenceFlow).Cast<tSequenceFlow>();
                    var flowNodes = xmlProcess.Items.Where(x => x is tFlowNode).Cast<tFlowNode>();

                    var process = CreateProcess(sequences, flowNodes);
                    process.SourceDefinitions = definitions;

                    processes.Add(process);
                }
            }

            return processes;

        }

        public static T? DeserializeXml<T>(string filename) where T : class
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            T? obj;
            using (Stream reader = new FileStream(filename, FileMode.Open))
            {
                obj = serializer.Deserialize(reader) as T;
            }

            return obj;

        }



    }
}
