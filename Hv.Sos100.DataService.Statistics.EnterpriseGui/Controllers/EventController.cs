
using Microsoft.AspNetCore.Mvc;

namespace DataGui.Controllers
{
    public class EventController : Controller
    {
        public ActionResult Index()
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
            int TotalSignups = 70;
            int SavedEvents = 120;

            ViewBag.Views = Views;
            ViewBag.TotalSignups = TotalSignups;
            ViewBag.SavedEvents = SavedEvents;

            int oneRating = 10;
            int twoRating = 15;
            int threeRating = 20;
            int fourRating = 25;
            int fiveRating = 30;


            ViewBag.oneRating = oneRating;
            ViewBag.twoRating = twoRating;
            ViewBag.threeRating = threeRating;
            ViewBag.fourRating = fourRating;
            ViewBag.fiveRating = fiveRating;

            return View();
        }
    }
}