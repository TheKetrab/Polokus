using Moq;
using Polokus.Lib;
using Polokus.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Tests.NodeHandlersTests
{
    public class InclusiveNodeHandlerTests
    {
        [Test]
        public async Task InclusiveNodeHandler_HandlerExecuted_2Times()
        {
            // Arrange
            int cnt = 0;
            var hooksMock = new Mock<IHooksProvider>();
            hooksMock.Setup(x => x.OnExecute(It.IsAny<FlowNode>(),It.IsAny<int>(),It.IsAny<string>()))
                .Callback((FlowNode n, int i, string s) => { if (n.Name == "inclusive") cnt++; });

            var process = Utils.GetSingleProcessFromFile("inclusive1.bpmn");
            ProcessInstance pi = new ProcessInstance(process, hooksMock.Object);

            // Act
            bool success = await pi.RunProcess(10);

            // Assert
            Assert.That(success, Is.EqualTo(true));
            Assert.That(cnt, Is.EqualTo(2));

        }
    }
}
