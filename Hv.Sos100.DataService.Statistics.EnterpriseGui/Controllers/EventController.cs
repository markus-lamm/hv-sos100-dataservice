
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

            
            int TotalSignups = 70;
            

           
            ViewBag.TotalSignups = TotalSignups;
         

            int TotalEvents = 60;
            int TotalEntertainmentEvents = 15;
            int TotalFoodEvents = 20;
            int TotalSportEvents = 25;
         


            ViewBag.TotalEvents = TotalEvents;
            ViewBag.TotalEntertainmentEvents = TotalEntertainmentEvents;
            ViewBag.TotalFoodEvents = TotalFoodEvents;
            ViewBag.TotalSportEvents = TotalSportEvents;
          

            return View();
        }
    }
}