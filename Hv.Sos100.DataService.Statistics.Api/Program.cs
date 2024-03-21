using Hv.Sos100.DataService.Statistics.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Hv.Sos100.DataService.Statistics.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options => options.AddPolicy("StatisticPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()));

            builder.Services.AddDbContext<StatisticsContext>(options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerDatabase")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("StatisticPolicy");
            app.MapControllers().RequireCors("StatisticPolicy");

            app.Run();
        }
    }
}
