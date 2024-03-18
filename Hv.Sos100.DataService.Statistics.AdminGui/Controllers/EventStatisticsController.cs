using Microsoft.AspNetCore.Mvc;
using Hv.Sos100.DataService.Statistics.AdminGui.Data;
using Hv.Sos100.DataService.Statistics.AdminGui.Models;

namespace Hv.Sos100.DataService.Statistics.AdminGui.Controllers;

public class EventStatisticsController : Controller
{
    private readonly AuthenticationUtils _authenticate;
    private readonly ApiService _apiService;

    public EventStatisticsController(AuthenticationUtils authenticate, ApiService apiService)
    {
        _authenticate = authenticate;
        _apiService = apiService;
    }

    public async Task<IActionResult> Index()
    {
        var isAuthenticatedAdmin = await _authenticate.IsAuthenticatedAdmin(controller: this, HttpContext);
        if (isAuthenticatedAdmin == false)
        {
            return Redirect("https://informatik5.ei.hv.se/eventivo/Home/Login");
        }

        List<Api.Models.EventStatistics>? eventList = await _apiService.GetApiRequest<Api.Models.EventStatistics>("https://informatik6.ei.hv.se/statisticapi/api/EventStatistics");
        if (eventList == null)
        {
            ViewBag.Message = "Tyvärr gick något fel";
            return View();
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
