using Microsoft.AspNetCore.Mvc;

namespace DataGui.Controllers
{
    public class ActivityController : Controller
    {
        public IActionResult Index()
        {
            int SavedEvents = 70;

            ViewBag.SavedEvents = SavedEvents;
            return View();
        }
    }
}
