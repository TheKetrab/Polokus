using Polokus.Core;
using Polokus.Core.Interfaces;
using Polokus.Core.Models;
using Polokus.Core.Models.BpmnObjects.Xsd;
using Polokus.Core.NodeHandlers;

namespace Polokus.CustomServiceTaskExample
{
    public class MyCustomServiceTaskNodeHandler : ServiceTaskNodeHandler
    {
        public MyCustomServiceTaskNodeHandler(ProcessInstance processInstance,
            FlowNode<tServiceTask> typedNode) : base(processInstance, typedNode)
        {
        }

        public override Task Action(INodeCaller? caller)
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