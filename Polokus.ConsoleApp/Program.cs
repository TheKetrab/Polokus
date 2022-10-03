using Polokus.Lib;


//var ctx = BpmnParser.ParseFile(@"C:\Custom\BPMN\Polokus\EXAMPLE\1\example1.bpmn");
var ctx = BpmnParser.ParseFile(@"C:\Custom\BPMN\Polokus\Polokus.Tests\NodeHandlersTests\Bpmn\exclusive1.bpmn");

Console.WriteLine(" ----- Process Graphs ----- ");
foreach (var process in ctx?.Processes)
{
    Console.WriteLine(process.GetSimpleGraph());
}

Console.WriteLine(" ----- Process Run ----- ");
foreach (var process in ctx.Processes)
{
    ProcessInstance pi = new ProcessInstance(process);
    if (await pi.RunProcess())
    {
        Console.WriteLine("Process finished successfully");
    }
    else
    {
        Console.WriteLine("Timeout...");
    }
}

Console.WriteLine();
Console.WriteLine(" ----- LOG ----- ");
Console.WriteLine(Logger.GetFullLog(true));

Console.ReadLine();