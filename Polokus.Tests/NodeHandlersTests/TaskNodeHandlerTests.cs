
using Polokus.Lib;

namespace Polokus.Tests.NodeHandlersTests
{
    public class TaskNodeHandlerTests
    {

        [Test]
        public async Task TaskNodeHandler_NormalSituation_Success()
        {
            // Arrange
            var logger = new EventLogger()
            {
                onNodeHandlerFinished = (sb, o, e) => sb.AppendLine(e.CurrentNode.Name)
            };

            var process = Utils.GetSingleProcessFromFile("task1.bpmn");
            ProcessInstance pi = new ProcessInstance(process,logger);

            // Act
            await pi.RunProcess();

            // Assert
            string n = Environment.NewLine;
            Assert.That(logger.GetResult(), Is.EqualTo($"start{n}task{n}end{n}"));

        }

        [Test]
        public async Task TaskNodeHandler_NormalSituation4Tasks_Success()
        {
            // Arrange
            var logger = new EventLogger()
            {
                onNodeHandlerFinished = (sb, o, e) => sb.AppendLine(e.CurrentNode.Name)
            };

            var process = Utils.GetSingleProcessFromFile("task2.bpmn");
            ProcessInstance pi = new ProcessInstance(process,logger);

            // Act
            await pi.RunProcess();

            // Assert
            string n = Environment.NewLine;
            Assert.That(logger.GetResult(), Is.EqualTo($"start{n}task1{n}task2{n}task3{n}task4{n}end{n}"));

        }

    }
}
