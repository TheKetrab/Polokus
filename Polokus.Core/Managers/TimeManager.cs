using Polokus.Core.Execution;
using Polokus.Core.Helpers;
using Polokus.Core.Extensibility.Hooks;
using Polokus.Core.Interfaces;
using Polokus.Core.Interfaces.BpmnModels;
using Polokus.Core.Interfaces.Execution;
using Polokus.Core.Interfaces.Managers;
using Quartz;
using Quartz.Impl;

namespace Polokus.Core.Managers
{
    public class TimeManager : BaseCallersManager, ITimeManager
    {
        StdSchedulerFactory factory = new StdSchedulerFactory();

        public override IWorkflow Workflow { get; }

        public TimeManager(IWorkflow workflow)
        {
            Workflow = workflow;
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

            AddStarter(starter.Id, starter);
            starter.HooksProvider?.OnCallerChanged(starter.Id, nameof(CallerChangedType.StarterStartedProcess));
        }

        public async Task RegisterWaiterCrone(
            string timeString, INodeHandlerWaiter waiter, bool oneTime,
            Action? continuation = null)
        {
            IScheduler scheduler = await factory.GetScheduler();

            IJobDetail job = JobBuilder.Create<WaiterJob>().Build();
            job.JobDataMap.Add("OneTime", oneTime);
            job.JobDataMap.Add("Waiter", waiter);
            job.JobDataMap.Add("TimeManager", this);
            job.JobDataMap.Add("Continuation", continuation);

            ITrigger trigger = TriggerBuilder.Create().WithCronSchedule(timeString).Build();

            await scheduler.ScheduleJob(job, trigger);
            await scheduler.Start();

            AddWaiter(waiter.Id, waiter);
            waiter.HooksProvider?.OnCallerChanged(waiter.Id, nameof(CallerChangedType.WaiterInserted));
        }

        public void RegisterWaiterNotCrone(string timeString, INodeHandlerWaiter waiter, bool oneTime, Action? continuation = null)
        {
            Task task = new Task(async () =>
            {
                AddWaiter(waiter.Id, waiter, continuation);
                int waitTime = TimeString.ParseToMiliseconds(timeString);
                await Task.Delay(waitTime);
                if (!IsWaiterCancelled(waiter.Id))
                {
                    waiter.Invoke();
                    continuation?.Invoke();
                }

                if (oneTime)
                {
                    RemoveWaiter(waiter.Id);
                }
                else
                {
                    RegisterWaiterNotCrone(timeString, waiter, oneTime, continuation);
                }
            });

            task.Start();
        }

        public override INodeHandlerWaiter RegisterWaiter(
            IProcessInstance pi, IFlowNode node, bool oneTime,
            Action? continuation = null)
        {
            string timedef = node.Name;
            if (TimeString.IsTimeString(timedef))
            {
                var waiter = new NodeHandlerWaiter(pi, node);
                RegisterWaiterNotCrone(timedef, waiter, oneTime, continuation);
                return waiter;
            }
            else if (TimeString.IsCroneString(timedef))
            {
                var waiter = new NodeHandlerWaiter(pi, node);
                bool registered = false;
                Task t = new Task(async () =>
                {
                    await RegisterWaiterCrone(timedef, waiter, true, continuation);
                    registered = true;
                });
                t.Start();
                while (!registered) Thread.Sleep(5);

                return waiter;
            }
            else
            {
                throw new Exception($"Invalid time string: {timedef} of node {node.Id}.");
            }
        }

        public override IProcessStarter RegisterStarter(IBpmnProcess bpmnProcess, IFlowNode startNode)
        {
            var starter = new ProcessStarter(Workflow, bpmnProcess, startNode);
            string croneString = startNode.Name;

            if (!TimeString.IsCroneString(croneString))
            {
                throw new Exception($"The starter seems not to be with proper crone string: {croneString}");
            }

            RegisterStarter(croneString, starter);

            return starter;
        }
    }

    public class WaiterJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            bool oneTime = (bool)context.JobDetail.JobDataMap["OneTime"];
            INodeHandlerWaiter waiter = (INodeHandlerWaiter)context.JobDetail.JobDataMap["Waiter"];
            TimeManager timeManager = (TimeManager)context.JobDetail.JobDataMap["TimeManager"];
            Action? continuation = (Action?)context.JobDetail.JobDataMap["Continuation"];

            if (timeManager.IsWaiterCancelled(waiter.Id))
            {
                timeManager.RemoveWaiter(waiter.Id);
                return;
            }

            if (oneTime)
            {
                await context.Scheduler.DeleteJob(context.JobDetail.Key);
                timeManager.RemoveWaiter(waiter.Id);
            }

            waiter.Invoke();
            continuation?.Invoke();
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
