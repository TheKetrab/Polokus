using Polokus.Lib.Hooks;
using Polokus.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Tests.NodeHandlersTests
{
    public class ScriptTaskTests
    {
        [Test]
        public async Task ScriptTaskNodeHandler_1()
        {
            // Arrange
            var visitor = new VisitorHooks(VisitTime.OnExecute | VisitTime.IgnoreScriptTaskNames);
            var process = Utils.GetSingleProcessFromFile("scriptTask1.bpmn");
            ProcessInstance pi = new ProcessInstance(process, visitor);

            // Act
            await pi.RunProcess();

            // Assert
            Assert.AreEqual(720 + 222, pi.BpmnProcess.Context.ScriptProvider.Globals.globals["a"]);
            Assert.AreEqual("start;exclusive;end1",visitor.GetResult());

        }
    }
}
