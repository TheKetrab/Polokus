using Polokus.Core.Interfaces.Execution.NodeHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Tests.IntegrationTests.FileSegregation
{
    public class MeasureFile : ServiceTaskNodeHandlerImpl
    {
        public MeasureFile(INodeHandler parent) : base(parent)
        {
        }

        public override Task Run()
        {
            string path = (string)Variables.GetValue("arg0");
            string str = File.ReadAllText(path);

            Variables.SetValue("len", str.Length);

            return Task.CompletedTask;
        }
    }

    public class MoveToLong : ServiceTaskNodeHandlerImpl
    {
        public MoveToLong(INodeHandler parent) : base(parent)
        {
        }

        public override Task Run()
        {
            string path = (string)Variables.GetValue("arg0");
            string fileName = Path.GetFileName(path);
            File.Move(path, Path.Combine(FileSegregationIntegrationTest.LongPath, fileName));

            return Task.CompletedTask;
        }
    }

    public class MoveToShort : ServiceTaskNodeHandlerImpl
    {
        public MoveToShort(INodeHandler parent) : base(parent)
        {
        }

        public override Task Run()
        {
            string path = (string)Variables.GetValue("arg0");
            string fileName = Path.GetFileName(path);
            File.Move(path, Path.Combine(FileSegregationIntegrationTest.ShortPath, fileName));

            return Task.CompletedTask;
        }
    }
}
