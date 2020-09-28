using Microsoft.AspNetCore.Mvc;
using SportsPro.DataLayer;


namespace SportsPro.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

        [Route("About")]
        public IActionResult About()
        {
            return View();
        }
    }
}