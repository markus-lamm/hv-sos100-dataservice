using Hv.Sos100.DataService.Advertisement.Gui.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using Hv.Sos100.DataService.Advertisement.Api.Model;
using Azure;
using System.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.IO.Compression;
using Hv.Sos100.SingleSignOn;
using Hv.Sos100.Logger;


namespace Hv.Sos100.DataService.Advertisement.Gui.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient = new();
        private readonly string _baseURL = "https://informatik6.ei.hv.se/advertisement/";
        private readonly AuthenticationService _authenticationService;
        private readonly LogService _logger = new();

        public HomeController( AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;

        }

        public async Task<IActionResult> Index()
        {
            var isAuthenticated = await IsLoggedin();
            
            List<Ads>? deserialized = new();
            _httpClient.BaseAddress = new Uri(_baseURL);
            HttpResponseMessage result = new();

            if (isAuthenticated)
            {
                var currentSession = GetSession();
                var userRole = currentSession!.GetString("UserRole");
                var userId = currentSession!.GetString("UserID");
                
                if (userRole == "Admin")
                {
                    result = await _httpClient.GetAsync("api/Ads/getallads");
                }
                else if (userRole == "Organizer")
                {
                    result = await _httpClient.GetAsync($"api/Ads/getuserads/{userId}");
                }

                var adsJsonString = await result.Content.ReadAsStringAsync();
                deserialized = JsonConvert.DeserializeObject<List<Ads>>(adsJsonString);
            }

            return View(deserialized);

        }

        public async Task<IActionResult> Create()
        {
            if (!await IsLoggedin())
            {
                ViewBag.ShowLoginModal = true;
                return View(nameof(Index));
            }

            var currentSession = GetSession();
            if (currentSession!.GetString("UserRole") == "Citizen")
            {
                return View(nameof(Index));
            }

            var imageTypes = new List<string> { "Fyrkantig anonns", "Horizontell annons", "Vertikal annons" };
            var imageTypesValues = new List<string> { "square", "horizontal", "vertical" };

            List<SelectListItem> selectListItems = new List<SelectListItem>();

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

                var currentSession = GetSession();
                var userId = currentSession!.GetString("UserID");
                ad.UserID = int.Parse(userId!);

                _httpClient.BaseAddress = new Uri(_baseURL);
                var responseTask = await _httpClient.PostAsJsonAsync("api/Ads", ad);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                await _logger.CreateLog("Create new advertisement", LogService.Severity.Error, "Failed to upload file or api is down");
                return View();
            }
        }

        // GET: MovieAPIController/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (!await IsLoggedin())
            {
                ViewBag.ShowLoginModal = true;
                return View(nameof(Index));
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
            catch
            {
                await _logger.CreateLog("Delete advertisement", LogService.Severity.Error, "Failed to delete ad perhaps the api is down");
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

        public async Task<bool> IsLoggedin()
        {
            var existingSession = await _authenticationService.ResumeSession(controllerBase: this, HttpContext);
            if (existingSession)
            {
                _authenticationService.ReadSessionVariables(controller: this, HttpContext);
            }

            return bool.TryParse(HttpContext.Session.GetString("IsAuthenticated"), out _);
        }
        public ISession? GetSession()
        {
            return HttpContext.Session;
        }

        public async Task<IActionResult> Login(string email, string password)
        {
            var authenticationResult = await _authenticationService.CreateSession(email, password, controllerBase: this, HttpContext);
            if (authenticationResult)
            {
                _authenticationService.ReadSessionVariables(controller: this, HttpContext);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Logout()
        {
            _authenticationService.EndSession(controllerBase: this, HttpContext);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
