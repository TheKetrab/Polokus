
using Polokus.Core;
using Polokus.Core.Hooks;
using Polokus.Tests.Helpers;

namespace Polokus.Tests.NodeHandlersTests
{
    public class TaskNodeHandlerTests
    {

        [Test]
        public async Task TaskNodeHandler_NormalSituation_Success()
        {
            // Arrange
            var visitor = new VisitorHooks();
            var pi = BpmnLoader.LoadBpmnXmlIntoSimpleProcessInstance(Resources.Task1);

            // Act
            await pi.RunSimple(visitor);

            // Assert
            Assert.That(visitor.GetResult(), Is.EqualTo("start;task;end"));

        }

        [Test]
        public async Task TaskNodeHandler_NormalSituation4Tasks_Success()
        {
            // Arrange
            var visitor = new VisitorHooks();
            var pi = BpmnLoader.LoadBpmnXmlIntoSimpleProcessInstance(Resources.Task2);

            // Act
            await pi.RunSimple(visitor);


            // Assert
            Assert.That(visitor.GetResult(), Is.EqualTo($"start;task1;task2;task3;task4;end"));

        }

    }
}
