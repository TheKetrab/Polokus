using Polokus.Core.Execution;
using Polokus.Core.Externals;
using Polokus.Core.Helpers;
using Polokus.Core.Hooks;
using Polokus.Core.Interfaces;
using Polokus.Core.Models;
using Polokus.Core.Models.BpmnObjects.Xsd;
using Polokus.Core.NodeHandlers;
using Polokus.Core.Scripting;
using Polokus;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core
{
    public class PolokusMaster : IPolokusMaster
    {
        internal IDictionary<string, IWorkflow> _workflows
            = new Dictionary<string, IWorkflow>();

        public ISettingsProvider SettingsProvider { get; set; }

        public IHooksManager HooksManager { get; set; } = new HooksManager();

        public Externals.Externals? Externals { get; }

        public ICollection<FileMonitor> FileMonitors { get; }
            = new List<FileMonitor>();


        private Dictionary<string, Logger> _logs = new();

        public event EventHandler<Signal>? Signal;

        public Logger GetOrCreateLogger(string wfId, string piId)
        {
            return GetOrCreateLogger($"{wfId}/{piId}");
        }

        public Logger GetOrCreateLogger(string globalPiId)
        {
            if (_logs.ContainsKey(globalPiId))
            {
                return _logs[globalPiId];
            }
            else
            {
                var newLogger = new Logger();
                _logs.Add(globalPiId, new Logger());
                return newLogger;
            }
        }

        public IFlowNode GetFlowNode(string wfId, string piId, string nodeId)
        {
            IWorkflow workflow = _workflows[wfId];
            IProcessInstance processInstance = workflow.GetProcessInstanceById(piId);
            IFlowNode node = processInstance.BpmnProcess.GetNodeById(nodeId);

            return node;
        }

        public void Log(string wfId, string piId, string info, Logger.MsgType type)
        {
            Log($"{wfId}/{piId}", info, type);
        }

        public void Log(string globalPiId, string info, Logger.MsgType type)
        {
            var logger = GetOrCreateLogger(globalPiId);
            switch (type)
            {
                case Logger.MsgType.Simple:
                    logger.Log(info);
                    break;
                case Logger.MsgType.Warning:
                    logger.LogWarning(info);
                    break;
                case Logger.MsgType.Error:
                    logger.LogError(info);
                    break;
            }
        }

        public bool ClientConnected { get; } = false; // TODO


        public PolokusMaster()
        {
            Externals = ExternalsManager.TryLoadExternals("./externals.json");
            if (Externals != null)
            {
                // ----- Register Hooks Providers -----
                if (Externals.HooksProviders != null)
                {
                    foreach (var externalHp in Externals.HooksProviders)
                    {
                        var hooksProvider = ExternalsManager.InstantiateHooksProvider(externalHp);
                        HooksManager.RegisterHooksProvider(hooksProvider);
                    }
                }

                // ----- Register Settings Provider -----
                if (Externals.SettingsProvider != null)
                {
                    SettingsProvider = ExternalsManager.InstantiateSettingsProvider(Externals.SettingsProvider);
                }

                // ----- Register File Monitors -----
                if (Externals.FileMonitors != null)
                {
                    foreach (var fileMonitor in Externals.FileMonitors)
                    {
                        RegisterFileMonitor(fileMonitor.Path,
                            fileMonitor.Actions.Select(x => Tuple.Create(x.Event, x.Signal, x.Params)));
                    }
                }

            }

            // ===== ===== ASSURE SETTINGS PROVIDER IS NOT NULL ===== =====
            if (SettingsProvider == null)
            {
                SettingsProvider = new DefaultSettingsProvider();
            }

        }

        private void RegisterActionForFileMonitor(FileMonitor fm,
            FileMonitor.FileEvtType evtType, string signal, string? parameters)
        {
            EventHandler<string> evt = (s, e) =>
            {
                string queryString = string.IsNullOrEmpty(parameters)
                    ? $"path={e}"
                    : $"{parameters}&path={e}";

                Signal?.Invoke(this, new Signal(signal, queryString));
            };

            switch (evtType)
            {
                case FileMonitor.FileEvtType.FileCreated:
                    fm.FileCreated += evt;
                    break;
                case FileMonitor.FileEvtType.FileRenamed:
                    fm.FileRenamed += evt;
                    break;
                case FileMonitor.FileEvtType.FileModified:
                    fm.FileModified += evt;
                    break;
                case FileMonitor.FileEvtType.DirectoryRenamed:
                    fm.DirectoryRenamed += evt;
                    break;
                case FileMonitor.FileEvtType.DirectoryCreated:
                    fm.DirectoryCreated += evt;
                    break;
                case FileMonitor.FileEvtType.ItemDeleted:
                    fm.ItemDeleted += evt;
                    break;
            }
        }

        public void RegisterFileMonitor(string pathToMonitor,
            FileMonitor.FileEvtType evtType, string signal, string? parameters = null)
        {
            var fm = new FileMonitor(pathToMonitor);
            RegisterActionForFileMonitor(fm, evtType, signal, parameters);
            fm.StartMonitoring();
            FileMonitors.Add(fm);
        }

        public void RegisterFileMonitor(string pathToMonitor,
            IEnumerable<Tuple<FileMonitor.FileEvtType,string,string>> actions)
        {
            var fm = new FileMonitor(pathToMonitor);
            foreach (var action in actions) 
            {
                var evtType = action.Item1;
                string signal = action.Item2;
                string parameters = action.Item3;

                RegisterActionForFileMonitor(fm, evtType, signal, parameters);
            }

            fm.StartMonitoring();
            FileMonitors.Add(fm);
        }

        public void AddWorkflow(string id, IWorkflow workflow)
        {
            if (_workflows.ContainsKey(id))
            {
                throw new Exception($"Collection already contains workflow with id {id}");
            }

            _workflows.Add(id, workflow);
        }

        public IWorkflow GetWorkflow(string id)
        {
            if (!_workflows.ContainsKey(id))
            {
                throw new Exception($"Workflow with id {id} not found.");
            }

            return _workflows[id];
        }

        public IEnumerable<IWorkflow> GetWorkflows()
        {
            return _workflows.Values;
        }

        /// <summary>
        /// This method reads and parses BPMN workflow definitions from given string, adds it to stored dictionary and returns parsed workflow.
        /// </summary>
        /// <param name="xmlString">BPMN process stored in xml as string.</param>
        /// <param name="bpmnWorkflowName">Name of process.</param>
        public void LoadXmlString(string xmlString, string bpmnWorkflowName)
        {
            BpmnParser parser = new BpmnParser();
            IBpmnWorkflow bpmnWorkflow = parser.ParseBpmnXml(xmlString);

            var workflow = new Workflow(this, bpmnWorkflow, bpmnWorkflowName, hooksProvider: HooksManager);
            RegisterStarters(workflow);

            AddWorkflow(bpmnWorkflowName, workflow);
        }

        private void RegisterStarters(Workflow workflow)
        {
            var allStartNodes = workflow.BpmnWorkflow.BpmnProcesses.SelectMany(x => x.GetStartNodes());
            foreach (var startNode in allStartNodes)
            {
                if (startNode is FlowNode<tStartEvent> startFlowNode)
                {
                    if (startFlowNode.XmlElement.Items?.Any() ?? false)
                    {
                        var eventDefinition = startFlowNode.XmlElement.Items[0];
                        if (eventDefinition is tTimerEventDefinition)
                        {
                            string timeDefinition = startFlowNode.Name;
                            var processStarter = new ProcessStarter(workflow, startFlowNode.BpmnProcess, startNode);
                            workflow.TimeManager.RegisterStarter(timeDefinition, processStarter);
                        }
                        else if (eventDefinition is tMessageEventDefinition)
                        {
                            var processStarter = new ProcessStarter(workflow, startFlowNode.BpmnProcess, startNode);
                            workflow.MessageManager.RegisterMessageListener(processStarter);
                        }
                        else if (eventDefinition is tSignalEventDefinition)
                        {
                            var processStarter = new ProcessStarter(workflow, startFlowNode.BpmnProcess, startNode);
                            workflow.SignalManager.RegisterSignalListener(processStarter);
                        }

                    }

                }

            }
        }

        public void EmitSignal(object? sender, string signal, string? parameters)
        {
            Signal?.Invoke(sender, new Signal(signal,parameters));
        }
    }
}
