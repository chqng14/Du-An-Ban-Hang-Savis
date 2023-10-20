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
using SixLabors.ImageSharp.Formats.Jpeg;
using System.Net.Http;

namespace App_View.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SaleController : Controller
    {
        private readonly IAllRepo<Sale> repos;
        DbContextModel context = new DbContextModel();
        DbSet<Sale> sale;
        ISaleService SaleService;
        HttpClient _httpClient;
        public SaleController()
        {
            sale = context.Sales;
            AllRepo<Sale> all = new AllRepo<Sale>(context, sale);
            repos = all;
            _httpClient = new HttpClient();
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
        public async Task<IActionResult> Create(Sale sale, IFormFile formFile)
        {

            //using (var content = new MultipartFormDataContent())
            //{
            //    // Thêm dữ liệu của Sale vào MultipartFormDataContent

            //    content.Add(new StringContent(sale.Ma), "ma");
            //    content.Add(new StringContent(sale.Ten), "ten");
            //    content.Add(new StringContent(sale.NgayBatDau.ToString()), "ngaybatdau");
            //    content.Add(new StringContent(sale.NgayKetThuc.ToString()), "ngayketthuc");
            //    content.Add(new StringContent(sale.LoaiHinhKm), "LoaiHinhKm");
            //    content.Add(new StringContent(sale.MoTa), "mota");
            //    content.Add(new StringContent(sale.MucGiam.ToString()), "mucgiam");
            //    content.Add(new StringContent(sale.TrangThai.ToString()), "trangthai");

            //    if (formFile != null && formFile.Length > 0)
            //    {
            //        // Tạo StreamContent cho tệp tin ảnh
            //        var streamContent = new StreamContent(formFile.OpenReadStream());
            //        //streamContent.Headers.Add("Content-Type", formFile.ContentType);
            //        content.Add(streamContent, "formFile", formFile.FileName); // 'formFile' phải trùng với tên tham số trong API

            //        var response = await _httpClient.PostAsync("https://localhost:7165/api/Sale/create-sale", content); // Đảm bảo URL là "/api/Sale/create-sale"
            //        if (response.IsSuccessStatusCode)
            //        {
            //            var responseContent = await response.Content.ReadAsStringAsync();
            //            Console.WriteLine("Create successful. Response: " + responseContent);
            //            return RedirectToAction("ShowAllSale");
            //        }
            //        else
            //        {
            //            var responseContent = await response.Content.ReadAsStringAsync();
            //            Console.WriteLine("Create failed. Response: " + responseContent);
            //            return BadRequest();
            //        }
            //    }
            //    return BadRequest();
            //}

            if (await SaleService.CreateSale(sale, formFile))
            {
                return RedirectToAction("ShowAllSale");
            }
            else
            {
                return BadRequest();
            }
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
