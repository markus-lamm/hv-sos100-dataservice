using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Hv.Sos100.DataService.Statistics.AdminGui.Controllers
{
    public class EventStatisticsController : Controller
    {
        string _baseURL = "https://informatik6.ei.hv.se/statisticapi/api/EventStatistics";
        public async Task<IActionResult> Index()
        {

            List<Models.EventStatistics>? eventList = new List<Models.EventStatistics>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseURL);
                    HttpResponseMessage response = await client.GetAsync("Event");
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        eventList = JsonSerializer.Deserialize<List<Models.EventStatistics>>(content,
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
            return View(eventList);
        }
    }
}
