using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WebSiteClient.Models;

namespace WebSiteClient.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Employees()
    {
        return View();
    }

    public IActionResult Customers()
    {
        return View();
    }

    //GET: Home/ExchangeRate
    [HttpGet]
    public async Task<string> ExchangeRate()
    {
        string Uri = "https://openapi.taifex.com.tw/v1/DailyForeignExchangeRates";
        HttpClient Client = new HttpClient();
        HttpResponseMessage Response = await Client.GetAsync(Uri);//發送呼叫
        Response.EnsureSuccessStatusCode();//確定Http狀態碼是對的(400以內)
        string Json=await Response.Content.ReadAsStringAsync();//讀取結果 字串
        Rate[] Arr=JsonSerializer.Deserialize<Rate[]>(Json);//反序列化 新增一類別讓接收到的還原成物件(要給類別)
        return Arr.Last().USDNTD;//拿最後一筆 屬性為 USDNTD 此為字串
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
