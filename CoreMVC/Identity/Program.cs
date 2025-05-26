using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Identity.Data;

namespace Identity;

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

        builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>();
        builder.Services.AddControllersWithViews();
        //=================
        builder.Services.Configure<IdentityOptions>(options => {  //設定Identity選項
            options.Password.RequireDigit = true;//包含阿拉伯數字
            options.Password.RequireLowercase = true;//小寫
            options.Password.RequireNonAlphanumeric = true;//非英、數字(ex @!$)
            options.Password.RequireUppercase = true;//大寫
            options.Password.RequiredLength = 8;//長度
            options.Password.RequiredUniqueChars = 1;//不同字
            //以上密碼原則
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);//鎖住5分鐘
            options.Lockout.MaxFailedAccessAttempts = 3;//輸入錯3次鎖住
            options.Lockout.AllowedForNewUsers = true;//鎖定新使用者(要先驗證)

            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = true;//電子郵件要唯一
            options.SignIn.RequireConfirmedEmail = true;//要確認
        });
        builder.Services.ConfigureApplicationCookie(options => {
            options.Cookie.HttpOnly = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(5);//逾時相對時間5分鐘
            options.LoginPath = "/Identity/Account/Login";
            options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            options.SlidingExpiration = true;
        });
        //===================
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

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapRazorPages();

        app.Run();
    }
}
