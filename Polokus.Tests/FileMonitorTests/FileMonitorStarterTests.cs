using Polokus.Core;
using Polokus.Core.Extensibility.Hooks;
using Polokus.ExternalsExample.FileMonitoring;
using System.Reflection;

namespace Polokus.Tests.FileMonitorTests
{
    public class FileMonitorStarterTests
    {
        private string _dirToMonitor;

        [SetUp]
        public void Initialize()
        {
            string testPath = Assembly.GetExecutingAssembly().Location;
            _dirToMonitor = Path.Combine(testPath, "../../../..", "FileMonitorTests", "TestDirectoryToMonitorStarters");
            Assert.IsTrue(Directory.Exists(_dirToMonitor));
        }

        [Test]
        public void FileMonitorStarterTest_FileCreateSignal()
        {
            // Arrange
            PolokusMaster master = new PolokusMaster();

            VisitorHooks visitor = new VisitorHooks(master, VisitTime.BeforeExecute);
            master.HooksManager!.RegisterHooksProvider(visitor);
            master.LoadXmlString(Resources.SignalProcessStarter1, "SignalProcessStarter");

            var fileMonitor = new FileMonitor(master, _dirToMonitor);
            master.RegisterMonitor(fileMonitor);

            // Act
            string filePath = Path.Combine(_dirToMonitor, "file.txt");
            File.Create(filePath).Close();
            Thread.Sleep(1000);
            File.Delete(filePath);
            Thread.Sleep(1000);

            // Assert
            string visitorResult = visitor.GetResult();
            Assert.AreEqual("FileCreated;taskA;end", visitorResult);

        }
    }
}
