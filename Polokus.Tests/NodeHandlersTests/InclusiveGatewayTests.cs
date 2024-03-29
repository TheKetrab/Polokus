﻿using Polokus.Core.Extensibility.Hooks;
using Polokus.Core.Interfaces.BpmnModels;
using Polokus.Core.Interfaces.Execution;
using Polokus.Tests.Helpers;

namespace Polokus.Tests.NodeHandlersTests
{
    public class InclusiveGatewayTests : PolokusTestBase
    {
        [Test]
        public async Task InclusiveGatewayNodeHandler_BaseBehaviour_1()
        {
            // Arrange
            var master = TestHelper.ReadBpmn(Resources.Inclusive1,
                out IWorkflow wf, out IProcessInstance pi, out IFlowNode startNode);

            var visitor = new VisitorHooks(master, VisitTime.AfterExecuteSuccess | VisitTime.MarkNameForSpecialNodes);
            master.HooksManager.RegisterHooksProvider(visitor);

            // Act
            await wf.RunProcessAsync(pi, startNode);

            // Assert
            CustomAsserts.MatchAnyRegex(visitor.GetResult(),
                "start;tScriptTask;;positive;odd;;end",
                "start;tScriptTask;;odd;positive;;end");

        }
    }
}
