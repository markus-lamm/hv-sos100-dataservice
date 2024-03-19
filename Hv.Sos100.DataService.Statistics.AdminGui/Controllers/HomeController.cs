using Hv.Sos100.DataService.Statistics.AdminGui.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Hv.Sos100.SingleSignOn;

namespace Hv.Sos100.DataService.Statistics.AdminGui.Controllers
{
    public class HomeController : Controller
    {
        private readonly AuthenticationService _authenticationService;

        public HomeController(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<IActionResult> Index()
        {
            //Create a new session for localhost
            if (HttpContext.Request.Host.Host == "localhost")
            {
                await _authenticationService.CreateSession("ssoadmin@eventivo.com", "ssoadmin", controllerBase: this, HttpContext);
            }

            return RedirectToAction("Index", "EventStatistics");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
