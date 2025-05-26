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
        builder.Services.Configure<IdentityOptions>(options => {  //�]�wIdentity�ﶵ
            options.Password.RequireDigit = true;//�]�t���ԧB�Ʀr
            options.Password.RequireLowercase = true;//�p�g
            options.Password.RequireNonAlphanumeric = true;//�D�^�B�Ʀr(ex @!$)
            options.Password.RequireUppercase = true;//�j�g
            options.Password.RequiredLength = 8;//����
            options.Password.RequiredUniqueChars = 1;//���P�r
            //�H�W�K�X��h
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);//���5����
            options.Lockout.MaxFailedAccessAttempts = 3;//��J��3�����
            options.Lockout.AllowedForNewUsers = true;//��w�s�ϥΪ�(�n������)

            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = true;//�q�l�l��n�ߤ@
            options.SignIn.RequireConfirmedEmail = true;//�n�T�{
        });
        builder.Services.ConfigureApplicationCookie(options => {
            options.Cookie.HttpOnly = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(5);//�O�ɬ۹�ɶ�5����
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
