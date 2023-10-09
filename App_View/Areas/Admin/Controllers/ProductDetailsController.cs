using App_Data.Models;
using App_Data.ViewModels.ProductDetail;
using App_View.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace App_View.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductDetailsController : Controller
    {
        private readonly IProductDetailService _productDetailService;
        private readonly DbContextModel _context;

        public ProductDetailsController(IProductDetailService productDetailService)
        {
            _productDetailService = productDetailService;
            _context = new DbContextModel();
        }

        [HttpPost]
        public async Task<IActionResult> CheckProductDetailForAddOrUpdate([FromBody] ProductDetailDTO productDetailDTO)
        {
            var response = await _productDetailService.GetProductDetailForUpdateOrAdd(productDetailDTO);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Content(content, "application/json");

            }
            return BadRequest();
        }

        //GET: Admin/ProductDetails
        public async Task<IActionResult> Index()
        {
            return View(await _productDetailService.GetListProductViewModelAsync());
        }

        public async Task<IActionResult> LoadPartialViewChiTietSanPham(Guid id)
        {
            return PartialView("_DetailPartialView", await _productDetailService.GetProductVMsAsync(id));
        }

        public class ListGuidDTO
        {
            public List<Guid>? LstGuid { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> LoadPartialviewDanhSachUpdate([FromBody]ListGuidDTO listGuidDTO)
        {
            var model = (await _productDetailService.GetListProductViewModelAsync())!
                .Where(sp=>listGuidDTO.LstGuid!.Contains(sp.Id));
            return PartialView("_DanhSachSanPhamUpdate", model);
        }

        public async Task<IActionResult> CreateNameProduct(string nameProduct)
        {
            return Ok(await _productDetailService.CreateProductAsynsc(nameProduct));
        }


        // GET: Admin/ProductDetails/Details/5
        //public async Task<IActionResult> Details(Guid? id)
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

        // GET: Admin/ProductDetails/Create

        public IActionResult ManageProduct()
        {
            ViewData["IdColor"] = new SelectList(_context.Colors, "Id", "Ten");
            ViewData["IdMaterial"] = new SelectList(_context.Materials, "Id", "Ten");
            ViewData["IdProduct"] = new SelectList(_context.Products, "Id", "Ten");
            ViewData["IdSize"] = new SelectList(_context.Sizes, "Id", "Size1");
            ViewData["IdTypeProduct"] = new SelectList(_context.TypeProducts, "Id", "Ten");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts(int draw, int start, int length, string searchValue)
        {
            var query = (await _productDetailService.GetListProductViewModelAsync())
                .Skip(start)
                .Take(length)
                .ToList();

            if (!string.IsNullOrEmpty(searchValue))
            {
                string searchValueLower = searchValue.ToLower();
                query = (await _productDetailService.GetListProductViewModelAsync()).Where(x => x.NameProduct.ToLower().Contains(searchValueLower) || x.Loai.ToLower().Contains(searchValueLower) || x.ChatLieu.ToLower().Contains(searchValueLower) || x.MauSac.ToLower().Contains(searchValueLower))
                .Skip(start)
                .Take(length)
                .ToList();

            }

            var totalRecords = (await _productDetailService.GetListProductViewModelAsync()).Count();
            
            return Json(new
            {
                draw = draw,
                recordsTotal = totalRecords,
                recordsFiltered = totalRecords,
                data = query
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDetailDTO productDetailDTO)
        {
            var response = await _productDetailService.CreatProductDetailAsync(productDetailDTO);
            if (response.IsSuccessStatusCode)
            {
                return Content(await response.Content.ReadAsStringAsync(), "application/json");
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task UpdateProduct([FromBody] ProductDetailDTO productDetailDTO)
        {
            await _productDetailService.UpdateProductDetailAsync(productDetailDTO);
        }

        [HttpPost]
        public async Task UpdateProductDTO([FromBody]ProductUpdateDTO productUpdateDTO)
        {
            await _productDetailService.UpdateProductAsync(productUpdateDTO);
        }



        // POST: Admin/ProductDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,IdProduct,IdColor,IdSize,IdTypeProduct,IdMaterial,BaoHanh,MoTa,SoLuongTon,GiaNhap,GiaBan,TrangThai")] ProductDetails productDetails)
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

        // GET: Admin/ProductDetails/Edit/5
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

        // POST: Admin/ProductDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("Id,IdProduct,IdColor,IdSize,IdTypeProduct,IdMaterial,BaoHanh,MoTa,SoLuongTon,GiaNhap,GiaBan,TrangThai")] ProductDetails productDetails)
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

        //GET: Admin/ProductDetails/Delete/5
        [HttpPost]
        public async Task Delete(Guid id)
        {
             await _productDetailService.DeleteProductDetail(id);
        }

        // POST: Admin/ProductDetails/Delete/5
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
