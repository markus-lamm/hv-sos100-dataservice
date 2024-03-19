using Microsoft.AspNetCore.Mvc;
using Hv.Sos100.DataService.Statistics.EnterpriseGui.Data;

namespace Hv.Sos100.DataService.Statistics.EnterpriseGui.Controllers
{
    public class AdController : Controller
    {
        private readonly AuthenticationUtils _authenticate;
        private readonly ApiService _apiService;

        public AdController(AuthenticationUtils authenticate, ApiService apiService)
        {
            _authenticate = authenticate;
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var isAuthenticatedNonCitizen = await _authenticate.IsAuthenticatedNonCitizen(controller: this, HttpContext);
            if (isAuthenticatedNonCitizen == false)
            {
                return Redirect("https://informatik5.ei.hv.se/eventivo/Home/Login");
            }

            List<Api.Models.AdStatistics>? adList = await _apiService.GetApiRequest<Api.Models.AdStatistics>("https://informatik6.ei.hv.se/statisticapi/api/AdStatistics");
            if (adList == null)
            {
                ViewBag.Message = "Tyvärr gick något fel";
                return View();
            }

            var userId = HttpContext.Session.GetString("UserID");
            var userRole = HttpContext.Session.GetString("UserRole");

            // Prune the list of events to only show the events that the user is allowed to see
            if (userRole == "Organizer")
            {
                adList = adList.Where(d => d.UserID == int.Parse(userId!)).ToList();
            }

            return View(adList);
        }
    }
}
