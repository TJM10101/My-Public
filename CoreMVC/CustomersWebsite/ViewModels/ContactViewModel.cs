using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace CustomersWebsite.ViewModels
{
    public class ContactViewModel: IValidatableObject
    {
        [Required(ErrorMessage ="姓名欄位不可空白")]
        [StringLength(maximumLength:8, MinimumLength =3, ErrorMessage ="姓名長度不正確")]
        [Display(Name="姓名")]
        public string Name { get; set; }//沒有講show什麼就會去抓資料庫資料表的欄位名稱
        
        [Display(Name = "電子郵件")]
        [EmailAddress(ErrorMessage ="電子郵件格式錯誤")]
        public string? Email { get; set; }
        
        
        [Display(Name = "聯絡電話")]
        
        public string? Phone { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)//可列舉的 IEnumerable 要搭配 yield
        {
            if (string.IsNullOrEmpty(Email)&& string.IsNullOrEmpty(Phone))//如果Email及Phone沒有值或空自字串
            {
                yield return new ValidationResult(          //yield return找到一個結果就先回傳此結果給呼叫者
                    "電子郵件或連絡電話必須至少填寫一項",
                    new string[] { "Email", "Phone" });//錯誤訊息顯示在哪個欄位下方
            }
        }
    }
}