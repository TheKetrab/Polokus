using Polokus.Lib.Hooks;
using Polokus.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Tests.NodeHandlersTests
{
    public class TimerEventNodeHandlerTests
    {

        [Test]
        public async Task TimerEventNodeHandler_Wait3s()
        {
            // Arrange
            var visitor = new VisitorHooks(VisitTime.OnExecute | VisitTime.PutNameInParenthesis | VisitTime.MarkNameForSpecialNodes);
            var process = Utils.GetSingleProcessFromFile("timerEvent1.bpmn");
            ProcessInstance pi = new ProcessInstance(process, visitor);

            // Act
            var start = DateTime.Now;
            await pi.RunProcess();
            var end = DateTime.Now;

            var duration = end - start;

            // Assert
            Assert.That(visitor.GetResult(), Is.EqualTo("start;taskA;tIntermediateCatchEvent(3s);taskB;end"));
            Assert.IsTrue(duration > TimeSpan.FromSeconds(3) && duration < TimeSpan.FromSeconds(4));

        }


        [Test]
        public async Task TimerEventNodeHandler_OrderOfInvocation()
        {
            // Arrange
            var visitor = new VisitorHooks(VisitTime.OnExecute | VisitTime.MarkNameForSpecialNodes);
            var process = Utils.GetSingleProcessFromFile("timerEvent2.bpmn");
            ProcessInstance pi = new ProcessInstance(process, visitor);

            // Act
            await pi.RunProcess();

            // Assert
            CustomAsserts.MatchAnyRegex(visitor.GetResult(),
                "start;par1;taskA;par2;tIntermediateCatchEvent;taskB;par2;end",
                "start;par1;tIntermediateCatchEvent;taskA;par2;taskB;par2;end");

        }

    }
}
