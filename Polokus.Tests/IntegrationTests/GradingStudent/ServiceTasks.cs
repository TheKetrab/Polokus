using Polokus.Core.Interfaces.Execution.NodeHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Polokus.Tests.IntegrationTests.GradingStudent
{
    public class FindWorstStudents : ServiceTaskNodeHandlerImpl
    {
        public FindWorstStudents(INodeHandler parent) : base(parent)
        {
        }

        public override Task Run()
        {
            string path = GradingStudentIntegrationTest.FilePath;
            var students = StudentsStore.ReadStudentsStore(path);

            var worstStudents = students
                .OrderBy(x => (x.G1 + x.G2 + x.G3) / 3)
                .Take(3)
                .ToArray();
            
            Variables.SetValue("x", worstStudents[0].Name);
            Variables.SetValue("y", worstStudents[1].Name);
            Variables.SetValue("z", worstStudents[2].Name);

            return Task.CompletedTask;
        }
    }

    public class ChooseAStudent : ServiceTaskNodeHandlerImpl
    {
        public ChooseAStudent(INodeHandler parent) : base(parent)
        {
        }

        public override Task Run()
        {
            int id = Random.Shared.Next(3);
            string[] names = new string[3] 
            {
                (string)Variables.GetValue("x"),
                (string)Variables.GetValue("y"),
                (string)Variables.GetValue("z")
            };

            Variables.SetValue("student", names[id]);
            return Task.CompletedTask;
        }
    }

    public class ChooseAQuestion : ServiceTaskNodeHandlerImpl
    {
        public ChooseAQuestion(INodeHandler parent) : base(parent)
        {
        }

        public override Task Run()
        {
            Parent.ProcessInstance.ScriptProvider.Globals
                .SetValue("question", Random.Shared.Next(5));
            return Task.CompletedTask;
        }
    }

    public class AnswerTheQuestion : ServiceTaskNodeHandlerImpl
    {
        public AnswerTheQuestion(INodeHandler parent) : base(parent)
        {
        }

        public async override Task Run()
        {
            string studentName = (string)Variables.GetValue("student");
            int question = (int)Variables.GetValue("question");

            var students = StudentsStore.ReadStudentsStore(GradingStudentIntegrationTest.FilePath);
            var s = students.First(s => s.Name == studentName);

            var avg = (s.G1 + s.G2 + s.G3) / 3;

            await Task.Delay(1000); // THINKING

            // student clever enough to answer?
            string answer = (avg < question) ? "KO" : "OK";

            if (!Parent.CancellationTokenSource.IsCancellationRequested)
            {
                Variables.SetValue("answer", answer);
            }

        }
    }

    public class ValidateAnswer : ServiceTaskNodeHandlerImpl
    {
        public ValidateAnswer(INodeHandler parent) : base(parent)
        {
        }

        public override Task Run()
        {
            string answer = (string)Variables.GetValue("answer");

            if (answer == "OK")
            {
                Variables.SetValue("ok", true);
            }
            else
            {
                Variables.SetValue("ok", false);
            }

            return Task.CompletedTask;
        }
    }

    public abstract class Rate : ServiceTaskNodeHandlerImpl
    {
        protected abstract int Grade { get; }

        public Rate(INodeHandler parent) : base(parent)
        {
        }

        public override Task Run()
        {
            string path = GradingStudentIntegrationTest.FilePath;

            string studentName = (string)Variables.GetValue("student");
            var students = StudentsStore.ReadStudentsStore(path);
            var s = students.First(x => x.Name == studentName);

            s.G4 = Grade;

            StudentsStore.SaveStudentsStore(students, path);

            return Task.CompletedTask;
        }

    }

    public class Rate5 : Rate
    {
        public Rate5(INodeHandler parent) : base(parent)
        {
        }

        protected override int Grade => 5;
    }

    public class Rate2 : Rate
    {
        public Rate2(INodeHandler parent) : base(parent)
        {
        }

        protected override int Grade => 2;
    }
}
