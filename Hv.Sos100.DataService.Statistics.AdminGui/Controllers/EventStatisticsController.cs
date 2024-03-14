using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Hv.Sos100.DataService.Statistics.AdminGui.Models;
using Hv.Sos100.Logger;


namespace Hv.Sos100.DataService.Statistics.AdminGui.Controllers
{
    public class EventStatisticsController : Controller
    {
        string _baseURL = "https://informatik6.ei.hv.se/statisticapi/api/EventStatistics";
        public async Task<IActionResult> Index()
        {

            List<EventStatistics>? eventList = new List<EventStatistics>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(_baseURL);
                    //HttpResponseMessage response = await client.GetAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        eventList = JsonSerializer.Deserialize<List<EventStatistics>>(content,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    }
                    else
                        ViewBag.Message = "Tyvärr gick något fel: " + response.ReasonPhrase;
                }
            }
            catch (Exception ex)
            {
                var logger = new LogService();

                await logger.CreateLog("StatisticsAdminGui", ex);
            }
            return View(eventList);
        }
    }
}
