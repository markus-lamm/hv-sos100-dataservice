using Hv.Sos100.DataService.Advertisement.Gui.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using Hv.Sos100.DataService.Advertisement.Api.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using Hv.Sos100.SingleSignOn;
using Hv.Sos100.Logger;
using Hv.Sos100.DataService.Advertisement.Gui.Data;

namespace Hv.Sos100.DataService.Advertisement.Gui.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient = new();
        private readonly string _baseURL = "https://informatik6.ei.hv.se/advertisement/";
        private readonly AuthenticationUtils _authenticate;
        private readonly AuthenticationService _authenticationService;
        private readonly LogService _logger = new();

        public HomeController(AuthenticationUtils authenticate, AuthenticationService authenticationService)
        {
            _authenticate = authenticate;
            _authenticationService = authenticationService;
        }

        public async Task<IActionResult> Index()
        {
            //Create a new session for localhost
            if (HttpContext.Request.Host.Host == "localhost")
            {
                await _authenticationService.CreateSession("ssoadmin@eventivo.com", "ssoadmin", controllerBase: this, HttpContext);
            }

            var isAuthenticatedNonCitizen = await _authenticate.IsAuthenticatedNonCitizen(controller: this, HttpContext);
            if (isAuthenticatedNonCitizen == false)
            {
                return Redirect("https://informatik5.ei.hv.se/eventivo/Home/Login");
            }

            var userId = HttpContext.Session.GetString("UserID");
            var userRole = HttpContext.Session.GetString("UserRole");

            _httpClient.BaseAddress = new Uri(_baseURL);
            HttpResponseMessage result = new();
            if (userRole == "Admin")
            {
                result = await _httpClient.GetAsync("api/Ads/getallads");
            }
            else if (userRole == "Organizer")
            {
                result = await _httpClient.GetAsync($"api/Ads/getuserads/{userId}");
            }

            var response = await result.Content.ReadAsStringAsync();
            List<Ads>? adsList = JsonConvert.DeserializeObject<List<Ads>>(response);

            return View(adsList);
        }

        public async Task<IActionResult> Create()
        {
            var isAuthenticatedNonCitizen = await _authenticate.IsAuthenticatedNonCitizen(controller: this, HttpContext);
            if (isAuthenticatedNonCitizen == false)
            {
                return Redirect("https://informatik5.ei.hv.se/eventivo/Home/Login");
            }

            var imageTypes = new List<string> { "Fyrkantig annons", "Horizontell annons", "Vertikal annons" };
            var imageTypesValues = new List<string> { "square", "horizontal", "vertical" };

            List<SelectListItem> selectListItems = new();

            for (int i = 0; i < imageTypes.Count; i++)
            {
                selectListItems.Add(new SelectListItem
                {
                    Text = imageTypes[i],
                    Value = imageTypesValues[i]
                });
            }
            ViewData["ImageTypes"] = new SelectList(selectListItems, "Value", "Text");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("AdvertisementID,ImageSource,ImageLink,TotalViews,TimeStamp,ImageDimension,UserID")] Ads ad, IFormCollection form)
        {
            try
            {
                var file = Request.Form.Files[0];
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    var Content = memoryStream.ToArray();
                    string byte64string = Convert.ToBase64String(Content);
                    ad.ImageSource = $"data:{file.ContentType};base64,{byte64string}";
                }

                var userId = HttpContext.Session.GetString("UserID");
                ad.UserID = int.Parse(userId!);

                _httpClient.BaseAddress = new Uri(_baseURL);
                var responseTask = await _httpClient.PostAsJsonAsync("api/Ads", ad);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                await _logger.CreateLog("DataService.Advertisement.Gui.HomeController.Create", ex);
                return View();
            }
        }

        // GET: MovieAPIController/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            var isAuthenticatedNonCitizen = await _authenticate.IsAuthenticatedNonCitizen(controller: this, HttpContext);
            if (isAuthenticatedNonCitizen == false)
            {
                return Redirect("https://informatik5.ei.hv.se/eventivo/Home/Login");
            }

            if (id == null)
            {
                return NotFound();
            }

            var Ads = await GetAds();
            var ad = Ads?.Where(s => s.AdvertisementID == id).FirstOrDefault();

            if (ad == null)
            {
                return NotFound();
            }

            return View(ad);
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
            catch (Exception ex)
            {
                await _logger.CreateLog("DataService.Advertisement.Gui.HomeController.Delete", ex);
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
