using Moq;
using Polokus.Core.Hooks;
using Polokus.Core.Models;
using Polokus.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polokus.Core.Models.BpmnObjects.Xsd;
using Polokus.Core.NodeHandlers;
using Polokus.Core.Interfaces;
using Polokus.Tests.Helpers;

namespace Polokus.Tests.NodeHandlersTests
{
    public class ServiceTaskTests
    {
        private class CustomServiceTaskNodeHandler : ServiceTaskNodeHandler
        {
            public CustomServiceTaskNodeHandler(ProcessInstance processInstance, FlowNode<tServiceTask> typedNode)
                : base(processInstance, typedNode)
            {
            }

            protected override async Task Action(IFlowNode? caller)
            {
                int x1 = await ScriptProvider.EvalCSharpScriptAsync<int>("$x = -1; return (int)$x;");
                int x2 = await ScriptProvider.EvalCSharpScriptAsync<int>("int y; y=(42+58) * (int)$x; $y = y; return y;");
                int x3 = await ScriptProvider.EvalCSharpScriptAsync<int>("return $y;");
            }

        }


        [Test]
        public async Task DefaultServiceTask()
        {
            // Arrange
            VisitorHooks visitor = new VisitorHooks();
            var pi = BpmnLoader.LoadBpmnXmlIntoSimpleProcessInstance("serviceTask1.bpmn");
            pi.ContextInstance.NodeHandlerFactory
                .RegisterNodeHandlerForServiceTask<CustomServiceTaskNodeHandler>("CustomServiceTask");

            // Act
            await pi.RunSimple(visitor);

            // Assert
            Assert.AreEqual(2, pi.ContextInstance.ScriptProvider.Globals.globals.Count);
            Assert.AreEqual("start;CustomServiceTask;exclusive;end2", visitor.GetResult());


        }
    }
}
