using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Hv.Sos100.DataService.Statistics.AdminGui.Controllers
{
    public class AdStatisticsController : Controller
    {
        string _baseURL = "https://informatik6.ei.hv.se/statisticapi/api/AdStatistics";
        public async Task<IActionResult> Index()
        {

            List<Models.AdStatistics>? adList = new List<Models.AdStatistics>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseURL);
                    HttpResponseMessage response = await client.GetAsync("Ad");
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        adList = JsonSerializer.Deserialize<List<Models.AdStatistics>>(content,
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
            return View(adList);
        }
    }
}
