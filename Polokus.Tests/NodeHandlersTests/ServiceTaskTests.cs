using Moq;
using Polokus.Lib.Hooks;
using Polokus.Lib.Models;
using Polokus.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polokus.Lib.Models.BpmnObjects.Xsd;
using Polokus.Lib.NodeHandlers;

namespace Polokus.Tests.NodeHandlersTests
{
    public class ServiceTaskTests
    {
        private class CustomServiceTaskNodeHandler : ServiceTaskNodeHandler
        {

            public CustomServiceTaskNodeHandler(FlowNode<tServiceTask> typedNode) : base(typedNode)
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
            var process = Utils.GetSingleProcessFromFile("serviceTask1.bpmn");
            ProcessInstance pi = new ProcessInstance(process, visitor);

            pi.NodeHandlersDictionary.SetNodeHandlerForServiceTask<CustomServiceTaskNodeHandler>("CustomServiceTask");

            // Act
            bool success = await pi.RunProcess();

            // Assert
            Assert.AreEqual(2, pi.BpmnProcess.Context.ScriptProvider.Globals.globals.Count);
            Assert.AreEqual("start;CustomServiceTask;exclusive;end2", visitor.GetResult());


        }
    }
}
