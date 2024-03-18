using Microsoft.AspNetCore.Mvc;
using Hv.Sos100.DataService.Statistics.AdminGui.Data;
using Hv.Sos100.DataService.Statistics.AdminGui.Models;

namespace Hv.Sos100.DataService.Statistics.AdminGui.Controllers;

public class ActivityStatisticsController : Controller
{
    private readonly AuthenticationUtils _authenticate;
    private readonly ApiService _apiService;

    public ActivityStatisticsController(AuthenticationUtils authenticate, ApiService apiService)
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

        List<Api.Models.ActivityStatistics>? activityList = await _apiService.GetApiRequest<Api.Models.ActivityStatistics>("https://informatik6.ei.hv.se/statisticapi/api/ActivityStatistics");
        if (activityList == null)
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

        var viewModel = new ActivityStatisticsViewModel
        {
            ActivityList = activityList,
            CategoryList = categoryList
        };

        return View(viewModel);
    }
}
