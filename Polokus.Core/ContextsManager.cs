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
    public class ContextsManager : IContextsManager
    {
        public IDictionary<string, IContextInstance> ContextInstances { get; } = new Dictionary<string, IContextInstance>();




        private void RegisterWaiters(ContextInstance contextInstance)
        {
            var allStartNodes = contextInstance.BpmnContext.BpmnProcesses.SelectMany(x => x.GetStartNodes());
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
                            var processStarter = new ProcessStarter(contextInstance, startFlowNode.BpmnProcess, startNode);
                            contextInstance.TimeManager.RegisterStarter(timeDefinition, processStarter);
                        }
                        else if (eventDefinition is tMessageEventDefinition)
                        {
                            var processStarter = new ProcessStarter(contextInstance, startFlowNode.BpmnProcess, startNode);
                            contextInstance.MessageManager.RegisterMessageListener(processStarter);
                        }

                    }

                }

            }
        }


        public void LoadXmlFile(string xmlFilePath,
            string? bpmnContextName = null,
            IHooksProvider? hooksProvider = null,
            ISettingsProvider? settingsProvider = null,
            Func<ContextInstance,IHooksProvider>? createHooksProviderFunc = null)
        {
            bpmnContextName ??= new FileInfo(xmlFilePath).Name;

            BpmnParser parser = new BpmnParser();
            IBpmnContext bpmnContext = parser.ParseFile(xmlFilePath);

            var contextInstance = new ContextInstance(this,
                bpmnContext, bpmnContextName,
                settingsProvider: settingsProvider,
                hooksProvider: hooksProvider);

            if (hooksProvider == null && createHooksProviderFunc != null)
            {
                hooksProvider = createHooksProviderFunc(contextInstance);
                contextInstance.SetHooksProvider(hooksProvider);
            }

            RegisterWaiters(contextInstance);

            ContextInstances.Add(bpmnContextName,contextInstance);
        }

        public async Task<bool> RunContextManually(string name, int secTimeout = -1, IHooksProvider? hooksProvider = null)
        {
            // TODO: ta metoda jest tylko w testach, trzeba ja dac gdzies indziej

            if (!ContextInstances.ContainsKey(name))
            {
                throw new Exception("Cannot find a context with given name.");
            }

            var contextInstance = ContextInstances[name];
            if (hooksProvider != null) 
            {
                if (contextInstance is ContextInstance ci)
                {
                    ci.SetHooksProvider(hooksProvider);
                }

            }

            var bpmnProcess = contextInstance.BpmnContext.BpmnProcesses.First();
            var startNode = bpmnProcess.GetStartNodes().First();

            var processInstance = contextInstance.CreateProcessInstance(bpmnProcess);
            return await contextInstance.RunProcessAsync(processInstance, startNode, secTimeout);
        }


    }
}
