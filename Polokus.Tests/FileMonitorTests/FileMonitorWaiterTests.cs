using Polokus.Core.Hooks;
using Polokus.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Polokus.Tests.Helpers;
using Polokus.Core.Interfaces.Execution;
using Polokus.ExternalsExample.FileMonitoring;

namespace Polokus.Tests.FileMonitorTests
{
    public class FileMonitorWaiterTests
    {
        private string _dirToMonitor;

        [SetUp]
        public void Initialize()
        {
            string testPath = Assembly.GetExecutingAssembly().Location;
            _dirToMonitor = Path.Combine(testPath, "../../../..", "FileMonitorTests", "TestDirectoryToMonitorWaiters");
            Assert.IsTrue(Directory.Exists(_dirToMonitor));
        }

        [Test]
        public void FileMonitorWaiterTest_FileCreateSignal()
        {
            // Arrange
            PolokusMaster master = new PolokusMaster();

            VisitorHooks visitor = new VisitorHooks(master, VisitTime.BeforeExecute);
            master.HooksManager!.RegisterHooksProvider(visitor);
            master.LoadXmlString(Resources.SignalWaiter1, "SignalWaiter");

            var fileMonitor = new FileMonitor(master, _dirToMonitor);
            master.RegisterMonitor(fileMonitor);

            var wf = master.GetFirstWorkflow();
            string bpmnProcessId = wf.BpmnWorkflow.BpmnProcesses.First().Id;

            // Act
            wf.StartProcessManually(bpmnProcessId);
            Thread.Sleep(1000);

            string filePath = Path.Combine(_dirToMonitor, "file.txt");
            File.Create(filePath).Close();
            Thread.Sleep(1000);
            File.Delete(filePath);
            Thread.Sleep(1000);

            // Assert
            string visitorResult = visitor.GetResult();
            Assert.AreEqual("start;taskA;FileCreated;FileCreated;taskB;end", visitorResult);

        }
    }
}
