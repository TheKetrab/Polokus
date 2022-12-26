using Polokus.Core.Hooks;
using Polokus.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.ExternalsExample
{
    public class ExampleHooksProvider : EmptyHooksProvider
    {
        private string logPath = "./exampleHooksProviderLog.log";
        public override void AfterExecuteNodeSuccess(string wfId, string piId, string nodeId, int taskId)
        {
            File.AppendAllLines(logPath, new List<string>
                { $"{wfId, 50} | {piId, 50} | {nodeId, 30} | {taskId, 10}"});
        }

    }
}
