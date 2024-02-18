using Microsoft.EntityFrameworkCore;
using Quartz;
using SyncBackgroundJobs.Jobs;
using SyncBackgroundJobs.Shared;

namespace SyncBackgroundJobs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddJobsAsDependency();
            builder.Services.AddHttpClient();

            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
