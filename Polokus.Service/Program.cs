using Polokus.Service;
using Topshelf;

PrintHelper.PrintHeader();

var exitCode = HostFactory.Run(x =>
{
    PrintHelper.PrintInfo("Initializing service...");
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

    PrintHelper.PrintInfo("Service initialized.");
});

int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
Environment.ExitCode = exitCodeValue;


