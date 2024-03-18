using Hv.Sos100.DataService.Log.Gui.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Hv.Sos100.SingleSignOn;
using Hv.Sos100.DataService.Log.Gui.Data;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Hv.Sos100.DataService.Log.Gui.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApiService _apiService;
        private readonly AuthenticationService _authenticationService;

        public HomeController(ApiService apiService, AuthenticationService authenticationService)
        {
            _apiService = apiService;
            _authenticationService = authenticationService;
        }

        public async Task<IActionResult> Index()
        {
            //Create a new session for localhost
            if (HttpContext.Request.Host.Host == "localhost")
            {
                await _authenticationService.CreateSession("ssoadmin@eventivo.com", "ssoadmin", controllerBase: this, HttpContext);
            }

            var isAuthenticated = HttpContext.Session.GetString("IsAuthenticated");
            if (isAuthenticated == null)
            {
                var existingSession = await _authenticationService.ResumeSession(controllerBase:this, HttpContext);

                if (existingSession == false)
                {
                    Redirect("https://informatik5.ei.hv.se/eventivo/Home/Login");
                }
            }

            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "Admin")
            {
                Redirect("https://informatik5.ei.hv.se/eventivo/Home/Login");
            }

            var logs = await _apiService.GetLogs() ?? new List<Api.Models.Log>();
            logs = logs.OrderByDescending(l => l.TimeStamp).ToList();
            return View(logs);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
