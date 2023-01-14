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

            public override async Task Action(INodeCaller? caller)
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
