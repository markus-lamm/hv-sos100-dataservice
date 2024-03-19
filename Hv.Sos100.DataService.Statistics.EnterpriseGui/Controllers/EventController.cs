using Microsoft.AspNetCore.Mvc;
using Hv.Sos100.DataService.Statistics.EnterpriseGui.Data;
using Hv.Sos100.DataService.Statistics.EnterpriseGui.Models;

namespace Hv.Sos100.DataService.Statistics.EnterpriseGui.Controllers
{
    public class EventController : Controller
    {
        private readonly AuthenticationUtils _authenticate;
        private readonly ApiService _apiService;

        public EventController(AuthenticationUtils authenticate, ApiService apiService)
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

            List<Api.Models.EventStatistics>? eventList = await _apiService.GetApiRequest<Api.Models.EventStatistics>("https://informatik6.ei.hv.se/statisticapi/api/EventStatistics");
            if (eventList == null)
            {
                ViewBag.Message = "Tyvärr gick något fel";
                return View();
            }

            var userId = HttpContext.Session.GetString("UserID");
            var userRole = HttpContext.Session.GetString("UserRole");

            // Prune the list of events to only show the events that the user is allowed to see
            if (userRole == "Organizer")
            {
                eventList = eventList.Where(d => d.UserID == int.Parse(userId!)).ToList();
            }

            List<Category>? categoryList = await _apiService.GetApiRequest<Category>("https://informatik1.ei.hv.se/ActivityAPI/api/Categories");
            if (categoryList == null)
            {
                ViewBag.Message = "Tyvärr gick något fel";
                return View();
            }

            var viewModel = new EventStatisticsViewModel
            {
                EventList = eventList,
                CategoryList = categoryList
            };

            return View(viewModel);
        }
    }
}