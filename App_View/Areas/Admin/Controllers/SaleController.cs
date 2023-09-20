using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App_Data.Models;
using App_Data.IRepositories;
using App_Data.Repositories;
using App_View.IServices;
using App_View.Services;

namespace App_View.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SaleController : Controller
    {
        private readonly IAllRepo<Sale> repos;
        DbContextModel context = new DbContextModel();
        DbSet<Sale> sale;
        ISaleService SaleService;
        public SaleController()
        {
            sale = context.Sales;
            AllRepo<Sale> all = new AllRepo<Sale>(context, sale);
            repos = all;
            SaleService = new SaleService();
        }

        // GET: Admin/Sale
        public async Task<IActionResult> ShowAllSale()
        {
            var sales = await SaleService.GetAllSale();
            return View(sales);
        }

        // GET: Admin/Sale/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            var sales = repos.GetAll().FirstOrDefault(c => c.Id == id);
            return View(sales);
        }

        // GET: Admin/Sale/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Sale/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ma,Ten,NgayBatDau,NgayKetThuc,LoaiHinhKm,MucGiam,MoTa,TrangThai")] Sale sale)
        {
            if (await SaleService.CreateSale(sale))
            {
                return RedirectToAction("ShowAllSale");
            }
            else return BadRequest();
        }

        // GET: Admin/Sale/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            var sp = repos.GetAll().FirstOrDefault(x => x.Id == id);

            if (sp == null)
            {
                return NotFound();
            }

            return View(sp);
        }

        // POST: Admin/Sale/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Ma,Ten,NgayBatDau,NgayKetThuc,LoaiHinhKm,MucGiam,MoTa,TrangThai")] Sale sale)
        {
            if (await SaleService.EditSale(sale))
            {
                return RedirectToAction("ShowAllSale");
            }
            else return BadRequest();
        }

        // GET: Admin/Sale/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (await SaleService.DeleteSale(id))
            {
                return RedirectToAction("ShowAllSale");
            }
            else return BadRequest();
        }

        // POST: Admin/Sale/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(Guid id)
        //{
        //    if (_context.Sales == null)
        //    {
        //        return Problem("Entity set 'DbContextModel.Sales'  is null.");
        //    }
        //    var sale = await _context.Sales.FindAsync(id);
        //    if (sale != null)
        //    {
        //        _context.Sales.Remove(sale);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool SaleExists(Guid id)
        //{
        //    return (_context.Sales?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
