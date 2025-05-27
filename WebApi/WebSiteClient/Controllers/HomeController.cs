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
        HttpResponseMessage Response = await Client.GetAsync(Uri);//�o�e�I�s
        Response.EnsureSuccessStatusCode();//�T�wHttp���A�X�O�諸(400�H��)
        string Json=await Response.Content.ReadAsStringAsync();//Ū�����G �r��
        Rate[] Arr=JsonSerializer.Deserialize<Rate[]>(Json);//�ϧǦC�� �s�W�@���O�������쪺�٭즨����(�n�����O)
        return Arr.Last().USDNTD;//���̫�@�� �ݩʬ� USDNTD �����r��
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
