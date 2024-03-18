using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Hv.Sos100.SingleSignOn;
using Microsoft.AspNetCore.Http;
using Hv.Sos100.Logger;
using Hv.Sos100.DataService.Statistics.EnterpriseGui.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net.Http;

namespace DataGui.Controllers
{
    public class EventController : Controller
    {
        private readonly Hv.Sos100.SingleSignOn.AuthenticationService _authenticationService;
        private readonly APIservice aPIservice = new();

        public EventController(Hv.Sos100.SingleSignOn.AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;

        }
        public async Task<IActionResult> Index()
        {

            var isAuthenticated = await IsLoggedin();


            var eventlist = await aPIservice.GetEvents();


            if (isAuthenticated)
            {
              
                _authenticationService.ReadSessionVariables(controller: this, HttpContext);

                var userId = HttpContext.Session.GetString("UserID");
                var userRole = HttpContext.Session.GetString("UserRole");

                if (userRole == "Organizer")
                {
                    eventlist = eventlist.Where(d => d.UserID == int.Parse(userId)).ToList();
                }
                //else if (userRole == "Organizer")
                //{
                //    result = await _httpClient.GetAsync($"api/Ads/getuserads/{userId}");
                //}
                // Det fanns ingen giltig session att återuppta
                //return Redirect("https://informatik5.ei.hv.se/eventivo/Home/Login");
                
            }

            return View(eventlist);
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
    }
}