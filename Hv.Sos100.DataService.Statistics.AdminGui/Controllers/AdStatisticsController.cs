using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Hv.Sos100.DataService.Statistics.AdminGui.Models;
using Hv.Sos100.Logger;
using Hv.Sos100.SingleSignOn;


namespace Hv.Sos100.DataService.Statistics.AdminGui.Controllers
{
    public class AdStatisticsController : Controller
    {
        string _baseURL = "https://informatik6.ei.hv.se/statisticapi/api/AdStatistics";
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

            List<AdStatistics>? adList = new List<AdStatistics>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(_baseURL);
                    //HttpResponseMessage response = await client.GetAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        adList = JsonSerializer.Deserialize<List<AdStatistics>>(content,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    }
                    else
                        ViewBag.Message = "Tyvärr gick något fel: " + response.ReasonPhrase;
                }
            }
            catch (Exception ex)
            {
                var logger = new LogService();

                await logger.CreateLog("StatisticsAdminGui.AdStatisticsController", ex);
            }
            return View(adList);
        }
    }
}
