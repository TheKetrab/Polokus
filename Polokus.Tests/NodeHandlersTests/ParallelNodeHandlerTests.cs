using Moq;
using Polokus.Core;
using Polokus.Core.Hooks;
using Polokus.Core.Interfaces;
using Polokus.Tests.Helpers;
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
            hooksMock.Setup(x => x.BeforeExecuteNode(It.IsAny<IFlowNode>(),It.IsAny<int>(),It.IsAny<INodeCaller?>()))
                .Callback((IFlowNode n, int i, INodeCaller? c) => { if (n.Name == "parallel2") cnt++; });

            var pi = BpmnLoader.LoadBpmnXmlIntoSimpleProcessInstance("parallel1.bpmn");

            // Act
            var success = await pi.RunSimple(hooksProvider: hooksMock.Object, timeout: 10);

            // Assert
            Assert.That(success, Is.EqualTo(true));
            Assert.That(cnt, Is.EqualTo(2));

        }

        [Test]
        public async Task ParallelNodeHandler_Multitenancy_CorrectOrder()
        {
            // Arrange
            var visitor = new VisitorHooks(VisitTime.AfterExecuteSuccess);
            var pi = BpmnLoader.LoadBpmnXmlIntoSimpleProcessInstance("parallel2.bpmn");

            // Act
            await pi.RunSimple(visitor);

            // Assert
            CustomAsserts.MatchRegex(visitor.GetResult(), "start;par1;(a;b|b;a);par2;(c|d|e|f|g|h|;)*;par3;end");
            string innerPart = visitor.GetResult().SubstringBetween("par2;",";par3");
            CustomAsserts.GoodOrder(innerPart, "c", "f", "h");
            CustomAsserts.GoodOrder(innerPart, "d", "g");

        }
    }
}
