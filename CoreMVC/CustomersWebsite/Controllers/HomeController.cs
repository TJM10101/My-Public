using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CustomersWebsite.Models;
using CustomersWebsite.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Identity.Client;
using Azure.Identity;

namespace CustomersWebsite.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    NorthwindContext _context;//DI容器
    IMemoryCache _cache;
    public HomeController(ILogger<HomeController> logger,
        NorthwindContext context,
        IMemoryCache Cache)//宣告變數
    {
        _logger = logger;
        _context = context;
        _cache = Cache;
    }
    // GET: Home/Contact
    [HttpGet]
    public IActionResult Contact()
    {
        return View();
    }
    // POST: Home/Contact
    [HttpPost]
    [ValidateAntiForgeryToken]


    //public IActionResult Contact(string Name, string Email, string Phone)
    //public IActionResult Contact([Bind("Name,Email,Phone")] ContactViewModel cvm)//Bind防止過度張貼 不收其他的傳回屬性
    public IActionResult Contact(IFormCollection coll)
    {
        if(ModelState.IsValid)
        {
            //儲存到資料庫
            TempData["Success"] = "資料驗證成功！";//(Key(Success)-Value(資料驗證成功！) Pair)
            return RedirectToAction("Index", "Home"); //下一個Request
        }
        return View(null);
    }
    public IActionResult Index()
    {
        HttpContext.Session.SetString("SessionKey","TJMSession");
        
        MemoryCacheEntryOptions CacheOptions = new MemoryCacheEntryOptions();
        CacheOptions.SetSlidingExpiration(TimeSpan.FromDays(1));
        CacheOptions.SetPriority(CacheItemPriority.Normal);
        _cache.Set<string>("CacheKey", "TJMCache", CacheOptions);

        CookieOptions CookieOption = new CookieOptions();
        CookieOption.Expires = DateTime.Now.AddYears(1);
        CookieOption.Secure = true;
        CookieOption.HttpOnly = true;
        Response.Cookies.Append("CookieKey","TJMCookie",CookieOption);

        
        ViewBag.Banner = "TJM101";
        //ViewBag.CustomerCountry = new SelectList(
        ViewData["CustomerCountry"] = new SelectList(
            _context.Customers.Select(c=>c.Country).Distinct().OrderBy(c=>c));
        return View();
    }

    public IActionResult Privacy()
    {
        string? SessionData = HttpContext.Session.GetString("SessionKey");
        if (SessionData != null)
        {

        }
        string? CacheData= _cache.Get<string>("CacheKey");
        if (CacheData != null)
        { 
        }

        string? CookieData = Request.Cookies["CookieKey"];
        if (CookieData != null)
        {

        }
        ViewBag.CustomerCount = $"alert('客戶人數:{_context.Customers.Count()}')";
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
