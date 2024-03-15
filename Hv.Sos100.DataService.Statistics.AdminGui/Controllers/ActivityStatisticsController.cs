using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Hv.Sos100.DataService.Statistics.AdminGui.Models;
using Hv.Sos100.Logger;
using Hv.Sos100.SingleSignOn;

namespace Hv.Sos100.DataService.Statistics.AdminGui.Controllers
{
    public class ActivityStatisticsController : Controller
    {
        string _baseURL = "https://informatik6.ei.hv.se/statisticapi/api/ActivityStatistics";
        public async Task<IActionResult> Index()
        {
            var isAuthenticated = HttpContext.Session.GetString("IsAuthenticated");
            if (isAuthenticated == null)
            {
                var authenticationService = new AuthenticationService();
                var existingSession = await authenticationService.ResumeSession(controllerBase: this, HttpContext);
                if (existingSession == false)
                {
                    return Redirect("https://informatik5.ei.hv.se/eventivo/Home/Login");
                }
                var userRole = HttpContext.Session.GetString("UserRole");
                if (userRole != "Admin")
                {
                    return Redirect("https://informatik5.ei.hv.se/eventivo/Home/Login");
                }
            }

            List<ActivityStatistics>? activityList = new List<ActivityStatistics>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(_baseURL);
                    //HttpResponseMessage response = await client.GetAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        activityList = JsonSerializer.Deserialize<List<ActivityStatistics>>(content,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    }
                    else
                        ViewBag.Message = "Tyvärr gick något fel: " + response.ReasonPhrase;
                }
            }
            catch (Exception ex)
            {
                var logger = new LogService();

                await logger.CreateLog("StatisticsAdminGui.ActivityStatisticsController", ex);
            }
            return View(activityList);
        }
    }
}
