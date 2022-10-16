using Polokus.Core.Interfaces;
using Polokus.Core.Models;
using Polokus.Core.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core
{
    public class ContextsManager : IContextsManager
    {
        public IDictionary<string, IContextInstance> ContextInstances { get; } = new Dictionary<string, IContextInstance>();

        public ITimeManager TimeManager { get; } = new TimeManager();
        public IMessageManager MessageManager { get; } = new MessageManager();

        public void LoadXmlFile(string xmlFilePath, string? bpmnContextName = null)
        {
            bpmnContextName ??= new FileInfo(xmlFilePath).Name;

            BpmnParser parser = new BpmnParser();
            IBpmnContext bpmnContext = parser.ParseFile(xmlFilePath);

            var contextInstance = new ContextInstance(this, bpmnContext, bpmnContextName);
            ContextInstances.Add(bpmnContextName,contextInstance);
        }

        // secTimeout < 0 == no timeout
        public async Task<bool> RunContextManually(string name, int secTimeout = -1, IHooksProvider hooksProvider = null)
        {
            if (!ContextInstances.ContainsKey(name))
            {
                throw new Exception("Cannot find a context with given name.");
            }

            var bpmnProcess = ContextInstances[name].BpmnContext.BpmnProcesses.First();
            ProcessInstance instance = new ProcessInstance(ContextInstances.First().Value, bpmnProcess, hooksProvider);

            DateTime start = DateTime.Now;
            instance.Begin(bpmnProcess.GetStartNodes().First());
            while (instance.IsRunning())
            {
                await Task.Delay(100);

                if (secTimeout >= 0)
                {
                    DateTime tick = DateTime.Now;
                    TimeSpan span = tick - start;
                    if (span.Seconds > secTimeout)
                    {
                        hooksProvider?.OnTimeout();
                        return false;
                    }

                }
            }

            var end = DateTime.Now;

            Logger.Log($"Process finished. Time: {end - start}");

            return true;
        }


    }
}
