﻿using Polokus.Core.Execution;
using Polokus.Core.Externals;
using Polokus.Core.Helpers;
using Polokus.Core.Hooks;
using Polokus.Core.Interfaces;
using Polokus.Core.Interfaces.BpmnModels;
using Polokus.Core.Interfaces.Execution;
using Polokus.Core.Interfaces.Managers;
using Polokus.Core.Interfaces.Xsd;
using Polokus.Core.Models;

namespace Polokus.Core
{
    public class PolokusMaster : IPolokusMaster
    {
        internal IDictionary<string, IWorkflow> _workflows
            = new Dictionary<string, IWorkflow>();

        public ISettingsProvider SettingsProvider { get; set; }

        public IHooksManager HooksManager { get; set; }

        /// <summary>
        /// Object that reprezents externals and manages them. Can be null if externals.json not found.
        /// </summary>

        public Externals.Externals? Externals { get; }

        public ICollection<IMonitor> Monitors { get; }
            = new List<IMonitor>();


        private Dictionary<string, Logger> _logs = new();

        public event EventHandler<ISignal>? Signal;

        public StateSerializerManager StateSerializerManager { get; }

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

        public IFlowNode? GetFlowNode(string wfId, string piId, string nodeId)
        {
            if (!_workflows.ContainsKey(wfId))
            {
                return null;
            }

            IWorkflow workflow = _workflows[wfId];
            
            IProcessInstance? processInstance = workflow.GetProcessInstanceById(piId);

            if (processInstance == null)
            {
                return null;
            }

            return processInstance.BpmnProcess.GetNodeById(nodeId) ?? null;
        }

        public void Log(string wfId, string piId, string info, MsgType type)
        {
            Log($"{wfId}/{piId}", info, type);
        }

        public void Log(string globalPiId, string info, MsgType type)
        {
            var logger = GetOrCreateLogger(globalPiId);
            switch (type)
            {
                case MsgType.Simple:
                    logger.Log(info);
                    break;
                case MsgType.Warning:
                    logger.LogWarning(info);
                    break;
                case MsgType.Error:
                    logger.LogError(info);
                    break;
            }
        }

        public IConnectionManager ConnectionManager { get; }
        public bool ClientConnected => ConnectionManager.ClientConnected;


        public PolokusMaster(bool isGUIAppManaged = false)
        {
            HooksManager = new HooksManager(this);
            Externals = ExternalsManager.TryLoadExternals("C:\\Custom\\BPMN\\Polokus\\Examples\\Programmatibility\\externals.json");
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

                // ----- Register Monitors -----
                if (Externals.Monitors != null)
                {
                    foreach (var monitor in Externals.Monitors)
                    {
                        var m = ExternalsManager.InstantiateMonitor(this, monitor);
                        RegisterMonitor(m);
                    }
                }

            }

            ConnectionManager = new ConnectionManager(isGUIAppManaged);

            StateSerializerManager = new StateSerializerManager(this);
            StateSerializerHooks stateSerializer = new StateSerializerHooks(this);
            HooksManager.RegisterHooksProvider(stateSerializer);

            // ===== ===== ASSURE SETTINGS PROVIDER IS NOT NULL ===== =====
            if (SettingsProvider == null)
            {
                SettingsProvider = new DefaultSettingsProvider();
            }

        }

        public void RegisterMonitor(IMonitor monitor)
        {
            monitor.StartMonitoring();
            Monitors.Add(monitor);
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
                        var manager = GetManagerByType(workflow, eventDefinition);
                        manager.RegisterStarter(startFlowNode.BpmnProcess, startNode);
                    }

                }

            }
        }

        private ICallersManager GetManagerByType(Workflow workflow, tEventDefinition def)
        {
            switch (def)
            {
                case tTimerEventDefinition ted:
                    return workflow.TimeManager;
                case tMessageEventDefinition med:
                    return workflow.MessageManager;
                case tSignalEventDefinition sed:
                    return workflow.SignalManager;
                default:
                    throw new Exception("Not defined manager for this type.");
            };
        }


        public void EmitSignal(object? sender, string signal, string? parameters)
        {
            Signal?.Invoke(sender, new Signal(signal,parameters));
        }
    }
}
