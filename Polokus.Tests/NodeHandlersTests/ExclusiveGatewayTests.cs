using Polokus.Core.Extensibility.Hooks;
using Polokus.Core.Interfaces.BpmnModels;
using Polokus.Core.Interfaces.Execution;
using Polokus.Tests.Helpers;

namespace Polokus.Tests.NodeHandlersTests
{
    public class ExclusiveGatewayTests
    {
        [Test]
        public async Task ExclusiveGateway_BaseBehaviour_Ok()
        {
            // Arrange
            var master = TestHelper.ReadBpmn(Resources.Exclusive1,
                out IWorkflow wf, out IProcessInstance pi, out IFlowNode startNode);

            var visitor = new VisitorHooks(master, VisitTime.AfterExecuteSuccess | VisitTime.MarkNameForSpecialNodes);
            master.HooksManager.RegisterHooksProvider(visitor);

            // Act
            await wf.RunProcessAsync(pi, startNode);

            // Assert
            Assert.AreEqual("start;tScriptTask;exclusive;task2;end2", visitor.GetResult());

        }

        [Test]
        public async Task ExclusiveGateway_DefaultSequence_Ok()
        {
            // Arrange
            var master = TestHelper.ReadBpmn(Resources.Exclusive2,
                out IWorkflow wf, out IProcessInstance pi, out IFlowNode startNode);

            var visitor = new VisitorHooks(master, VisitTime.AfterExecuteSuccess | VisitTime.MarkNameForSpecialNodes);
            master.HooksManager.RegisterHooksProvider(visitor);

            // Act
            await wf.RunProcessAsync(pi, startNode);

            // Assert
            Assert.AreEqual("start;tScriptTask;exclusive;end3", visitor.GetResult());

        }

    }
}
