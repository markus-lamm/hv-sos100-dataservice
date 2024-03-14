using Hv.Sos100.DataService.Advertisement.Gui.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using Hv.Sos.DataService.Advertisement.Api.Model;
using Azure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hv.Sos100.DataService.Advertisement.Gui.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient = new();
        private readonly string _baseURL = "https://informatik6.ei.hv.se/advertisement/";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            _httpClient.BaseAddress = new Uri(_baseURL);

            var result = await _httpClient.GetAsync("api/Ads/getallads");
            var adsJsonString = await result.Content.ReadAsStringAsync();
            var deserialized = JsonConvert.DeserializeObject<IEnumerable<Ads>>(adsJsonString);
            return View(deserialized);
        }

       
        public IActionResult Create()
        {
            var imageTypes = new List<string> { "square", "horizontal", "vertical" };
            ViewData["ImageTypes"] = new SelectList(imageTypes);

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Id,ImageUrl,Views,ImageSize")] Ads ad)
        {
            try
            {
                _httpClient.BaseAddress = new Uri(_baseURL);

                var responseTask = await _httpClient.PostAsJsonAsync("api/Ads", ad);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MovieAPIController/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Ads = await GetAds();
            var movie = Ads?.Where(s => s.Id == id).FirstOrDefault();

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }


        // POST: MovieAPIController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                _httpClient.BaseAddress = new Uri(_baseURL);
                var responseTask = await _httpClient.DeleteAsync($"api/Ads/{id}");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<List<Ads>> GetAds()
        {

            _httpClient.BaseAddress = new Uri(_baseURL);

            var result = await _httpClient.GetAsync("api/Ads/getallads");
            var adsJsonString = await result.Content.ReadAsStringAsync();
            var deserialized = JsonConvert.DeserializeObject<List<Ads>>(adsJsonString);


            return deserialized ?? new List<Ads>();
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
