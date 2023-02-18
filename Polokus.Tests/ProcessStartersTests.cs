using Moq;
using Polokus.Core;
using Polokus.Core.Extensibility.Hooks;
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
            var master = BpmnLoader.LoadBpmnXmlIntoMaster(Resources.MsgStart);
            var visitor = new VisitorHooks(master, VisitTime.AfterExecuteSuccess);
            master.HooksManager.RegisterHooksProvider(visitor);

            var wf = master.GetFirstWorkflow();

            await Task.Delay(1000);
            string listenerId = wf.MessageManager.GetStarters().First().Id;

            // Act
            await wf.MessageManager.PingListener(listenerId);
            await Task.Delay(1000);

            while (true)
            {
                await Task.Delay(100);
                var running = master.GetWorkflows().SelectMany(x => x.ProcessInstances.GetAll());
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
