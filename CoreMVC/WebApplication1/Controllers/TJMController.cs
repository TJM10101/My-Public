using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class TJMController : Controller
    {
        // GET: TJM/Index  
        [HttpGet]
        public IActionResult Index()
        {
            //int x = 0;
            //int y = 10;
            //int z = y / x;
            return View();
        }


    }
}
