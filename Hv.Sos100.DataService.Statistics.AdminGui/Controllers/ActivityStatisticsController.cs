using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Hv.Sos100.DataService.Statistics.Api.Models;

namespace Hv.Sos100.DataService.Statistics.AdminGui.Controllers
{
    public class ActivityStatisticsController : Controller
    {
        string _baseURL = "https://informatik6.ei.hv.se/statisticapi/api/ActivityStatistics";
        public async Task<IActionResult> Index()
        {

            List<ActivityStatistics>? activityList = new List<ActivityStatistics>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseURL);
                    HttpResponseMessage response = await client.GetAsync("Activities");
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
                ViewBag.Message = "Tyvärr gick något fel: " + ex.Message;
            }
            return View(activityList);
        }
    }
}
