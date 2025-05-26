using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace WebApplication1;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>();
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

       // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        //app.UseMiddleware<類別>();
        app.UseHttpsRedirection();//User瀏覽HTTP協定網址-->自動重導至HTTPS協定網址(加密連線)
        app.UseStaticFiles();//指定wwwroot資料夾為網站根目錄

        app.UseRouting();//可以做一定資訊隱藏 Url Rewriting --> URI(I=>Identifier)獨一無二的識別碼

        app.UseAuthorization();//身分驗證(Authentication)-->授予權限(Authorization)

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        //{}代表變數 controller類別結尾一定要寫controller
        //action method 處理user操作view引發的事件 動作執行的結果即HTML
        app.MapRazorPages();

        app.Run();
    }
}
