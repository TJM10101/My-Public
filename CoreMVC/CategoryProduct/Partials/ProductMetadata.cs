using System.ComponentModel.DataAnnotations;

namespace CategoryProduct.Models
{
    internal class ProductMetadata
    {
        [Display(Name ="商品名稱")]
        [StringLength(maximumLength:40, MinimumLength =3, ErrorMessage ="商品名稱太短")]
        [Required(ErrorMessage ="商品名稱不可空白")]
        public string ProductName { get; set; } = null!;

        [Display(Name ="商品單價")]
        [DisplayFormat(DataFormatString ="{0:C}")]
        public decimal? UnitPrice { get; set; }

        [Display(Name ="訂購數量")]
        [Range(1,100,ErrorMessage ="訂購數量必須介於1~100之間")]

        public short? UnitsOnOrder { get; set; }


    }
}