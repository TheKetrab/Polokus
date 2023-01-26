using Polokus.Core.Interfaces.Execution.NodeHandlers;

namespace Polokus.ExternalsExample
{
    public class MyCustomServiceTaskNodeHandler : ServiceTaskNodeHandlerImpl
    {
        public MyCustomServiceTaskNodeHandler(INodeHandler parent) : base(parent)
        {
        }

        public override Task Run()
        {
            const string path = @"C:\Custom\BPMN\Polokus\Examples\mycustomservice.txt";
            using (var fs = File.OpenWrite(path))
            {
                using (var writer = new StreamWriter(fs))
                {
                    writer.WriteLine($"Hello world! Now is: {DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}");
                }
            }

            return Task.CompletedTask;
        }
    }
}