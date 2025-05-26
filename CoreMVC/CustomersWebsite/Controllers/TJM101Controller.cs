using CustomersWebsite.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomersWebsite.Controllers
{
    public class TJM101Controller : Controller
    {
        NorthwindContext Context;//因為網頁程式沒有狀態的 所以可以放到變數中共用
        public TJM101Controller(NorthwindContext context)//DI 容器中取出物件當作參數回傳
        {
            Context = context;
        }
        //GET: TJH101/Index
        [HttpGet]
        public IActionResult Index()
        {
            //NorthwindContext Context = new NorthwindContext(); 
            return View(Context.Customers);
        }
    }
}
