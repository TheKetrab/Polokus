using Moq;
using Polokus.Core;
using Polokus.Core.Hooks;
using Polokus.Core.Interfaces;
using Polokus.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Tests
{
    public class ProcessStartersTests
    {
        [Test]
        public async Task ProcessStarters_StartProcessViaMessage()
        {
            // Arrange
            var visitor = new VisitorHooks(VisitTime.AfterExecuteSuccess);
            var wfManager = BpmnLoader.LoadBpmnXmlIntoWorkflowsManager(Resources.MsgStart,visitor);
            await Task.Delay(1000);
            string listenerId = wfManager.GetFirstWorkflow().MessageManager.GetStarters().First().Id;

            // Act
            await wfManager.GetFirstWorkflow().MessageManager.PingListener(listenerId);
            await Task.Delay(1000);

            while (true)
            {
                await Task.Delay(100);
                var running = wfManager.GetWorkflows().SelectMany(x => x.ProcessInstances);
                if (!running.Any())
                {
                    break;
                }
            }

            // Assert
            Assert.AreEqual("msgStart;taskA;end", visitor.GetResult());

        }

    }
}
