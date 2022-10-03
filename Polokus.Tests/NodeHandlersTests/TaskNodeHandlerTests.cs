
using Polokus.Lib;
using Polokus.Lib.Hooks;

namespace Polokus.Tests.NodeHandlersTests
{
    public class TaskNodeHandlerTests
    {

        [Test]
        public async Task TaskNodeHandler_NormalSituation_Success()
        {
            // Arrange
            var visitor = new VisitorHooks();
            var process = Utils.GetSingleProcessFromFile("task1.bpmn");
            ProcessInstance pi = new ProcessInstance(process,visitor);

            // Act
            await pi.RunProcess();

            // Assert
            Assert.That(visitor.GetResult(), Is.EqualTo("start;task;end"));

        }

        [Test]
        public async Task TaskNodeHandler_NormalSituation4Tasks_Success()
        {
            // Arrange
            var visitor = new VisitorHooks();
            var process = Utils.GetSingleProcessFromFile("task2.bpmn");
            ProcessInstance pi = new ProcessInstance(process,visitor);

            // Act
            await pi.RunProcess();

            // Assert
            Assert.That(visitor.GetResult(), Is.EqualTo($"start;task1;task2;task3;task4;end"));

        }

    }
}
