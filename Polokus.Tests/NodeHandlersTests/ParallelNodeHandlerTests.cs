using Polokus.Core.Extensibility.Hooks;
using Polokus.Core.Interfaces.BpmnModels;
using Polokus.Core.Interfaces.Execution;
using Polokus.Tests.Helpers;

namespace Polokus.Tests.NodeHandlersTests
{
    public class ParallelNodeHandlerTests
    {
        //[Test]
        //public async Task ParallelNodeHandler_HandlerExecuted_2Times()
        //{
        //    // Arrange
        //    int cnt = 0;
        //    var hooksMock = new Mock<IHooksProvider>();
        //    hooksMock.Setup(x => x.BeforeExecuteNode(It.IsAny<string>(),It.IsAny<string>(),It.IsAny<string>(),It.IsAny<int>(),It.IsAny<string?>()))
        //        .Callback((string wf, string pi, IFlowNode n, int i, INodeCaller? c) => { if (n.Name == "parallel2") cnt++; });

        //    var pi = BpmnLoader.LoadBpmnXmlIntoSimpleProcessInstance(Resources.Parallel1);

        //    // Act
        //    var success = await pi.RunSimple(hooksProvider: hooksMock.Object, timeout: 10);

        //    // Assert
        //    Assert.That(success, Is.EqualTo(true));
        //    Assert.That(cnt, Is.EqualTo(2));

        //}

        [Test]
        public async Task ParallelNodeHandler_Multitenancy_CorrectOrder()
        {
            // Arrange
            var master = TestHelper.ReadBpmn(Resources.Parallel2,
                out IWorkflow wf, out IProcessInstance pi, out IFlowNode startNode);


            var visitor = new VisitorHooks(master, VisitTime.AfterExecuteSuccess);
            master.HooksManager.RegisterHooksProvider(visitor);

            // Act
            await wf.RunProcessAsync(pi, startNode);

            // Assert
            CustomAsserts.MatchRegex(visitor.GetResult(), "start;par1;(a;b|b;a);par2;(c|d|e|f|g|h|;)*;par3;end");
            string innerPart = visitor.GetResult().SubstringBetween("par2;", ";par3");
            CustomAsserts.GoodOrder(innerPart, "c", "f", "h");
            CustomAsserts.GoodOrder(innerPart, "d", "g");

        }
    }
}
