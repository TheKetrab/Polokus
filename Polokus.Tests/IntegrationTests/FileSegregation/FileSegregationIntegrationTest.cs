using Polokus.Core.Interfaces.Execution;
using Polokus.ExternalsExample.FileMonitoring;
using Polokus.Tests.Helpers;

namespace Polokus.Tests.IntegrationTests.FileSegregation
{
    public class FileSegregationIntegrationTest : PolokusTestBase
    {
        private static string? _mainPath;
        public static string MainPath => _mainPath ??= Path.Combine(Path.GetTempPath(), "PolokusTestFileSegregation");

        public static string ObservedPath;
        public static string LongPath;
        public static string ShortPath;


        [SetUp]
        public void Init()
        {
            ObservedPath = Directory.CreateDirectory(Path.Combine(MainPath, "Observed")).FullName;
            LongPath = Directory.CreateDirectory(Path.Combine(MainPath, "Long")).FullName;
            ShortPath = Directory.CreateDirectory(Path.Combine(MainPath, "Short")).FullName;
        }

        [TearDown]
        public void Cleanup()
        {
            Directory.Delete(MainPath, true);
        }


        [Test]
        public async Task Test()
        {
            // Arrange
            var master = TestHelper.ReadBpmnWithoutPi(Resources.FileSegregation, out IWorkflow wf);

            wf.NodeHandlerFactory.RegisterNodeHandlerForServiceTask
                <MeasureFile>("Measure file");
            wf.NodeHandlerFactory.RegisterNodeHandlerForServiceTask
                <MoveToLong>("Move to long");
            wf.NodeHandlerFactory.RegisterNodeHandlerForServiceTask
                <MoveToShort>("Move to short");

            var fileMonitor = new FileMonitor(master, ObservedPath);
            fileMonitor.StopMonitoring();
            master.RegisterMonitor(fileMonitor);

            // Act
            for (int i = 0; i < 200; i += 2)
            {
                string fileName = $"f{i}.txt";
                string filePath = Path.Combine(MainPath, fileName);
                using (var fs = File.Create(filePath))
                {
                    using (var sw = new StreamWriter(fs))
                    {
                        sw.Write(new String('x', i));
                    }
                }
                File.Move(filePath, Path.Combine(ObservedPath, fileName));
            }

            // Assert
            await Task.Delay(10000);
            for (int i = 0; i <= 100; i += 2)
            {
                Assert.IsTrue(File.Exists(Path.Combine(ShortPath, $"f{i}.txt")));
            }
            for (int i = 102; i < 200; i += 2)
            {
                Assert.IsTrue(File.Exists(Path.Combine(LongPath, $"f{i}.txt")));
            }


        }
    }
}
