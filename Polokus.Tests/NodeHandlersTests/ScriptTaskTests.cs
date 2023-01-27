using Polokus.Core.Extensibility.Hooks;
using Polokus.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polokus.Tests.Helpers;
using Polokus.Core.Interfaces;
using Polokus.Core.Interfaces.BpmnModels;
using Polokus.Core.Interfaces.Execution;

namespace Polokus.Tests.NodeHandlersTests
{
    public class ScriptTaskTests
    {
        [Test]
        public async Task ScriptTaskNodeHandler_1()
        {
            // Arrange
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
