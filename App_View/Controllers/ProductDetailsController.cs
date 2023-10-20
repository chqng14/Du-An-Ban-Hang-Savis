using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App_Data.Models;
using App_View.IServices;
using App_Data.ViewModels.ProductDetail;
using App_Data.ViewModels.Filter;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace App_View.Controllers
{
    public class ProductDetailsController : Controller
    {
        private readonly IProductDetailService _productDetailService;

        public ProductDetailsController(IProductDetailService productDetailService)
        {
            _productDetailService = productDetailService;
        }

        // GET: ProductDetails
        public async Task<IActionResult> Index(string brand, string search)
        {
            var data = (await _productDetailService.GetProductItemShopVMsAsync());
            if (!string.IsNullOrEmpty(brand))
            {
                data = data.Where(x => x.Loai!.ToLower() == brand.ToLower()).ToList();
            }
            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(x => x.NameProduct!.ToLower().Contains(search.ToLower())).ToList();
            }
            var model = new FilterVM()
            {
                Items = data.Take(9).ToList(),
                PagingInfor = new PagingInfor()
                {
                    SoItemTrenMotTrang = 9,
                    TongSoItem = data.Count,
                    TrangHienTai = 1
                }
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LoadPartialViewProducts([FromBody] FilterDaTa filterDaTa)
        {
            var data = (await _productDetailService.GetProductItemShopVMsAsync());

            var brand = HttpContext.Request.Query["brand"].ToString();

            if (!string.IsNullOrEmpty(brand))
            {
                data = data.Where(sp=>sp.Loai!.ToLower().Contains(brand.ToLower())).ToList();
            }

            data = data.Where(sp => sp.GiaBan > filterDaTa.GiaMin && sp.GiaBan < filterDaTa.GiaMax).ToList();
            if (filterDaTa.ListCorlor!.Any())
            {
                data = data.Where(sp => filterDaTa.ListCorlor!.Contains(sp.MauSac!)).ToList();
            }

            if (filterDaTa.ListSize!.Any())
            {
                data = data.Where(sp => filterDaTa.ListSize!.Contains(sp.Size!)).ToList();
            }

            return PartialView("_ProductsPartialView", new FilterVM()
            {
                Items = data.Skip((filterDaTa.TrangHienTai - 1) * 9).Take(9).ToList(),
                PagingInfor = new PagingInfor()
                {
                    TrangHienTai = filterDaTa.TrangHienTai,
                    SoItemTrenMotTrang = 9,
                    TongSoItem = data.Count
                }
            });

        }


        // GET: ProductDetails/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var item = await _productDetailService.GetDetailProductAsync(id);
            return View(item);
        }
        [HttpPost]
        public async Task<IActionResult> GetDetails([FromBody] DataProductDetailVm dataProductDetailVm)
        {
            return Json(await _productDetailService.GetProductDetailRespoAsync(dataProductDetailVm));
        }


        //// GET: ProductDetails/Create
        //public IActionResult Create()
        //{
        //    ViewData["IdColor"] = new SelectList(_context.Colors, "Id", "Ma");
        //    ViewData["IdMaterial"] = new SelectList(_context.Materials, "Id", "Ten");
        //    ViewData["IdProduct"] = new SelectList(_context.Products, "Id", "Ma");
        //    ViewData["IdSize"] = new SelectList(_context.Sizes, "Id", "Ma");
        //    ViewData["IdTypeProduct"] = new SelectList(_context.TypeProducts, "Id", "Id");
        //    return View();
        //}

        //// POST: ProductDetails/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,IdProduct,IdColor,IdSize,IdTypeProduct,IdMaterial,BaoHanh,MoTa,SoLuongTon,SoLuongDaBan,NgayTao,IsNoiBat,GiaNhap,GiaBan,TrangThai")] ProductDetails productDetails)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        productDetails.Id = Guid.NewGuid();
        //        _context.Add(productDetails);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["IdColor"] = new SelectList(_context.Colors, "Id", "Ma", productDetails.IdColor);
        //    ViewData["IdMaterial"] = new SelectList(_context.Materials, "Id", "Ten", productDetails.IdMaterial);
        //    ViewData["IdProduct"] = new SelectList(_context.Products, "Id", "Ma", productDetails.IdProduct);
        //    ViewData["IdSize"] = new SelectList(_context.Sizes, "Id", "Ma", productDetails.IdSize);
        //    ViewData["IdTypeProduct"] = new SelectList(_context.TypeProducts, "Id", "Id", productDetails.IdTypeProduct);
        //    return View(productDetails);
        //}

        //// GET: ProductDetails/Edit/5
        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null || _context.ProductDetails == null)
        //    {
        //        return NotFound();
        //    }

        //    var productDetails = await _context.ProductDetails.FindAsync(id);
        //    if (productDetails == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["IdColor"] = new SelectList(_context.Colors, "Id", "Ma", productDetails.IdColor);
        //    ViewData["IdMaterial"] = new SelectList(_context.Materials, "Id", "Ten", productDetails.IdMaterial);
        //    ViewData["IdProduct"] = new SelectList(_context.Products, "Id", "Ma", productDetails.IdProduct);
        //    ViewData["IdSize"] = new SelectList(_context.Sizes, "Id", "Ma", productDetails.IdSize);
        //    ViewData["IdTypeProduct"] = new SelectList(_context.TypeProducts, "Id", "Id", productDetails.IdTypeProduct);
        //    return View(productDetails);
        //}

        //// POST: ProductDetails/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("Id,IdProduct,IdColor,IdSize,IdTypeProduct,IdMaterial,BaoHanh,MoTa,SoLuongTon,SoLuongDaBan,NgayTao,IsNoiBat,GiaNhap,GiaBan,TrangThai")] ProductDetails productDetails)
        //{
        //    if (id != productDetails.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(productDetails);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ProductDetailsExists(productDetails.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["IdColor"] = new SelectList(_context.Colors, "Id", "Ma", productDetails.IdColor);
        //    ViewData["IdMaterial"] = new SelectList(_context.Materials, "Id", "Ten", productDetails.IdMaterial);
        //    ViewData["IdProduct"] = new SelectList(_context.Products, "Id", "Ma", productDetails.IdProduct);
        //    ViewData["IdSize"] = new SelectList(_context.Sizes, "Id", "Ma", productDetails.IdSize);
        //    ViewData["IdTypeProduct"] = new SelectList(_context.TypeProducts, "Id", "Id", productDetails.IdTypeProduct);
        //    return View(productDetails);
        //}

        //// GET: ProductDetails/Delete/5
        //public async Task<IActionResult> Delete(Guid? id)
        //{
        //    if (id == null || _context.ProductDetails == null)
        //    {
        //        return NotFound();
        //    }

        //    var productDetails = await _context.ProductDetails
        //        .Include(p => p.Color)
        //        .Include(p => p.Material)
        //        .Include(p => p.Products)
        //        .Include(p => p.Size)
        //        .Include(p => p.TypeProduct)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (productDetails == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(productDetails);
        //}

        //// POST: ProductDetails/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(Guid id)
        //{
        //    if (_context.ProductDetails == null)
        //    {
        //        return Problem("Entity set 'DbContextModel.ProductDetails'  is null.");
        //    }
        //    var productDetails = await _context.ProductDetails.FindAsync(id);
        //    if (productDetails != null)
        //    {
        //        _context.ProductDetails.Remove(productDetails);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool ProductDetailsExists(Guid id)
        //{
        //  return (_context.ProductDetails?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
