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
    public class InclusiveGatewayNodeHandlerTests
    {
        [Test]
        public async Task InclusiveGatewayNodeHandler_BaseBehaviour_1()
        {
            // Arrange
            var visitor = new VisitorHooks(VisitTime.AfterExecuteSuccess | VisitTime.MarkNameForSpecialNodes);
            var pi = BpmnLoader.LoadBpmnXmlIntoSimpleProcessInstance("inclusive1.bpmn");

            // Act
            await pi.RunSimple(visitor);

            // Assert
            CustomAsserts.MatchAnyRegex(visitor.GetResult(),
                "start;tScriptTask;;positive;odd;;end",
                "start;tScriptTask;;odd;positive;;end");

        }
    }
}
