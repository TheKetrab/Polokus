using Polokus.Lib;


var ctx = BpmnParser.ParseFile(@"C:\Users\Bartlomiej.Grochowsk\Downloads\flow.bpmn");

Console.WriteLine(" ----- Process Graphs ----- ");
foreach (var process in ctx?.Processes)
{
    Console.WriteLine(process.GetSimpleGraph());
}

Console.WriteLine(" ----- Process Run ----- ");
foreach (var process in ctx.Processes)
{
    ProcessInstance pi = new ProcessInstance(process);
    pi.Start();
}

Console.WriteLine();
Console.WriteLine(" ----- LOG ----- ");
Console.WriteLine(Logger.GetFullLog(true));

Console.ReadLine();