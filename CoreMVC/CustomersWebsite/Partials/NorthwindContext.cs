using Microsoft.EntityFrameworkCore;

namespace CustomersWebsite.Models
{
    public partial class NorthwindContext : DbContext
    {
        public NorthwindContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)//override父類別
        {
            if (!optionsBuilder.IsConfigured) //是否完成設定過 沒有才設定
            {
                IConfiguration Config = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)//專案根目錄在哪
                    .AddJsonFile("appsettings.json")//設定檔
                    .Build();
                optionsBuilder.UseSqlServer(Config.GetConnectionString("Northwind"));//給自己new類別的人用的

            }
        }
    }
}
