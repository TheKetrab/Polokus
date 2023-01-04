
using Polokus.Core;
using Polokus.Core.Hooks;
using Polokus.Core.Interfaces;
using Polokus.Tests.Helpers;

namespace Polokus.Tests.NodeHandlersTests
{
    public class TaskNodeHandlerTests
    {

        [Test]
        public async Task TaskNodeHandler_NormalSituation_Success()
        {
            // Arrange
            var master = TestHelper.ReadBpmn(Resources.Task1,
                out IWorkflow wf, out IProcessInstance pi, out IFlowNode startNode);

            var visitor = new VisitorHooks(master);
            master.HooksManager.RegisterHooksProvider(visitor);

            // Act
            await wf.RunProcessAsync(pi, startNode);

            // Assert
            Assert.That(visitor.GetResult(), Is.EqualTo("start;task;end"));

        }

        [Test]
        public async Task TaskNodeHandler_NormalSituation4Tasks_Success()
        {
            // Arrange
            var master = TestHelper.ReadBpmn(Resources.Task2,
                out IWorkflow wf, out IProcessInstance pi, out IFlowNode startNode);
            var visitor = new VisitorHooks(master);
            master.HooksManager.RegisterHooksProvider(visitor);

            // Act
            await wf.RunProcessAsync(pi, startNode);

            // Assert
            Assert.That(visitor.GetResult(), Is.EqualTo($"start;task1;task2;task3;task4;end"));

        }

    }
}
