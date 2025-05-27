using EmployeeService.Models;

namespace EmployeeService.DTO
{
    //internal class EmployeeDTO : Employee//繼承所以有Employee所有欄位
    public class EmployeeDTO
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Title { get; set; }
    }
}