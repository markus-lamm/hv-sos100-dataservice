using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Hv.Sos100.SingleSignOn;
using Microsoft.AspNetCore.Http;
using Hv.Sos100.Logger;
using Hv.Sos100.DataService.Statistics.EnterpriseGui.Data;

namespace DataGui.Controllers
{
    public class EventController : Controller
    {
        private readonly APIservice aPIservice = new();
        public async Task<IActionResult> Index()
        {

            var eventlist = await aPIservice.GetEvents();

            var authenticationService = new Hv.Sos100.SingleSignOn.AuthenticationService();
            var isAuthenticated = HttpContext.Session.GetString("IsAuthenticated");

            if (isAuthenticated == null)
            {
                var existingSession = await authenticationService.ResumeSession(controllerBase: this, HttpContext);
                if (existingSession != false)
                {
                    var userId = HttpContext.Session.GetString("UserID");
                    var userRole = HttpContext.Session.GetString("UserRole");
                }
                else
                {
                    // Det fanns ingen giltig session att återuppta
                    return Redirect("https://informatik5.ei.hv.se/eventivo/Home/Login");
                }
            }

            return View(eventlist);
        }
    }
}