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

        //app.UseMiddleware<���O>();
        app.UseHttpsRedirection();//User�s��HTTP��w���}-->�۰ʭ��ɦ�HTTPS��w���}(�[�K�s�u)
        app.UseStaticFiles();//���wwwwroot��Ƨ��������ڥؿ�

        app.UseRouting();//�i�H���@�w��T���� Url Rewriting --> URI(I=>Identifier)�W�@�L�G���ѧO�X

        app.UseAuthorization();//��������(Authentication)-->�¤��v��(Authorization)

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        //{}�N���ܼ� controller���O�����@�w�n�gcontroller
        //action method �B�zuser�ާ@view�޵o���ƥ� �ʧ@���檺���G�YHTML
        app.MapRazorPages();

        app.Run();
    }
}
