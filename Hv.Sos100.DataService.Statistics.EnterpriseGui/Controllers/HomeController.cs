using Hv.Sos100.SingleSignOn;
using Microsoft.AspNetCore.Mvc;

namespace DataGui.Controllers
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

            return RedirectToAction("Index", "Event");
        }
    }
}