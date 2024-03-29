﻿using Polokus.Core.Extensibility.Hooks;
using Polokus.Core.Interfaces;
using Polokus.Core.Interfaces.BpmnModels;
using Polokus.Core.Interfaces.Execution;
using Polokus.Core.Interfaces.Execution.NodeHandlers;
using Polokus.Tests.Helpers;

namespace Polokus.Tests.NodeHandlersTests
{
    public class ServiceTaskTests : PolokusTestBase
    {
        private class CustomServiceTaskNodeHandler : ServiceTaskNodeHandlerImpl
        {
            public CustomServiceTaskNodeHandler(INodeHandler parent) : base(parent)
            {
            }

            public override async Task Run()
            {
                int x1 = await Parent.ProcessInstance.ScriptProvider.EvalScriptAsync<int>("$x = -1; return (int)$x;");
                int x2 = await Parent.ProcessInstance.ScriptProvider.EvalScriptAsync<int>("int y; y=(42+58) * (int)$x; $y = y; return y;");
                int x3 = await Parent.ProcessInstance.ScriptProvider.EvalScriptAsync<int>("return $y;");
            }
        }


        [Test]
        public async Task DefaultServiceTask()
        {
            // Arrange
            Settings.ScriptingLanguage = "CS";
            var master = TestHelper.ReadBpmn(Resources.ServiceTask1,
                out IWorkflow wf, out IProcessInstance pi, out IFlowNode startNode);

            var visitor = new VisitorHooks(master);
            master.HooksManager.RegisterHooksProvider(visitor);
            wf.NodeHandlerFactory.RegisterNodeHandlerForServiceTask
                <CustomServiceTaskNodeHandler>("CustomServiceTask");

            // Act
            await wf.RunProcessAsync(pi, startNode);

            // Assert
            Assert.AreEqual(2, pi.ScriptProvider.Globals.Values.Count);
            Assert.AreEqual("start;CustomServiceTask;exclusive;end2", visitor.GetResult());


        }
    }
}
