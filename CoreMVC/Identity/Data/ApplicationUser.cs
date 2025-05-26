using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Identity.Data
{
    public class ApplicationUser:IdentityUser
    {
        [MaxLength(3)]
        public string CountryCode {  get; set; }
    }
}
