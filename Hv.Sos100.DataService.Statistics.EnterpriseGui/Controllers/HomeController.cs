
using Microsoft.AspNetCore.Mvc;

namespace DataGui.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Event");
        }
    }
}