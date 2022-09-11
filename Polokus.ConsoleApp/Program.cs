using Polokus.Lib.BpmnParser;


var ctx = BpmnParser.ParseFile(@"C:\Users\Bartlomiej.Grochowsk\Downloads\flow.bpmn");

Console.Write(ctx.Processs.First().GetSimpleGraph());

Console.ReadLine();