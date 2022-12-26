﻿using Quartz.Impl;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Quartz.Logging.OperationName;
using Polokus.Core.Interfaces;
using Polokus.Core.Hooks;

namespace Polokus.Core.Execution
{
    public class TimeManager : ITimeManager
    {
        StdSchedulerFactory factory = new StdSchedulerFactory();


        public TimeManager()
        {
        }

        Dictionary<string, IProcessStarter> _starters = new();
        Dictionary<string, INodeHandlerWaiter> _waiters = new();

        public IEnumerable<IProcessStarter> GetStarters()
        {
            return _starters.Values;
        }

        public IEnumerable<INodeHandlerWaiter> GetWaiters()
        {
            return _waiters.Values;
        }

        public async void RegisterStarter(string timeString, IProcessStarter starter)
        {
            IScheduler scheduler = await factory.GetScheduler();

            IJobDetail job = JobBuilder.Create<StarterJob>().Build();
            job.JobDataMap.Add("Starter", starter);
            job.JobDataMap.Add("TimeManager", this);

            ITrigger trigger = TriggerBuilder.Create().WithCronSchedule(timeString).Build();

            await scheduler.ScheduleJob(job, trigger);
            await scheduler.Start();

            _starters.Add(starter.Id, starter);
            starter.HooksProvider?.OnCallerChanged(starter.Id, nameof(CallerChangedType.StarterStartedProcess));
        }

        public async void RegisterWaiter(string timeString, INodeHandlerWaiter waiter, bool oneTime)
        {
            IScheduler scheduler = await factory.GetScheduler();

            IJobDetail job = JobBuilder.Create<WaiterJob>().Build();
            job.JobDataMap.Add("OneTime", oneTime);
            job.JobDataMap.Add("Waiter", waiter);
            job.JobDataMap.Add("TimeManager", this);

            ITrigger trigger = TriggerBuilder.Create().WithCronSchedule(timeString).Build();

            await scheduler.ScheduleJob(job, trigger);
            await scheduler.Start();

            _waiters.Add(waiter.Id, waiter);
            waiter.HooksProvider?.OnCallerChanged(waiter.Id, nameof(CallerChangedType.WaiterInserted));
        }

        public void RemoveWaiter(INodeHandlerWaiter waiter)
        {
            _waiters.Remove(waiter.Id);
            waiter.HooksProvider?.OnCallerChanged(waiter.Id, nameof(CallerChangedType.WaiterRemoved));
        }


    }

    public class WaiterJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            bool oneTime = (bool)context.JobDetail.JobDataMap["OneTime"];
            INodeHandlerWaiter waiter = (INodeHandlerWaiter)context.JobDetail.JobDataMap["Waiter"];
            TimeManager timeManager = (TimeManager)context.JobDetail.JobDataMap["TimeManager"];

            if (oneTime)
            {
                await context.Scheduler.DeleteJob(context.JobDetail.Key);
                timeManager.RemoveWaiter(waiter);
            }

            waiter.Invoke();
        }
    }

    public class StarterJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            IProcessStarter starter = (IProcessStarter)context.JobDetail.JobDataMap["Starter"];

            starter.Workflow.StartProcessInstance(starter.BpmnProcess, starter.StartNode, null);

            await Task.CompletedTask;
        }
    }
}
