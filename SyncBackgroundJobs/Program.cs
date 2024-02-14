using Microsoft.EntityFrameworkCore;
using SyncBackgroundJobs.Model;

namespace SyncBackgroundJobs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<DataServiceDbContext>(options =>
             options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnection")));

            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
