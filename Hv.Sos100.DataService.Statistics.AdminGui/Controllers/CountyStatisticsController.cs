using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Hv.Sos100.DataService.Statistics.AdminGui.Controllers
{
    public class CountyStatisticsController : Controller
    {
        string _baseURL = "https://informatik6.ei.hv.se/statisticapi/api/CountyStatistics";
        public async Task<IActionResult> Index()
        {

            List<Models.CountyStatistics>? countyList = new List<Models.CountyStatistics>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseURL);
                    HttpResponseMessage response = await client.GetAsync("County");
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        countyList = JsonSerializer.Deserialize<List<Models.CountyStatistics>>(content,
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
            return View(countyList);
        }
    }
}
