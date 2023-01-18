using Moq;
using Polokus.Core.Hooks;
using Polokus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polokus.Core.NodeHandlers;
using Polokus.Core.Interfaces;
using Polokus.Tests.Helpers;
using Polokus.Core.Execution;
using Polokus.Core.Interfaces.Xsd;
using Polokus.Core.Interfaces.BpmnModels;
using Polokus.Core.Interfaces.NodeHandlers;
using Polokus.Core.Scripting;

namespace Polokus.Tests.NodeHandlersTests
{
    public class ServiceTaskTests
    {
        private class CustomServiceTaskNodeHandler : ServiceTaskNodeHandlerImpl
        {
            public CustomServiceTaskNodeHandler(INodeHandler parent) : base(parent)
            {
            }

            public override async Task Run()
            {
                int x1 = await Parent.Workflow.ScriptProvider.EvalCSharpScriptAsync<int>("$x = -1; return (int)$x;");
                int x2 = await Parent.Workflow.ScriptProvider.EvalCSharpScriptAsync<int>("int y; y=(42+58) * (int)$x; $y = y; return y;");
                int x3 = await Parent.Workflow.ScriptProvider.EvalCSharpScriptAsync<int>("return $y;");
            }
        }


        [Test]
        public async Task DefaultServiceTask()
        {
            // Arrange
            var master = TestHelper.ReadBpmn(Resources.ServiceTask1,
                out IWorkflow wf, out IProcessInstance pi, out IFlowNode startNode);

            var visitor = new VisitorHooks(master);
            master.HooksManager.RegisterHooksProvider(visitor);
            wf.NodeHandlerFactory.RegisterNodeHandlerForServiceTask
                <CustomServiceTaskNodeHandler>("CustomServiceTask");

            // Act
            await wf.RunProcessAsync(pi, startNode);

            // Assert
            Assert.AreEqual(2, wf.ScriptProvider.Globals.Values.Count);
            Assert.AreEqual("start;CustomServiceTask;exclusive;end2", visitor.GetResult());


        }
    }
}
