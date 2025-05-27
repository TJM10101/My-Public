
using EmployeeService.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<NorthwindContext>(options => {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Northwind"));
            });

            string AllowSelfPolicy = "AllowSelf";
            builder.Services.AddCors(options => {
                options.AddPolicy(name: AllowSelfPolicy, policy => { //第一個參數為name 第二個參數為委派
                    policy.WithHeaders("*").WithMethods("*").WithOrigins("https://localhost:7204");//AllowAnyOrigin可取代("*")p.43
                });//Header開放所有Header Methods開放所有動詞 Origins開放對象/來源
            });

            builder.Services.AddControllers().AddOData(
                options => {
                    options.Select()//挑欄位
                           .Filter()//篩選
                           .OrderBy()//排序
                           .Expand()//關聯查詢
                           .SetMaxTop(100)
                           .Count();
                });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseCors();
            app.UseHttpsRedirection();
            app.UseAuthorization();


            app.MapControllers();
            app.Run();
        }
    }
}
