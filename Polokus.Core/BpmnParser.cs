using Polokus.Core.Models;
using Polokus.Core.Models.BpmnObjects.Xsd;
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
using Polokus.Core.Factories;
using Polokus.Core.Interfaces;
using Polokus.Core.Helpers;
using Polokus.Core.BpmnModels;

namespace Polokus.Core
{
    public class BpmnParser
    {

        public IBpmnContext ParseFile(string filename)
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

            var context = new BpmnContext();
            LoadDefinitions(context, definitions);

            string rawString = File.ReadAllText(filename);
            context.RawString = rawString.ReplaceLineEndings("");

            return context;
        }

        private void FillProcessConnections(BpmnProcess process, IEnumerable<tSequenceFlow> xmlSequences, IEnumerable<tFlowNode> xmlFlowNodes)
        {
            var flowNodeFactory = new FlowNodeFactory();
            
            var nodes = xmlFlowNodes.Select(x => flowNodeFactory.CreateFlowNode(process, x)).ToList();
            var sequences = xmlSequences.Select(x => new Sequence(process, x)).ToList();

            process.SetNodes(nodes);
            process.SetSequences(sequences);

            foreach (var x in xmlSequences)
            {
                ISequence? seq = process.GetSequenceById(x.id);
                if (seq == null)
                {
                    Logger.Global.LogError($"Unable to find object for {x.id} sequence.");
                }

                IFlowNode? src = process.GetNodeById(x.sourceRef);
                IFlowNode? dest = process.GetNodeById(x.targetRef);

                if (src == null)
                {
                    Logger.Global.LogWarning($"Not found sourceRef {x.sourceRef} node for {x.id} sequence.");
                }
                if (dest == null)
                {
                    Logger.Global.LogWarning($"Not found targetRef {x.targetRef} node for {x.id} sequence.");
                }

                if (src != null && dest != null && seq != null)
                {
                    src.Outgoing.Add(seq);
                    dest.Incoming.Add(seq);
                }

            }

            if (!nodes.Any(x => x.IsStartNode()))
            {
                Logger.Global.LogWarning($"None start event found for process {process.Id}");
            }
            
        }

        private void LoadDefinitions(BpmnContext context, tDefinitions definitions)
        {
            // Read processes
            List<IBpmnProcess> processes = new();
            foreach (var item in definitions.Items)
            {
                if (item is tProcess xmlProcess)
                {
                    var sequences = xmlProcess.Items.Where(x => x is tSequenceFlow).Cast<tSequenceFlow>();
                    var flowNodes = xmlProcess.Items.Where(x => x is tFlowNode).Cast<tFlowNode>();

                    var process = new BpmnProcess(context, item.id);
                    process.SourceDefinitions = definitions;
                    FillProcessConnections(process, sequences, flowNodes);

                    processes.Add(process);
                }
                
            }

            // Read messages
            foreach (var item in definitions.Items)
            {
                if (item is tCollaboration collaboration)
                {
                    foreach (var mf in collaboration.messageFlow)
                    {
                        string sourceId = mf.sourceRef.Name;
                        string targetId = mf.targetRef.Name;

                        var sourceProcess = processes.FirstOrDefault(x => x.ContainsNode(sourceId))
                            ?? throw new Exception($"Unable to find process that contains node with id {sourceId}");

                        var targetProcess = processes.FirstOrDefault(x => x.ContainsNode(targetId))
                            ?? throw new Exception($"Unable to find process that contains node with id {targetId}");

                        var msgFlow = new MessageFlow(sourceProcess, targetProcess, mf);

                        var sourceNode = sourceProcess.GetMessageCallerNode(mf.sourceRef.Name)
                            ?? throw new Exception($"$Unable to find callerNode (process: {sourceProcess.Id} nodeId: {sourceId}");

                        var targetNode = targetProcess.GetMessageReceiverNode(mf.targetRef.Name)
                            ?? throw new Exception($"$Unable to find receiverNode (process: {targetProcess.Id} nodeId: {targetId}");

                        sourceNode.OutgoingMessages.Add(msgFlow);
                        targetNode.IncommingMessages.Add(msgFlow);

                    }
                }

            }


            context.SetBpmnProcesses(processes);
        }

        public static T? DeserializeXml<T>(string filename) where T : class
        {
            var serializer = new XmlSerializer(typeof(T));

            T? obj;
            using (Stream reader = new FileStream(filename, FileMode.Open))
            {
                obj = serializer.Deserialize(reader) as T;
            }

            return obj;

        }

    }
}
