﻿using Polokus.Core.Extensibility.Hooks;
using Polokus.Core.Interfaces.BpmnModels;
using Polokus.Core.Interfaces.Execution;
using Polokus.Tests.Helpers;

namespace Polokus.Tests.NodeHandlersTests
{
    public class TimerEventNodeHandlerTests : PolokusTestBase
    {

        [Test]
        public async Task TimerEventNodeHandler_Wait3s()
        {
            // Arrange
            var master = TestHelper.ReadBpmn(Resources.TimerEvent1,
                out IWorkflow wf, out IProcessInstance pi, out IFlowNode startNode);

            var visitor = new VisitorHooks(master, VisitTime.BeforeExecute | VisitTime.PutNameInParenthesis | VisitTime.MarkNameForSpecialNodes);
            master.HooksManager.RegisterHooksProvider(visitor);

            // Act
            await wf.RunProcessAsync(pi, startNode);

            // Assert
            Assert.That(visitor.GetResult(),
                Is.EqualTo("start;taskA;tIntermediateCatchEvent(3s);tIntermediateCatchEvent(3s);taskB;end"));
            Assert.IsTrue(
                pi.StatusManager.TotalTime > TimeSpan.FromSeconds(3)
                && pi.StatusManager.TotalTime < TimeSpan.FromSeconds(4));

        }


        [Test]
        public async Task TimerEventNodeHandler_OrderOfInvocation()
        {
            // Arrange
            var master = TestHelper.ReadBpmn(Resources.TimerEvent2,
                out IWorkflow wf, out IProcessInstance pi, out IFlowNode startNode);

            var visitor = new VisitorHooks(master,
                VisitTime.BeforeExecute | VisitTime.MarkNameForSpecialNodes | VisitTime.IgnoreWhenWaiterIsCalling);
            master.HooksManager.RegisterHooksProvider(visitor);

            // Act
            await wf.RunProcessAsync(pi, startNode);

            // Assert
            CustomAsserts.MatchAnyRegex(visitor.GetResult(),
                "start;par1;taskA;par2;tIntermediateCatchEvent;taskB;par2;end",
                "start;par1;taskA;tIntermediateCatchEvent;par2;taskB;par2;end",
                "start;par1;tIntermediateCatchEvent;taskA;par2;taskB;par2;end"
                );

        }

        [Test]
        public async Task TimerEventNodeHandler_Cron()
        {
            // Arrange
            var master = TestHelper.ReadBpmn(Resources.TimerEvent3,
                out IWorkflow wf, out IProcessInstance pi, out IFlowNode startNode);
            var visitor = new VisitorHooks(master, VisitTime.StartNewSequence);
            master.HooksManager.RegisterHooksProvider(visitor);

            // Act
            await wf.RunProcessAsync(pi, startNode);

            // Assert
            Assert.IsTrue(DateTime.Now.Second % 10 == 0);
            CustomAsserts.MatchRegex(
                visitor.GetResult(),
                @"^null;Waiter_\(WF\)_\(pi.*\)_\(Process_0ez9rd5\)_\(Event_0sy3nhf\)$");

        }


    }
}
