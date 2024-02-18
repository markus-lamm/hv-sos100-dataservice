using Quartz;
using SyncBackgroundJobs.Jobs;

namespace SyncBackgroundJobs.Shared
{
    public static class DependencyInjection
    {
        public static void AddJobsAsDependency(this IServiceCollection services)
        {
            services.AddQuartz(options =>
            {
                ConfigureAndAddJob<AdvertisementStaticsJob>(options, nameof(AdvertisementStaticsJob), "0 0/1 * * * ?");

            });

            services.AddQuartzHostedService();


        }
        static void ConfigureAndAddJob<T>(IServiceCollectionQuartzConfigurator options, string jobName, string cronSchedule) where T : IJob
        {
            options.AddJob<T>(jobBuilder => jobBuilder.WithIdentity(JobKey.Create(jobName)))
                   .AddTrigger(trigger => trigger.ForJob(jobName).WithCronSchedule(cronSchedule));
        }
    }
}
