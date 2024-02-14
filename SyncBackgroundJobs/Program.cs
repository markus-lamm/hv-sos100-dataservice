using Microsoft.EntityFrameworkCore;
using Quartz;
using SyncBackgroundJobs.Jobs;

namespace SyncBackgroundJobs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<StatisticsContext>(options =>
             options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerDatabase")));

            builder.Services.AddQuartz(options =>
            {
                options.UseMicrosoftDependencyInjectionJobFactory();

                options.AddJob<AdvertisementStaticsJob>(JobKey.Create(nameof(AdvertisementStaticsJob)))
                    .AddTrigger(trigger => trigger.ForJob(nameof(AdvertisementStaticsJob)).WithCronSchedule("0 0/1 * * * ?"));

            });

            builder.Services.AddQuartzHostedService();
                
            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
