using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Hv.Sos100.DataService.SyncBackgroundJobs;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;

namespace QuartzSampleApp
{
    public class Program
    {
        private static async Task Main(string[] args)
        {
            // Grab the Scheduler instance from the Factory
            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();

            // and start it off
            await scheduler.Start();

            await ScheduleJob<AdvertisementStaticsJob>(scheduler, "anotherjob", TimeSpan.FromSeconds(30));           


            var taskDelayer = new TaskCompletionSource<object>();
            await taskDelayer.Task;

            // and last shut down the scheduler when you are ready to close your program
            await scheduler.Shutdown();

        }

        private static async Task ScheduleJob<T>(IScheduler scheduler, string jobName, TimeSpan interval) where T : IJob
        {
            IJobDetail job = CreateJob<T>(jobName);
            ITrigger trigger = CreateTrigger(jobName + "Trigger", interval);

            await scheduler.ScheduleJob(job, trigger);
        }

        private static ITrigger CreateTrigger(string triggerName, TimeSpan interval)
        {
            return TriggerBuilder.Create()
                .WithIdentity(triggerName)
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithInterval(interval)
                    .RepeatForever())
                .Build();
        }
        private static IJobDetail CreateJob<T>(string jobName) where T : IJob
        {
            return JobBuilder.Create<T>()
                .WithIdentity(jobName)
                .Build();
        }
    }
}
