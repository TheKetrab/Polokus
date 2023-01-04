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
    public class BoundaryEventsTests
    {
        [Test]
        public async Task BoundaryEvent_Exception()
        {
            // Arrange
            var master = TestHelper.ReadBpmn(Resources.ExceptionBoundaryEvent,
                out IWorkflow wf, out IProcessInstance pi, out IFlowNode startNode);

            var visitor = new VisitorHooks(master, VisitTime.BeforeExecute | VisitTime.MarkNameForSpecialNodes);
            master.HooksManager.RegisterHooksProvider(visitor);

            // Act
            await wf.RunProcessAsync(pi, startNode);

            // Assert
            Assert.AreEqual("start;tScriptTask;tBoundaryEvent;endB", visitor.GetResult());

        }

    }
}
