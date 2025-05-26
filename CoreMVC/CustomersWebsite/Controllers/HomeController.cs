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
    NorthwindContext _context;//DI�e��
    IMemoryCache _cache;
    public HomeController(ILogger<HomeController> logger,
        NorthwindContext context,
        IMemoryCache Cache)//�ŧi�ܼ�
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
    //public IActionResult Contact([Bind("Name,Email,Phone")] ContactViewModel cvm)//Bind����L�ױi�K ������L���Ǧ^�ݩ�
    public IActionResult Contact(IFormCollection coll)
    {
        if(ModelState.IsValid)
        {
            //�x�s���Ʈw
            TempData["Success"] = "������Ҧ��\�I";//(Key(Success)-Value(������Ҧ��\�I) Pair)
            return RedirectToAction("Index", "Home"); //�U�@��Request
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
        ViewBag.CustomerCount = $"alert('�Ȥ�H��:{_context.Customers.Count()}')";
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
