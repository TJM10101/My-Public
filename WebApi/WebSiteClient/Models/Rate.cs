using System.Text.Json.Serialization;

namespace WebSiteClient.Models
{

    public class Rate
    {
        public string Date { get; set; }
        [JsonPropertyName("USD/NTD")]//要和json文件的屬性名稱一樣 因為C#類別屬性不能加/ 
        public string USDNTD { get; set; }
        public string RMBNTD { get; set; }
        public string EURUSD { get; set; }
        public string USDJPY { get; set; }
        public string GBPUSD { get; set; }
        public string AUDUSD { get; set; }
        public string USDHKD { get; set; }
        public string USDRMB { get; set; }
        public string USDZAR { get; set; }
        public string NZDUSD { get; set; }
    }

}
