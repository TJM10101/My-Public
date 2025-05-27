
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
                options.AddPolicy(name: AllowSelfPolicy, policy => { //�Ĥ@�ӰѼƬ�name �ĤG�ӰѼƬ��e��
                    policy.WithHeaders("*").WithMethods("*").WithOrigins("https://localhost:7204");//AllowAnyOrigin�i���N("*")p.43
                });//Header�}��Ҧ�Header Methods�}��Ҧ��ʵ� Origins�}���H/�ӷ�
            });

            builder.Services.AddControllers().AddOData(
                options => {
                    options.Select()//�D���
                           .Filter()//�z��
                           .OrderBy()//�Ƨ�
                           .Expand()//���p�d��
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
