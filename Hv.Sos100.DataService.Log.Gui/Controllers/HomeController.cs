using Hv.Sos100.DataService.Log.Gui.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Hv.Sos100.SingleSignOn;
using Hv.Sos100.DataService.Log.Gui.Data;

namespace Hv.Sos100.DataService.Log.Gui.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApiService _apiService;
        private readonly AuthenticationService _authenticationService;

        public HomeController(ILogger<HomeController> logger, ApiService apiService, AuthenticationService authenticationService)
        {
            _logger = logger;
            _apiService = apiService;
            _authenticationService = authenticationService;
        }

        public async Task<IActionResult> Index()
        {
            var existingSession = await _authenticationService.ResumeSession(controllerBase:this, HttpContext);
            if (existingSession)
            {
                _authenticationService.ReadSessionVariables(controller:this, HttpContext);
            }

            var logs = new List<Api.Models.Log>();

            var isAuthenticated = HttpContext.Session.GetString("IsAuthenticated");
            
            if (isAuthenticated == "true")
            {
                logs = await _apiService.GetLogs() ?? new List<Api.Models.Log>();
            }

            return View(logs);
        }

        public async Task<IActionResult> Login(string email, string password)
        {
            var authenticationResult = await _authenticationService.CreateSession(email, password, controllerBase: this, HttpContext);
            if (authenticationResult)
            {
                _authenticationService.ReadSessionVariables(controller:this, HttpContext);
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
