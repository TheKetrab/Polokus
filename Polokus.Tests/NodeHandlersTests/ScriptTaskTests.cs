using Polokus.Core.Hooks;
using Polokus.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polokus.Tests.Helpers;

namespace Polokus.Tests.NodeHandlersTests
{
    public class ScriptTaskTests
    {
        [Test]
        public async Task ScriptTaskNodeHandler_1()
        {
            // Arrange
            var visitor = new VisitorHooks(VisitTime.BeforeExecute | VisitTime.MarkNameForSpecialNodes);
            var pi = BpmnLoader.LoadBpmnXmlIntoSimpleProcessInstance(Resources.ScriptTask1);

            // Act
            await pi.RunSimple(visitor);

            // Assert
            Assert.AreEqual(720 + 222, pi.Workflow.ScriptProvider.Globals.globals["a"]);
            Assert.AreEqual("start;tScriptTask;tScriptTask;exclusive;end1", visitor.GetResult());

        }
    }
}
