using Polokus.Core.Execution;
using Polokus.Core.Helpers;
using Polokus.Core.Hooks;
using Polokus.Core.Interfaces;
using Polokus.Core.Models;
using Polokus.Core.Models.BpmnObjects.Xsd;
using Polokus.Core.NodeHandlers;
using Polokus.Tests.Helpers;

namespace Polokus.Tests
{
    [NonParallelizable]
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

            public override async Task Action(INodeCaller? caller)
            {
                await Task.Delay(2000); // 2s
                CancellationToken.ThrowIfCancellationRequested(); // cancell further action
                Test1 = 2;
                throw new InvalidCastException(FailureMsg);
            }

        }


        [Test]
        public async Task DefaultCancellTest_StopProcess_KillProcessAndDoNotProcessFurther()
        {
            // Arrange
            Logger.Global.ClearLog();
            var pi = BpmnLoader.LoadBpmnXmlIntoSimpleProcessInstance(Resources.DelayTask);
            pi.ContextInstance.NodeHandlerFactory
                .RegisterNodeHandlerForServiceTask<CustomServiceTaskNodeHandler>("DelayTask");

            // Act
            new Thread(() => 
            {
                Thread.Sleep(1000); // after 1 sec cancell running tasks
                pi.StatusManager.Stop();
            }).Start();

            await pi.RunSimple();
            Thread.Sleep(2000); // wait till end

            // Assert
            Assert.AreEqual(1, CustomServiceTaskNodeHandler.Test1); // value not changed
            Assert.AreEqual("", Logger.Global.GetFullLog()); // exception not thrown

        }

        private class CustomServiceTaskNodeHandler2 : ServiceTaskNodeHandler
        {
            public static int State { get; private set; } = 1;

            public CustomServiceTaskNodeHandler2(
                ProcessInstance processInstance, FlowNode<tServiceTask> typedNode)
                : base(processInstance, typedNode)
            {
            }

            public override async Task Action(INodeCaller? caller)
            {
                if (State == 1)
                {
                    State = 2;
                    await Task.Delay(2000); // 2s
                    CancellationToken.ThrowIfCancellationRequested(); // cancell further action
                    throw new Exception(FailureMsg);
                }
                else if (State == 2)
                {
                    // on the second call do not cancell, nor throw exception
                    State = 3;
                }
            }

        }

        [Test]
        public async Task DefaultCancellTest_PauseProcess()
        {
            // Arrange
            Logger.Global.ClearLog();
            var visitor = new VisitorHooks(VisitTime.BeforeExecute);
            var pi = BpmnLoader.LoadBpmnXmlIntoSimpleProcessInstance(Resources.DelayTask);
            pi.ContextInstance.NodeHandlerFactory
                .RegisterNodeHandlerForServiceTask<CustomServiceTaskNodeHandler2>("DelayTask");

            // Act
            new Thread(() =>
            {
                Thread.Sleep(1000); // after 1 sec cancell running tasks
                pi.StatusManager.Pause();

                Assert.AreEqual(0, pi.ActiveTasksManager.GetNodeHandlers().Count());
                Assert.AreEqual(1, pi.ActiveTasksManager.GetPausedNodeHandlers().Count());

                pi.StatusManager.Resume();

            }).Start();

            await pi.RunSimple(visitor);
            Thread.Sleep(3000); // wait till end

            // Assert
            Assert.AreEqual(3, CustomServiceTaskNodeHandler2.State);
            Assert.AreEqual("", Logger.Global.GetFullLog()); // exception not thrown
            Assert.AreEqual("start;DelayTask;DelayTask;end", visitor.GetResult()); // 'DelayTask' called two times
        }

    }

}
