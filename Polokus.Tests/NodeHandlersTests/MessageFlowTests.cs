using Polokus.Core.Hooks;
using Polokus.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Tests.NodeHandlersTests
{
    public class MessageFlowTests
    {
        [Test]
        public async Task MessageBetweenProcesses_Test()
        {
            // Arrange
            VisitorHooks visitor = new VisitorHooks(VisitTime.BeforeExecute);
            var pi = BpmnLoader.LoadBpmnXmlIntoSimpleProcessInstance("msgBetweenProcesses.bpmn");
            // todo: two processes loading, load as context instance?

            // Act
            await pi.RunSimple(visitor);

            // Assert
            Assert.AreEqual(2, pi.ContextInstance.ScriptProvider.Globals.globals.Count);
            Assert.AreEqual("start;CustomServiceTask;exclusive;end2", visitor.GetResult());

        }
    }
}
