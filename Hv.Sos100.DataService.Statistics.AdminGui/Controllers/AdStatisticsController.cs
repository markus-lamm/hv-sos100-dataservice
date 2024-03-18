using Microsoft.AspNetCore.Mvc;
using Hv.Sos100.DataService.Statistics.AdminGui.Models;
using Hv.Sos100.DataService.Statistics.AdminGui.Data;

namespace Hv.Sos100.DataService.Statistics.AdminGui.Controllers;

public class AdStatisticsController : Controller
{
    private readonly Authenticate _authenticate;
    private readonly ApiService _apiService;

    public AdStatisticsController(Authenticate authenticate, ApiService apiService)
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

        List<AdStatistics>? adList = await _apiService.GetApiRequest<AdStatistics>("https://informatik6.ei.hv.se/statisticapi/api/AdStatistics");
        if (adList == null)
        {
            ViewBag.Message = "Tyvärr gick något fel";
            return View();
        }

        return View(adList);
    }
}
