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
    public class TimerEventNodeHandlerTests
    {

        [Test]
        public async Task TimerEventNodeHandler_Wait3s()
        {
            // Arrange
            var visitor = new VisitorHooks(VisitTime.BeforeExecute | VisitTime.PutNameInParenthesis | VisitTime.MarkNameForSpecialNodes);
            var pi = BpmnLoader.LoadBpmnXmlIntoSimpleProcessInstance(Resources.TimerEvent1);

            // Act
            await pi.RunSimple(visitor);

            // Assert
            Assert.That(visitor.GetResult(), Is.EqualTo("start;taskA;tIntermediateCatchEvent(3s);taskB;end"));
            Assert.IsTrue(pi.StatusManager.TotalTime > TimeSpan.FromSeconds(3) && pi.StatusManager.TotalTime < TimeSpan.FromSeconds(4));

        }


        [Test]
        public async Task TimerEventNodeHandler_OrderOfInvocation()
        {
            // Arrange
            var visitor = new VisitorHooks(VisitTime.BeforeExecute | VisitTime.MarkNameForSpecialNodes);
            var pi = BpmnLoader.LoadBpmnXmlIntoSimpleProcessInstance(Resources.TimerEvent2);

            // Act
            await pi.RunSimple(visitor);

            // Assert
            CustomAsserts.MatchAnyRegex(visitor.GetResult(),
                "start;par1;taskA;par2;tIntermediateCatchEvent;taskB;par2;end",
                "start;par1;tIntermediateCatchEvent;taskA;par2;taskB;par2;end",
                "start;par1;taskA;tIntermediateCatchEvent;par2;taskB;par2;end");

        }

        [Test]
        public async Task TimerEventNodeHandler_Cron()
        {
            // Arrange
            var visitor = new VisitorHooks(VisitTime.StartNewSequence);
            var pi = BpmnLoader.LoadBpmnXmlIntoSimpleProcessInstance(Resources.TimerEvent3);

            // Act
            await pi.RunSimple(visitor);

            // Assert
            Assert.IsTrue(DateTime.Now.Second % 10 == 0);
            CustomAsserts.MatchRegex(
                visitor.GetResult(),
                @"^null;Waiter_\(pr\)_\(pi.*\)_\(Process_0ez9rd5\)_\(Event_0sy3nhf\)$");

        }


    }
}
