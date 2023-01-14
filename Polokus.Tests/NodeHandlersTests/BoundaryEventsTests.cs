using Polokus.Core.Execution;
using Polokus.Core.Hooks;
using Polokus.Core.Interfaces;
using Polokus.Core.Interfaces.BpmnModels;
using Polokus.Core.Interfaces.Xsd;
using Polokus.Core.Models;
using Polokus.Core.NodeHandlers;
using Polokus.Core.Scripting;
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

        [Test]
        public async Task BoundaryEvent_Timer()
        {
            // Arrange
            var master = TestHelper.ReadBpmn(Resources.BoundaryEventTimer1,
                out IWorkflow wf, out IProcessInstance pi, out IFlowNode startNode);

            var visitor = new VisitorHooks(master, VisitTime.BeforeExecute | VisitTime.MarkNameForSpecialNodes);
            master.HooksManager.RegisterHooksProvider(visitor);

            // Act
            await wf.RunProcessAsync(pi, startNode);

            // Assert
            Assert.AreEqual(
                "start;subprocess;substart;tIntermediateCatchEvent;tBoundaryEvent;endB",
                visitor.GetResult());

        }

        private class CustomServiceTaskNodeHandler : ServiceTaskNodeHandler
        {
            public CustomServiceTaskNodeHandler(ProcessInstance processInstance, FlowNode<tServiceTask> typedNode)
                : base(processInstance, typedNode)
            {
            }

            public override async Task Action(INodeCaller? caller)
            {
                await Task.Delay(10000); // 10s
            }

        }


        [Test]
        public async Task BoundaryEvent_Timer_BreakingLongServiceTask()
        {
            // Arrange
            var master = TestHelper.ReadBpmn(Resources.BoundaryEventTimer2,
                out IWorkflow wf, out IProcessInstance pi, out IFlowNode startNode);

            var visitor = new VisitorHooks(master);
            master.HooksManager.RegisterHooksProvider(visitor);
            wf.NodeHandlerFactory.RegisterNodeHandlerForServiceTask
                <CustomServiceTaskNodeHandler>("LongTask");

            // Act
            await wf.RunProcessAsync(pi, startNode);

            // Assert
            Assert.AreEqual("startA;LongTask;2s;endB", visitor.GetResult());


        }


    }
}
