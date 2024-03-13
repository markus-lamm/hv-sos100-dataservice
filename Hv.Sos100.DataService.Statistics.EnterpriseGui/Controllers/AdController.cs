using Microsoft.AspNetCore.Mvc;

namespace DataGui.Controllers
{
    public class AdController : Controller
    {
        public IActionResult Index()
        {
            int Views = 300;
            
            ViewBag.Views = Views;
          
            return View();
        }
    }
}
