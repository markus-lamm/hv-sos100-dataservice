using Microsoft.AspNetCore.Mvc;

namespace DataGui.Controllers
{
    public class AdController : Controller
    {
        public IActionResult Index()
        {
            int femaleSignups = 50;
            int maleSignups = 30;

            ViewBag.femaleSignups = femaleSignups;
            ViewBag.maleSignups = maleSignups;

            int Signups16_30 = 30;
            int Signups31_50 = 20;
            int Signups50plus = 10;


            ViewBag.Signups16_30 = Signups16_30;
            ViewBag.Signups31_50 = Signups31_50;
            ViewBag.Signups50plus = Signups50plus;

            int Views = 300;
            int Clicks = 70;

            ViewBag.Views = Views;
            ViewBag.Clicks = Clicks;
            return View();
        }
    }
}
