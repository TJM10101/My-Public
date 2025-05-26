using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CustomersWebsite.Models;

namespace CustomersWebsite.Controllers
{
    [Route("Customers/{action=Index}/{CustomerID?}")]
    public class CustomersController : Controller
    {
        private readonly NorthwindContext _context;

        public CustomersController(NorthwindContext context)
        {
            _context = context;
        }
        //從DI容器取東西

        // GET: Customers
        public async Task<IActionResult> Index()//看到Task即 三代非同步 IActionResult泛型
        {
            return View(_context.Customers);
        }


        // GET: Customers/Details/5
        public async Task<IActionResult> Details(string CustomerID) //非同步傳回 IActionResult
        {
            if (CustomerID == null)
            {
                return NotFound();//404
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerId == CustomerID);//FirstOrDefault拿第一筆 沒有的話就拿預設值
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()//生畫面用的
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]//form預設動詞為post
        [ValidateAntiForgeryToken]//檢查防偽標籤
        public async Task<IActionResult> Create([Bind("CustomerId,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax")] Customer customer)
        {
            if (ModelState.IsValid)//sever端驗證 做在Enitity or View Model裡面  驗證通過
            {
                _context.Add(customer); //寫到資料庫
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));//叫用Index action函式 會回到Index
            }
            return View(customer);//停留在頁面上
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(string CustomerID)
        {
            if (CustomerID == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(CustomerID);//傳主索引鍵
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string CustomerID, [Bind("CustomerId,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax")] Customer customer)
        //先比對ID 再比對Entity (可以不要比對ID)
        {
            if (CustomerID != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))//是否存在
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));//過了就回首頁
            }
            return View(customer);
        }
      
        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(string CustomerID)
        {
            if (CustomerID == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerId == CustomerID);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]//Action函式名稱還是叫("Delete") 
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string CustomerID)//因為C#中不能同名傳回值也相同
        {
            var customer = await _context.Customers.FindAsync(CustomerID);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(string id)//非action函式 action函式不能是private
        {
            return _context.Customers.Any(e => e.CustomerId == id); //有無符合這條件的任何紀錄 Any傳回true false(Where就是傳回值)
        }
    }
}
