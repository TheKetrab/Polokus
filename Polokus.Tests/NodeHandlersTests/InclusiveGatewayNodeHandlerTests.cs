using Polokus.Lib.Hooks;
using Polokus.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Tests.NodeHandlersTests
{
    public class InclusiveGatewayNodeHandlerTests
    {
        [Test]
        public async Task InclusiveGatewayNodeHandler_BaseBehaviour_1()
        {
            // Arrange
            var visitor = new VisitorHooks(VisitTime.OnFinished | VisitTime.IgnoreScriptTaskNames | VisitTime.NoteScriptTaskNamesAsScript);
            var process = Utils.GetSingleProcessFromFile("inclusive1.bpmn");
            ProcessInstance pi = new ProcessInstance(process, visitor);

            // Act
            await pi.RunProcess();

            // Assert
            CustomAsserts.MatchAnyRegex(visitor.GetResult(),
                "start;script;;positive;odd;;end",
                "start;script;;odd;positive;;end");

        }
    }
}
