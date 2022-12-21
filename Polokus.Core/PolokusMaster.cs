using Polokus.Core.Execution;
using Polokus.Core.Externals;
using Polokus.Core.Helpers;
using Polokus.Core.Hooks;
using Polokus.Core.Interfaces;
using Polokus.Core.Models;
using Polokus.Core.Models.BpmnObjects.Xsd;
using Polokus.Core.NodeHandlers;
using Polokus.Core.Scripting;
using Polokus.Monitors.FileMonitor;
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
            = new Dictionary<string,IWorkflow>();

        public ISettingsProvider SettingsProvider { get; set; }

        public IHooksManager? HooksManager { get; set; } = new HooksManager();

        public Externals.Externals? Externals { get; }

        public ICollection<FileMonitor> FileMonitors { get; }
            = new List<FileMonitor>();


        private Dictionary<string, Logger> _logs = new();

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



        public PolokusMaster()
        {
            Externals = ExternalsManager.TryLoadExternals("./externals.json");
            if (Externals != null)
            {


                if (Externals.HooksProvider != null)
                {
                    // TODO: wczytywanie z externalsow WIELU hooks providerow
                    //HooksManager = ExternalsManager.InstantiateHooksProvider(Externals);
                    
                }

                if (Externals.SettingsProvider != null)
                {
                    SettingsProvider = ExternalsManager.InstantiateSettingsProvider(Externals);
                }
                {

                }
            }

            if (SettingsProvider == null)
            {
                SettingsProvider = new DefaultSettingsProvider();
            }
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
            RegisterWaiters(workflow);

            AddWorkflow(bpmnWorkflowName, workflow);
        }

        private void RegisterWaiters(Workflow workflow)
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

                    }

                }

            }
        }



    }
}
