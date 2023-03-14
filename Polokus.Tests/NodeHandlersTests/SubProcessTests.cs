using Polokus.Core.Extensibility.Hooks;
using Polokus.Core.Interfaces.BpmnModels;
using Polokus.Core.Interfaces.Execution;
using Polokus.Tests.Helpers;

namespace Polokus.Tests.NodeHandlersTests
{
    public class SubProcessTests
    {
        [Test]
        public async Task SubProcessTest_SubProcessExpanded_Success()
        {
            // Arrange
            var master = TestHelper.ReadBpmn(Resources.SubProcess1,
                out IWorkflow wf, out IProcessInstance pi, out IFlowNode startNode);

            var visitor = new VisitorHooks(master);
            master.HooksManager.RegisterHooksProvider(visitor);

            // Act
            await wf.RunProcessAsync(pi, startNode);

            // Assert
            Assert.That(visitor.GetResult(), Is.EqualTo("startA;taskA1;taskA2;startB;taskB1;endB;taskA3;endA"));

        }

        [Test]
        public async Task SubProcessTest_SubProcessCollapsedNested_Success()
        {
            // Arrange
            var master = TestHelper.ReadBpmn(Resources.SubProcess2,
                out IWorkflow wf, out IProcessInstance pi, out IFlowNode startNode);

            var visitor = new VisitorHooks(master);
            master.HooksManager.RegisterHooksProvider(visitor);

            // Act
            await wf.RunProcessAsync(pi, startNode);

            // Assert
            Assert.That(visitor.GetResult(), Is.EqualTo("startA;taskA;startB;taskB;startC;taskC;startD;taskD;endD;endC;endB;endA"));

        }


        [Test]
        public async Task SubProcessTest_SubProcessExpandedNested_Success()
        {
            // Arrange
            var master = TestHelper.ReadBpmn(Resources.SubProcess3,
                out IWorkflow wf, out IProcessInstance pi, out IFlowNode startNode);

            var visitor = new VisitorHooks(master);
            master.HooksManager.RegisterHooksProvider(visitor);

            // Act
            await wf.RunProcessAsync(pi, startNode);

            // Assert
            Assert.That(visitor.GetResult(), Is.EqualTo("startA;;startB;;startC;;startD;taskD;endD;endC;endB;endA"));
        }


    }
}
