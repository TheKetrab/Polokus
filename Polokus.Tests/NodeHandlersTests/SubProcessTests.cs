using Polokus.Core.Hooks;
using Polokus.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Tests.NodeHandlersTests
{
    public class SubProcessTests
    {
        [Test]
        public async Task SubProcessTest_SubProcessExpanded_Success()
        {
            // Arrange
            var visitor = new VisitorHooks(VisitTime.BeforeExecute);
            var pi = BpmnLoader.LoadBpmnXmlIntoSimpleProcessInstance(Resources.SubProcess1);

            // Act
            await pi.RunSimple(visitor);

            // Assert
            Assert.That(visitor.GetResult(), Is.EqualTo("startA;taskA1;taskA2;startB;taskB1;endB;taskA3;endA"));

        }

        [Test]
        public async Task SubProcessTest_SubProcessCollapsedNested_Success()
        {
            // Arrange
            var visitor = new VisitorHooks(VisitTime.BeforeExecute);
            var pi = BpmnLoader.LoadBpmnXmlIntoSimpleProcessInstance(Resources.SubProcess2);

            // Act
            await pi.RunSimple(visitor);

            // Assert
            Assert.That(visitor.GetResult(), Is.EqualTo("startA;taskA;startB;taskB;startC;taskC;startD;taskD;endD;endC;endB;endA"));

        }


        [Test]
        public async Task SubProcessTest_SubProcessExpandedNested_Success()
        {
            // Arrange
            var visitor = new VisitorHooks(VisitTime.BeforeExecute);
            var pi = BpmnLoader.LoadBpmnXmlIntoSimpleProcessInstance(Resources.SubProcess3);

            // Act
            await pi.RunSimple(visitor);

            // Assert
            Assert.That(visitor.GetResult(), Is.EqualTo("startA;;startB;;startC;;startD;taskD;endD;endC;endB;endA"));

        }


    }
}
