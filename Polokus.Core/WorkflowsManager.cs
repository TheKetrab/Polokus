using Polokus.Core.Execution;
using Polokus.Core.Interfaces;
using Polokus.Core.Models;
using Polokus.Core.Models.BpmnObjects.Xsd;
using Polokus.Core.NodeHandlers;
using Polokus.Core.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core
{
    public class WorkflowsManager : IWorkflowsManager
    {
        public IDictionary<string, IWorkflow> Workflows { get; }
            = new Dictionary<string, IWorkflow>();

        public void LoadXmlString(string xmlString,
            string bpmnWorkflowName,
            IHooksProvider? hooksProvider = null,
            ISettingsProvider? settingsProvider = null,
            Func<Workflow, IHooksProvider>? createHooksProviderFunc = null)
        {
            BpmnParser parser = new BpmnParser();
            IBpmnWorkflow bpmnWorkflow = parser.ParseBpmnXml(xmlString);

            var workflow = new Workflow(this,
                bpmnWorkflow, bpmnWorkflowName,
                settingsProvider: settingsProvider,
                hooksProvider: hooksProvider);

            if (hooksProvider == null && createHooksProviderFunc != null)
            {
                hooksProvider = createHooksProviderFunc(workflow);
                workflow.SetHooksProvider(hooksProvider);
            }

            RegisterWaiters(workflow);

            Workflows.Add(bpmnWorkflowName, workflow);
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
