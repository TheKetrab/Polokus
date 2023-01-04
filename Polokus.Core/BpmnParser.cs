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
using Polokus.Core.Factories;
using Polokus.Core.Interfaces;
using Polokus.Core.Helpers;
using Polokus.Core.BpmnModels;

namespace Polokus.Core
{
    /// <summary>
    /// BpmnParser is an object that parses XML string of BPMN schema provided by OMG
    /// and returns structure of nodes for Polokus system.
    /// </summary>
    public class BpmnParser
    {
        public IBpmnWorkflow ParseBpmnXml(string bpmnXml)
        {
            var definitions = DeserializeXml<tDefinitions>(bpmnXml);
            if (definitions == null)
            {
                throw new XmlException($"Failed to parse bpmn xml:\n{bpmnXml}");
            }

            var Workflow = new BpmnWorkflow();
            LoadDefinitions(Workflow, definitions);

            Workflow.RawString = bpmnXml.Replace("\r","").Replace("\n","");

            return Workflow;
        }

        private void FillProcessConnections(BpmnProcess process,
            IEnumerable<tSequenceFlow> xmlSequences, IEnumerable<tFlowNode> xmlFlowNodes)
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

        private void ReadFlowElementsInProcess(BpmnWorkflow Workflow, tDefinitions definitions,
            tFlowElement[] items, string processId, List<IBpmnProcess> processes)
        {
            var sequences = items.Where(x => x is tSequenceFlow).Cast<tSequenceFlow>();
            var flowNodes = items.Where(x => x is tFlowNode).Cast<tFlowNode>();
            var boundaryEvents = items.Where(x => x is tBoundaryEvent).Cast<tBoundaryEvent>();

            var process = new BpmnProcess(Workflow, processId);
            process.SourceDefinitions = definitions;
            FillProcessConnections(process, sequences, flowNodes);
            SetUpBoundaryEvents(process, boundaryEvents);

            processes.Add(process);

            var subProcesses = items.Where(x => x is tSubProcess).Cast<tSubProcess>();
            foreach (var sp in subProcesses)
            {
                ReadFlowElementsInProcess(Workflow, definitions, sp.Items1, sp.id, processes);
            }
        }

        private void SetUpBoundaryEvents(BpmnProcess process, IEnumerable<tBoundaryEvent> boundaryEvents)
        {
            foreach (var evt in boundaryEvents)
            {
                string attachedToId = evt.attachedToRef.Name;
                var node = process.GetNodeById(attachedToId);
                var boundaryEvt = process.GetNodeById(evt.id) as BoundaryEvent;

                // TODO null checks
                boundaryEvt.AttachedTo = node;
                node.BoundaryEvents.Add(boundaryEvt);
            }
        }

        private void LoadDefinitions(BpmnWorkflow Workflow, tDefinitions definitions)
        {
            // Read processes
            List<IBpmnProcess> processes = new();
            foreach (var item in definitions.Items)
            {
                if (item is tProcess xmlProcess)
                {
                    ReadFlowElementsInProcess(Workflow, definitions, xmlProcess.Items, xmlProcess.id, processes);
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

            Workflow.SetBpmnProcesses(processes);
        }

        public static T? DeserializeXml<T>(string xmlString) where T : class
        {
            var serializer = new XmlSerializer(typeof(T));

            T? obj;
            using (TextReader reader = new StringReader(xmlString))
            {
                obj = serializer.Deserialize(reader) as T;
            }

            return obj;
        }

    }
}
