using Quartz.Impl;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Quartz.Logging.OperationName;
using Polokus.Core.Interfaces;

namespace Polokus.Core
{
    public class TimeManager : ITimeManager
    {
        public IContextsManager ContextsManager { get; }

        public TimeManager(ContextsManager contextsManager)
        {
            ContextsManager = contextsManager;
        }


        public async void method()
        {
            //// construct a scheduler factory using defaults
            //StdSchedulerFactory factory = new StdSchedulerFactory();

            //// get a scheduler
            //IScheduler scheduler = await factory.GetScheduler();
            //await scheduler.Start();

            //// define the job and tie it to our HelloJob class
            //IJobDetail job = JobBuilder.Create<HelloJob>()
            //    .WithIdentity("myJob", "group1")
            //    .Build();

            //// Trigger the job to run now, and then every 40 seconds
            //ITrigger trigger = TriggerBuilder.Create()
            //    .WithIdentity("myTrigger", "group1")
            //    .StartNow()
            //    .WithSimpleSchedule(x => x
            //        .WithIntervalInSeconds(40)
            //    .RepeatForever())
            //.Build();

            //await scheduler.ScheduleJob(job, trigger);

            //// You could also schedule multiple triggers for the same job with
            //// await scheduler.ScheduleJob(job, new List<ITrigger>() { trigger1, trigger2 }, replace: true);
        }
    }
}
