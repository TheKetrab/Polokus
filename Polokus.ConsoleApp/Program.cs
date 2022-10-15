using Polokus.Core;

var contextsManager = new ContextsManager();
contextsManager.LoadXmlFile(@"C:\Users\Bartlomiej.Grochowsk\Downloads\diagram (12).bpmn","CONTEXT");

if (await contextsManager.RunContextManually("CONTEXT"))
{
    Console.WriteLine("Process finished successfully");
}
else
{
    Console.WriteLine("Timeout...");
}

Console.WriteLine();
Console.WriteLine(" ----- LOG ----- ");
Console.WriteLine(Logger.GetFullLog(true));

Console.ReadLine();