using Polokus.Core.Interfaces.BpmnModels;
using Polokus.Core.Interfaces.Execution;
using Polokus.Tests.Helpers;

namespace Polokus.Tests.IntegrationTests.GradingStudent
{
    public class GradingStudentIntegrationTest
    {
        private static string? _filePath;
        public static string FilePath => _filePath ??= Path.GetTempFileName();

        [SetUp]
        public void Init()
        {
            File.Create(FilePath).Close();
            StudentsStore.CreateStudentsStore(FilePath);
        }

        [TearDown]
        public void Cleanup()
        {
            File.Delete(FilePath);
            _filePath = null;
        }


        [Test]
        public async Task Test()
        {
            // Arrange
            var master = TestHelper.ReadBpmn(Resources.GradingStudent,
                out IWorkflow wf, out IProcessInstance pi, out IFlowNode startNode);

            wf.NodeHandlerFactory.RegisterNodeHandlerForServiceTask
                <FindWorstStudents>("Get 3 Worst Students");
            wf.NodeHandlerFactory.RegisterNodeHandlerForServiceTask
                <ChooseAStudent>("Choose a student");
            wf.NodeHandlerFactory.RegisterNodeHandlerForServiceTask
                <ChooseAQuestion>("Choose a question");
            wf.NodeHandlerFactory.RegisterNodeHandlerForServiceTask
                <AnswerTheQuestion>("Answer the question");
            wf.NodeHandlerFactory.RegisterNodeHandlerForServiceTask
                <ValidateAnswer>("Validate answer");
            wf.NodeHandlerFactory.RegisterNodeHandlerForServiceTask
                <Rate5>("Rate 5");
            wf.NodeHandlerFactory.RegisterNodeHandlerForServiceTask
                <Rate2>("Rate 2");


            // Act
            await wf.RunProcessAsync(pi, startNode);

            // Assert
            var students = StudentsStore.ReadStudentsStore(FilePath);
            var globals = pi.ScriptProvider.Globals;
            string studentName = (string)globals.GetValue("student");
            bool ok = (bool)globals.GetValue("ok");
            var s = students.First(x => x.Name == studentName);

            int expectedGrade = ok ? 5 : 2;
            Assert.AreEqual(expectedGrade, s.G4);

        }

    }
}
