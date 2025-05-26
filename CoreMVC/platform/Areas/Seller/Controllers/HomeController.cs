using Microsoft.AspNetCore.Mvc;

namespace platform.Areas.Seller.Controllers
{
    [Area("Seller")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Remove()
        {
            return View();
        }
    }
}
