using CustomerOrder.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CustomerOrder.Controllers
{
    public class CustomersController : Controller
    {
        NorthwindContext _context;

        public CustomersController(NorthwindContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //ViewBag.Customers = new SelectList(
            //    _context.Customers.Select(c => c.CompanyName).OrderBy(c => c));//下拉選項僅放一個欄位 可以不用再查詢CustomerId
            ViewBag.Customers = new SelectList(
                _context.Customers.Select(c => new
                {
                    客戶代號 = c.CustomerId,
                    公司名稱 = c.CompanyName,
                }).OrderBy(c => c.公司名稱), "客戶代號", "公司名稱");
            //}).OrderBy(c=>c),"客戶代號", "公司名稱");//OrderBy完不能當SelectList的第一個參數
            return View();
        }
        //GET:Customers/Order/1
        [HttpGet]
        public async Task<IActionResult>Orders(string id)
        {
            Customer c = await _context.Customers.FindAsync(id);
            return PartialView("_OrdersPartial",c.Orders);
            //View是Html文件 會畫面刷新 故改成partial
            //給第一個參數 就可以與Orders脫鉤 而顯示_OrdersPartical
        }
    
    }

}
