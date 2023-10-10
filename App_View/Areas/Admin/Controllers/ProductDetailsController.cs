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
            return View();
        }

        //GET: Admin/ProductDetails
        public async Task<IActionResult> DanhSachSanPhamNgungKinhDoanh()
        {
            return View();
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

        public async Task<IActionResult> ViewAddSale()
        {
            ViewData["IdSale"] = new SelectList(_context.Sales, "Id", "Ten");
            return View();
        }

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


        //GET: Admin/ProductDetails/Delete/5
        [HttpPost]
        public async Task Delete(Guid id)
        {
             await _productDetailService.DeleteProductDetail(id);
        }

    }
}
