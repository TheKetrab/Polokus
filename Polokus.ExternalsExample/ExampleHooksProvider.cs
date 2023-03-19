using Polokus.Core.Interfaces.Extensibility;

namespace Polokus.ExternalsExample
{
    public class ExampleHooksProvider : EmptyHooksProvider
    {
        private static object _lock = new object();

        private string logPath = "./Examples/exampleHooksProviderLog.log";
        public override void AfterExecuteNodeSuccess(string wfId, string piId, string nodeId, int taskId)
        {
            lock (_lock)
            {
                File.AppendAllLines(logPath, new List<string>
                    { $"{wfId, 50} | {piId, 50} | {nodeId, 30} | {taskId, 10}"});
            }
        }

    }
}
