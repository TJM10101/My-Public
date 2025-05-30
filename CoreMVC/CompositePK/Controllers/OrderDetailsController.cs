﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CompositePK.Models;

namespace CompositePK.Controllers
{
    [Route("/OrderDetails/{action=Index}/{ProductId?}/{OrderId?}")]
    public class OrderDetailsController : Controller
    {
        private readonly NorthwindContext _context;

        public OrderDetailsController(NorthwindContext context)
        {
            _context = context;
        }

        // GET: OrderDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.OrderDetails.ToListAsync());
        }

        // GET: OrderDetails/Details/5
        public async Task<IActionResult> Details(int? OrderId, int? ProductId)
        {
            if (OrderId == null || ProductId== null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetails
                .FindAsync(OrderId,ProductId);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // GET: OrderDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,ProductId,UnitPrice,Quantity,Discount")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orderDetail);
        }

        // GET: OrderDetails/Edit/5
        public async Task<IActionResult> Edit(int? OrderId, int? ProductId)
        {
            if (OrderId == null || ProductId == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetails.FindAsync(OrderId, ProductId);
            if (orderDetail == null)
            {
                return NotFound();
            }
            return View(orderDetail);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int OrderId,int ProductId, [Bind("OrderId,ProductId,UnitPrice,Quantity,Discount")] OrderDetail orderDetail)
        {
            if (OrderId != orderDetail.OrderId || ProductId != orderDetail.ProductId )
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderDetailExists(orderDetail.OrderId,orderDetail.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(orderDetail);
        }

        // GET: OrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? OrderId, int? ProductId)
        {
            if (OrderId == null || ProductId == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetails.FindAsync(OrderId,ProductId);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int OrderId,int ProductId)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(OrderId,ProductId);
            if (orderDetail != null)
            {
                _context.OrderDetails.Remove(orderDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderDetailExists(int OrderId, int ProductId)
        {
            return _context.OrderDetails.Any(e => e.OrderId == OrderId 
            && e.ProductId== ProductId);
        }
    }
}
