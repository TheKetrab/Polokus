using Moq;
using Polokus.Lib;
using Polokus.Lib.Hooks;
using Polokus.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Tests.NodeHandlersTests
{
    public class ParallelNodeHandlerTests
    {
        [Test]
        public async Task ParallelNodeHandler_HandlerExecuted_2Times()
        {
            // Arrange
            int cnt = 0;
            var hooksMock = new Mock<IHooksProvider>();
            hooksMock.Setup(x => x.OnExecute(It.IsAny<IFlowNode>(),It.IsAny<int>(),It.IsAny<string>()))
                .Callback((IFlowNode n, int i, string s) => { if (n.Name == "parallel2") cnt++; });

            var process = Utils.GetSingleProcessFromFile("parallel1.bpmn");
            ProcessInstance pi = new ProcessInstance(process, hooksMock.Object);

            // Act
            bool success = await pi.RunProcess(10);

            // Assert
            Assert.That(success, Is.EqualTo(true));
            Assert.That(cnt, Is.EqualTo(2));

        }

        [Test]
        public async Task ParallelNodeHandler_Multitenancy_CorrectOrder()
        {
            // Arrange
            var visitor = new VisitorHooks(VisitTime.OnFinished);
            var process = Utils.GetSingleProcessFromFile("parallel2.bpmn");
            ProcessInstance pi = new ProcessInstance(process, visitor);

            // Act
            await pi.RunProcess();

            // Assert
            CustomAsserts.MatchRegex(visitor.GetResult(), "start;par1;(a;b|b;a);par2;(c|d|e|f|g|h|;)*;par3;end");
            string innerPart = Utils.SubstringBetween(visitor.GetResult(),"par2;",";par3");
            CustomAsserts.GoodOrder(innerPart, "c", "f", "h");
            CustomAsserts.GoodOrder(innerPart, "d", "g");

        }
    }
}
