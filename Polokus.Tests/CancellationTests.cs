using Polokus.Core.Hooks;
using Polokus.Core.Interfaces;
using Polokus.Core.Models.BpmnObjects.Xsd;
using Polokus.Core.Models;
using Polokus.Core.NodeHandlers;
using Polokus.Core.Scripting;
using Polokus.Core;
using Polokus.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;

namespace Polokus.Tests
{
    public class CancellationTests
    {
        const string FailureMsg = "Action should be cancelled but wasn't.";

        private class CustomServiceTaskNodeHandler : ServiceTaskNodeHandler
        {
            public static int Test1 { get; private set; } = 1;

            public CustomServiceTaskNodeHandler(ProcessInstance processInstance, FlowNode<tServiceTask> typedNode)
                : base(processInstance, typedNode)
            {
            }

            protected override async Task Action(INodeCaller? caller)
            {
                await Task.Delay(4000); // 4s
                CancellationToken.ThrowIfCancellationRequested(); // cancell further action
                Test1 = 2;
                throw new Exception(FailureMsg);
            }

        }


        [Test]
        public async Task DefaultCancellTest_StopProcess_KillProcessAndDoNotProcessFurther()
        {
            // Arrange
            var pi = BpmnLoader.LoadBpmnXmlIntoSimpleProcessInstance("delayTask.bpmn");
            pi.ContextInstance.NodeHandlerFactory
                .RegisterNodeHandlerForServiceTask<CustomServiceTaskNodeHandler>("DelayTask");

            // Act
            new Thread(() => 
            {
                Thread.Sleep(2000); // after 2 sec cancell running tasks
                pi.Stop();
            }).Start();

            await pi.RunSimple();

            // Assert
            Assert.AreEqual(1, CustomServiceTaskNodeHandler.Test1); // value not changed
            Assert.AreEqual("", Logger.Global.GetFullLog()); // exception not thrown

        }

    }

}
