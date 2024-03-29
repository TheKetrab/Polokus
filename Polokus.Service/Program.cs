﻿using log4net;
using log4net.Config;
using Polokus.Service;
using System.Reflection;
using Topshelf;

PrintHelper.PrintHeader();


var exitCode = HostFactory.Run(x =>
{
    PrintHelper.PrintInfo("Initializing service...");

    var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
    XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

    x.UseLog4Net();
    x.StartManually();
    x.RunAsNetworkService();

    x.Service<PolokusService>(s =>
        {
            s.ConstructUsing(polokus => new PolokusService());
            s.WhenStarted(polokus => polokus.Start());
            s.WhenStopped(polokus => polokus.Stop());


        });

    x.OnException((e) =>
        {
            using (var fs = File.Create(AppDomain.CurrentDomain.BaseDirectory + "errorlog.txt"))
            {
                using (var sw = new StreamWriter(fs))
                {
                    sw.WriteLine(e.Message);
                    sw.WriteLine(e.StackTrace);
                    while (e.InnerException != null)
                    {
                        e = e.InnerException;
                        sw.WriteLine("---");
                        sw.WriteLine(e.Message);
                        sw.WriteLine(e.StackTrace);
                    }
                }
            }
        });

    x.SetServiceName("PolokusService");
    x.SetDisplayName("Polokus Service");
    x.SetDescription("This service is an instance of BPMN 2.0 Polokus Engine that provides execution of BPMN Workflows.");

    PrintHelper.PrintInfo("Service initialized.");
});

int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
Environment.ExitCode = exitCodeValue;

