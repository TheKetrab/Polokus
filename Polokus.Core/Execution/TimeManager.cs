using Quartz.Impl;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Quartz.Logging.OperationName;
using Polokus.Core.Interfaces;
using Polokus.Core.Hooks;
using Polokus.Core.Helpers;

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
        Dictionary<string, CancellationTokenSource> _cancellationWaitersTokens = new();
        private object _ctokenslock = new object();

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

        public void RegisterWaiterNotCrone(string timeString, INodeHandlerWaiter waiter, Action afterInvokeAction)
        {
            
            CancellationTokenSource ctSrc = new();
            CancellationToken token = ctSrc.Token;
            
            Task task = new Task(async () =>
            {
                int waitTime = TimeString.ParseToMiliseconds(timeString);
                await Task.Delay(waitTime);
                if (!token.IsCancellationRequested)
                {
                    waiter.Invoke(); // first invoke waiter, then 'kill anc cancell', otherwise for a little time there will be no active task, process will finish, and then another task will occur, error!
                    afterInvokeAction();
                }
                lock (_ctokenslock)
                {
                    if (_cancellationWaitersTokens.ContainsKey(waiter.Id))
                    {
                        _cancellationWaitersTokens.Remove(waiter.Id);
                    }
                }
            });

            _cancellationWaitersTokens.Add(waiter.Id, ctSrc);
            task.Start();
        }

        public void CancellWaiter(string waiterId)
        {
            lock (_ctokenslock)
            {
                if (_cancellationWaitersTokens.ContainsKey(waiterId))
                {
                    var ctSrc = _cancellationWaitersTokens[waiterId];
                    ctSrc.Cancel();
                    _cancellationWaitersTokens.Remove(waiterId);
                }
            }
        }

        public bool IsWaiterCancelled(string waiterId)
        {
            lock (_ctokenslock)
            {
                if (_cancellationWaitersTokens.ContainsKey(waiterId))
                {
                    var ctSrc = _cancellationWaitersTokens[waiterId];
                    return ctSrc.Token.IsCancellationRequested;
                }
                else
                {
                    return true;
                }
            }

        }


    }

    public class WaiterJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            bool oneTime = (bool)context.JobDetail.JobDataMap["OneTime"];
            INodeHandlerWaiter waiter = (INodeHandlerWaiter)context.JobDetail.JobDataMap["Waiter"];
            TimeManager timeManager = (TimeManager)context.JobDetail.JobDataMap["TimeManager"];

            if (timeManager.IsWaiterCancelled(waiter.Id))
            {
                timeManager.RemoveWaiter(waiter);
                return;
            }

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
