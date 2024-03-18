using Microsoft.EntityFrameworkCore;
using Quartz;
using Hv.Sos100.DataService.Sync.Jobs;
using Hv.Sos100.DataService.Sync.Shared;
using System.Net.Http.Headers;

namespace Hv.Sos100.DataService.Sync
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddJobsAsDependency();
            builder.Services.AddHttpClient();

            builder.Services.AddHttpClient("activityapi", client =>
            {
                client.BaseAddress = new Uri("https://informatik1.ei.hv.se/ActivityAPI/");
            });

            builder.Services.AddHttpClient("statisticapi", client =>
            {
                client.BaseAddress = new Uri("https://informatik6.ei.hv.se/statisticapi/");
            });

            builder.Services.AddHttpClient("eventapi", client =>
            {
                client.BaseAddress = new Uri("https://informatik4.ei.hv.se/EVENTAPI2/");
            });

            builder.Services.AddHttpClient("profilapi", client =>
            {
                client.BaseAddress = new Uri("https://informatik7.ei.hv.se/ProfilAPI/");
            });

            builder.Services.AddHttpClient("organizerapi", client =>
            {
                client.BaseAddress = new Uri("https://informatik2.ei.hv.se/OrganizerAPI/");
            });

            builder.Services.AddHttpClient("adsapi", client =>
            {
                client.BaseAddress = new Uri("https://informatik6.ei.hv.se/advertisement/");
            });

            var app = builder.Build();

            app.MapGet("/", () => "Sync job running in the background");

            app.Run();
        }
    }
}
