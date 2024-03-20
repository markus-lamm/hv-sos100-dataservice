using Quartz;
using Hv.Sos100.DataService.Sync.Jobs;

namespace Hv.Sos100.DataService.Sync.Shared
{
    public static class DependencyInjection
    {
        public static void AddJobsAsDependency(this IServiceCollection services)
        {
            services.AddQuartz(options =>
            {
                ConfigureAndAddJob<AdvertisementStaticsJob>(options, nameof(AdvertisementStaticsJob));
                ConfigureAndAddJob<ActivityStatisticJob>(options, nameof(ActivityStatisticJob));
                ConfigureAndAddJob<EventStatisticJob>(options, nameof(EventStatisticJob));
            });
            services.AddQuartzHostedService();
        }
        static void ConfigureAndAddJob<T>(IServiceCollectionQuartzConfigurator options, string jobName) where T : IJob
        {
            options.AddJob<T>(jobBuilder => jobBuilder.WithIdentity(JobKey.Create(jobName)))
                   .AddTrigger(trigger => trigger.ForJob(jobName).WithDailyTimeIntervalSchedule
                      (s =>
                         s.WithIntervalInHours(1)
                        .OnEveryDay()
                        .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(00, 0))
                      ));
        }
    }
}
