using Microsoft.AspNetCore.Mvc;

namespace DataGui.Controllers
{
    public class ActivityController : Controller
    {
        public IActionResult Index()
        {

            int MonthlyViews = 300;
            int SavedEvents = 70;

            ViewBag.MonthlyViews = MonthlyViews;
            ViewBag.SavedEvents = SavedEvents;
            return View();
        }
    }
}
