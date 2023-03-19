using Polokus.Core.Extensibility.Hooks;
using Polokus.Core.Interfaces;
using Polokus.Core.Interfaces.BpmnModels;
using Polokus.Core.Interfaces.Execution;
using Polokus.Tests.Helpers;

namespace Polokus.Tests.NodeHandlersTests
{
    public class ScriptTaskTests : PolokusTestBase
    {

        [Test]
        public async Task ScriptTaskNodeHandler_1()
        {
            // Arrange
            Settings.ScriptingLanguage = "CS";
            var master = TestHelper.ReadBpmn(Resources.ScriptTask1,
                out IWorkflow wf, out IProcessInstance pi, out IFlowNode startNode);

            var visitor = new VisitorHooks(master, VisitTime.BeforeExecute | VisitTime.MarkNameForSpecialNodes);
            master.HooksManager.RegisterHooksProvider(visitor);

            // Act
            await wf.RunProcessAsync(pi, startNode);

            // Assert
            Assert.AreEqual(720 + 222, pi.ScriptProvider.Globals.TryGetValue<int>("a"));
            Assert.AreEqual("start;tScriptTask;tScriptTask;exclusive;end1", visitor.GetResult());

        }
    }
}
