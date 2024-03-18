using Hv.Sos100.DataService.Statistics.EnterpriseGui.Data;
using Hv.Sos100.Logger;
using Hv.Sos100.SingleSignOn;

namespace Hv.Sos100.DataService.Statistics.EnterpriseGui
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<AuthenticationService>();
            builder.Services.AddScoped<LogService>();
            builder.Services.AddScoped<ApiService>();
            builder.Services.AddScoped<AuthenticationUtils>();
            builder.Services.AddSession();

            var app = builder.Build();

            app.UseSession();
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
