using App_Data.Models;
using App_Data.ViewModels.ProductDetail;
using App_View.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
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
        public IActionResult Index()
        {
            return View();
        }

        //GET: Admin/ProductDetails
        public IActionResult DanhSachSanPhamNgungKinhDoanh()
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
        public async Task<IActionResult> LoadPartialviewDanhSachUpdate([FromBody] ListGuidDTO listGuidDTO)
        {
            var model = (await _productDetailService.GetListProductViewModelAsync())!
                .Where(sp => listGuidDTO.LstGuid!.Contains(sp.Id));
            return PartialView("_DanhSachSanPhamUpdate", model);
        }

        #region AddNameProduct
        public class ProductDTO
        {
            public string? nameProduct { get; set; }
        }
        [HttpPost]
        public async Task<IActionResult> CreateNameProduct([FromBody] ProductDTO productDTO)
        {
            if (!string.IsNullOrEmpty(productDTO.nameProduct))
            {
                return Ok(await _productDetailService.CreateProductAsynsc(productDTO.nameProduct));
            }
            return BadRequest();
        }
        #endregion

        #region AddNameTypeProduct
        public class TypeProductDTO
        {
            public string? nameTypeProduct { get; set; }
        }
        [HttpPost]
        public async Task<IActionResult> CreateNameTypeProduct([FromBody] TypeProductDTO typeProducttDTO)
        {
            if (!string.IsNullOrEmpty(typeProducttDTO.nameTypeProduct))
            {
                try
                {
                    var typeProduct = new TypeProduct()
                    {
                        Id = Guid.NewGuid(),
                        Ma = "TY" + (_context.TypeProducts.ToList().Count + 1),
                        Ten = typeProducttDTO.nameTypeProduct,
                        TrangThai = 0
                    };
                    await _context.TypeProducts.AddAsync(typeProduct);
                    await _context.SaveChangesAsync();
                    return Ok(new { id = typeProduct.Id, name = typeProduct.Ten });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return BadRequest();
                }

            }
            return BadRequest();
        }
        #endregion

        #region AddNameMaterial
        public class MaterialDTO
        {
            public string? nameMaterial { get; set; }
        }
        [HttpPost]
        public async Task<IActionResult> CreateNameMaterial([FromBody] MaterialDTO materialDTO)
        {
            if (!string.IsNullOrEmpty(materialDTO.nameMaterial))
            {
                try
                {
                    var material = new Material()
                    {
                        Id = Guid.NewGuid(),
                        Ten = materialDTO.nameMaterial,
                        TrangThai = 0
                    };
                    await _context.Materials.AddAsync(material);
                    await _context.SaveChangesAsync();
                    return Ok(new { id = material.Id, name = material.Ten });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return BadRequest();
                }

            }
            return BadRequest();
        }
        #endregion

        #region AddNameColor
        public class ColorDTO
        {
            public string? nameColor { get; set; }
        }
        [HttpPost]
        public async Task<IActionResult> CreateNameColor([FromBody] ColorDTO colorDTO)
        {
            if (!string.IsNullOrEmpty(colorDTO.nameColor))
            {
                try
                {
                    var color = new Color()
                    {
                        Id = Guid.NewGuid(),
                        Ten = colorDTO.nameColor,
                        Ma = "MS" + (_context.TypeProducts.ToList().Count + 1),
                        TrangThai = 0
                    };
                    await _context.Colors.AddAsync(color);
                    await _context.SaveChangesAsync();
                    return Ok(new { id = color.Id, name = color.Ten });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return BadRequest();
                }

            }
            return BadRequest();
        }
        #endregion

        #region AddNameSize
        public class SizeDTO
        {
            public string? nameSize { get; set; }
        }
        [HttpPost]
        public async Task<IActionResult> CreateNameSize([FromBody] SizeDTO sizeDTO)
        {
            if (!string.IsNullOrEmpty(sizeDTO.nameSize))
            {
                try
                {
                    var size = new Size()
                    {
                        Id = Guid.NewGuid(),
                        Size1 = sizeDTO.nameSize,
                        Ma = "Siz" + (_context.Sizes.ToList().Count + 1),
                        Cm = 1,
                        TrangThai = 0
                    };
                    await _context.Sizes.AddAsync(size);
                    await _context.SaveChangesAsync();
                    return Ok(new { id = size.Id, name = size.Size1 });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return BadRequest();
                }

            }
            return BadRequest();
        }
        #endregion


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
            var query = (await _productDetailService.GetListProductViewModelAsync())!
                .Skip(start)
                .Take(length)
                .ToList();

            if (!string.IsNullOrEmpty(searchValue))
            {
                string searchValueLower = searchValue.ToLower();
                query = (await _productDetailService.GetListProductViewModelAsync())!.Where(x => x.NameProduct!.ToLower().Contains(searchValueLower) || x.Loai!.ToLower().Contains(searchValueLower) || x.ChatLieu!.ToLower().Contains(searchValueLower) || x.MauSac!.ToLower().Contains(searchValueLower))
                .Skip(start)
                .Take(length)
                .ToList();

            }

            var totalRecords = (await _productDetailService.GetListProductViewModelAsync())!.Count();

            return Json(new
            {
                draw = draw,
                recordsTotal = totalRecords,
                recordsFiltered = totalRecords,
                data = query
            });
        }
        #region ViewSanPhamNgungKinhDoanh

        [HttpGet]
        public async Task<IActionResult> GetProductsNgungKinhDoanh(int draw, int start, int length, string searchValue)
        {
            var query = (await _productDetailService.GetLstProductDetailViewModelNgungKinhDoanhAynsc())!
                .Skip(start)
                .Take(length)
                .ToList();

            if (!string.IsNullOrEmpty(searchValue))
            {
                string searchValueLower = searchValue.ToLower();
                query = (await _productDetailService.GetLstProductDetailViewModelNgungKinhDoanhAynsc())!.Where(x => x.NameProduct!.ToLower().Contains(searchValueLower) || x.Loai!.ToLower().Contains(searchValueLower) || x.ChatLieu!.ToLower().Contains(searchValueLower) || x.MauSac!.ToLower().Contains(searchValueLower))
                .Skip(start)
                .Take(length)
                .ToList();

            }

            var totalRecords = (await _productDetailService.GetLstProductDetailViewModelNgungKinhDoanhAynsc())!.Count();

            return Json(new
            {
                draw = draw,
                recordsTotal = totalRecords,
                recordsFiltered = totalRecords,
                data = query
            });
        }
        #endregion

        #region KhoiPhucKinhDoanh
        public async Task<IActionResult> KhoiPhucKinhDoanh(Guid IdSanPham)
        {
            try
            {
                var sanPham = await _context.ProductDetails.FirstOrDefaultAsync(x => x.Id == IdSanPham);
                if (sanPham == null) { return NotFound(); }
                sanPham.TrangThai = 0;
                _context.ProductDetails.Update(sanPham);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }
        }
        #endregion

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
        public async Task UpdateProductDTO([FromBody] ProductUpdateDTO productUpdateDTO)
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
