using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeService.Models;
using System.Reflection;
using EmployeeService.DTO;
using Microsoft.AspNetCore.Cors;

namespace EmployeeService.Controllers
{
    [EnableCors("AllowSelf")]
    [Route("api/[controller]")]//變數用[] action函式不在這裏面
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public EmployeesController(NorthwindContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees() 
        //    return await _context.Employees.ToListAsync();
        //}

        public async Task<IEnumerable<EmployeeDTO>> GetEmployees()
        {
            return _context.Employees.Select(e => new EmployeeDTO{//new後面沒有型態即為匿名型態 此為新的型態 Controller會用的
                EmployeeId = e.EmployeeId,
                FirstName=e.FirstName,
                LastName=e.LastName,
                Title=e.Title,
            });
                                                                   
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]//依ID查詢某筆紀錄 {}是變數
        public async Task<EmployeeDTO> GetEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return null;
            }
            EmployeeDTO EmpDTO = new EmployeeDTO
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Title = employee.Title,
            };
            return EmpDTO;
        }

        //GET api/Employees/Test
        [HttpGet("Test")]//這是常數 因為沒有{}
        public string Test()
        {
            return "Web API";
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]//修改
        public async Task<string> PutEmployee(int id, EmployeeDTO EmpDTO)//公開給所有人用的話不要用字串 改成傳回值型態(因為怕語系不一樣)
        {
            if (id != EmpDTO.EmployeeId)
            {
                return "修改員工紀錄失敗!";
            }
            Employee Emp = new Employee
            {
                EmployeeId= EmpDTO.EmployeeId,
                FirstName = EmpDTO.FirstName,
                LastName = EmpDTO.LastName,
                Title = EmpDTO.Title,
            };
            _context.Entry(Emp).State = EntityState.Modified;//寫到資料庫要Model 故要新增上方Employee Emp = new Employee
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return "修改員工紀錄失敗!";
                }
                else
                {
                    throw;
                }
            }
            return "修改員工紀錄成功!";   // NoContent()沒有回報
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]//新增
        //public async Task<EmployeeDTO> PostEmployee(EmployeeDTO EmpDTO)
        //{
        //    Employee Emp = new Employee
        //    {
        //        EmployeeId = 0,//不用填:自動編號
        //        FirstName = EmpDTO.FirstName,
        //        LastName = EmpDTO.LastName,
        //        Title = EmpDTO.Title,
        //    };
        //    _context.Employees.Add(Emp);
        //    await _context.SaveChangesAsync();
        //    //return CreatedAtAction("GetEmployee", new { id = employee.EmployeeId }, employee);//CreatedAtAction新增成功之後叫用GetEmployee函式(查詢)
        //    EmpDTO.EmployeeId = Emp.EmployeeId;//取得自動編號的值
        //    return EmpDTO;
        //}

        public async Task<string> PostEmployee(EmployeeDTO EmpDTO)
        {
            Employee Emp = new Employee
            {
                EmployeeId = 0,//不用填:自動編號
                FirstName = EmpDTO.FirstName,
                LastName = EmpDTO.LastName,
                Title = EmpDTO.Title,
            };
            _context.Employees.Add(Emp);
            await _context.SaveChangesAsync();
            return $"員工編號:{Emp.EmployeeId}";
        }
        // DELETE: api/Employees/5
        [HttpDelete("{id}")]//刪除
        public async Task<string> DeleteEmployee(int id)//依據id
        {
            var employee = await _context.Employees.FindAsync(id);//找到要刪除的紀錄
            if (employee == null)
            {
                return "刪除員工紀錄失敗!";
            }

            try
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return "刪除員工關聯紀錄失敗!";
            }
            return "刪除員工紀錄成功!";
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}
