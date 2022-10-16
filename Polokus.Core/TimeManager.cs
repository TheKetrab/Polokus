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
        StdSchedulerFactory factory = new StdSchedulerFactory();


        public TimeManager()
        {
        }
        
        public async void RegisterWaiter(string timeString, INodeHandlerWaiter waiter, bool oneTime)
        {
            IScheduler scheduler = await factory.GetScheduler();

            IJobDetail job = JobBuilder.Create<MyJob>().Build();
            job.JobDataMap.Add("OneTime", oneTime);
            job.JobDataMap.Add("Waiter", waiter);

            ITrigger trigger = TriggerBuilder.Create().WithCronSchedule(timeString).Build();

            await scheduler.ScheduleJob(job, trigger);
            await scheduler.Start();

            
        }
    }

    public class MyJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            bool oneTime = (bool)context.JobDetail.JobDataMap["OneTime"];
            INodeHandlerWaiter waiter = (INodeHandlerWaiter)context.JobDetail.JobDataMap["Waiter"];

            if (oneTime)
            {
                await context.Scheduler.DeleteJob(context.JobDetail.Key);
            }            

            waiter.Invoke();
        }
    }
}
