using Polokus.Core.Extensibility.Hooks;
using Polokus.Tests.Helpers;

namespace Polokus.Tests
{
    public class ProcessStartersTests : PolokusTestBase
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
