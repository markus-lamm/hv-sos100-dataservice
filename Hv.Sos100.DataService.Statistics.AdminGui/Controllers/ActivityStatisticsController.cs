using Microsoft.AspNetCore.Mvc;
using Hv.Sos100.DataService.Statistics.AdminGui.Models;
using Hv.Sos100.DataService.Statistics.AdminGui.Data;

namespace Hv.Sos100.DataService.Statistics.AdminGui.Controllers;

public class ActivityStatisticsController : Controller
{
    private readonly Authenticate _authenticate;
    private readonly ApiService _apiService;

    public ActivityStatisticsController(Authenticate authenticate, ApiService apiService)
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

        List<ActivityStatistics>? activityList = await _apiService.GetApiRequest<ActivityStatistics>("https://informatik6.ei.hv.se/statisticapi/api/ActivityStatistics");
        if (activityList == null)
        {
            ViewBag.Message = "Tyvärr gick något fel";
            return View();
        }

        return View(activityList);
    }
}
