using Polokus.Service;
using Topshelf;

Console.WriteLine("Hello, World!");


var exitCode = HostFactory.Run(x =>
{
    x.Service<PolokusService>(s =>
    {
        s.ConstructUsing(polokus => new PolokusService());
        s.WhenStarted(polokus => polokus.Start());
        s.WhenStopped(polokus => polokus.Stop());


    });

    x.RunAsLocalSystem();
    x.SetServiceName("PolokusService");
    x.SetDisplayName("Polokus Service");
    x.SetDescription("This service is an instance of BPMN 2.0 Polokus Engine that provides execution of BPMN Workflows.");

});

int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
Environment.ExitCode = exitCodeValue;
