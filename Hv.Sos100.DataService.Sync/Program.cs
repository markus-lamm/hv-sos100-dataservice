using Microsoft.EntityFrameworkCore;
using Quartz;
using Hv.Sos100.DataService.Sync.Jobs;
using Hv.Sos100.DataService.Sync.Shared;

namespace Hv.Sos100.DataService.Sync
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddJobsAsDependency();
            builder.Services.AddHttpClient();

            var app = builder.Build();

            app.MapGet("/", () => "Sync job running in the background");

            app.Run();
        }
    }
}
