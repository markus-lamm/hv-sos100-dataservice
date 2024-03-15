using Hv.Sos100.DataService.Statistics.AdminGui.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using Hv.Sos100.SingleSignOn;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Hv.Sos100.DataService.Statistics.AdminGui.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            var authenticationService = new AuthenticationService();
            var authenticatedSession = await authenticationService.CreateSession("ssoadmin@eventivo.com", "ssoadmin", controllerBase: this, HttpContext);
            return RedirectToAction("Index", "EventStatistics");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
