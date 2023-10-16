using App_Data.Configurations;
using App_Data.IRepositories;
using App_Data.Models;
using App_Data.ViewModels.ProductDetail;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace App_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailController : ControllerBase
    {
        private readonly IAllRepo<ProductDetails> _allRepoProductDetail;
        private readonly IAllRepo<Material> _allRepoMaterial;
        private readonly IAllRepo<TypeProduct> _allRepoTypeProduct;
        private readonly IAllRepo<App_Data.Models.Color> _allRepoColor;
        private readonly IAllRepo<Product> _allRepoProduct;
        private readonly IAllRepo<App_Data.Models.Size> _allRepoSize;
        private readonly IAllRepo<App_Data.Models.Images> _allImages;
        private readonly IMapper _mapper;
        public ProductDetailController(IAllRepo<ProductDetails> allRepoProductDetail, IAllRepo<Material> allRepoMaterial, IAllRepo<TypeProduct> allRepoTypeProduct, IAllRepo<App_Data.Models.Color> allRepoColor, IAllRepo<Product> allRepoProduct, IAllRepo<App_Data.Models.Size> allRepoSize, IMapper mapper, IAllRepo<Images> allImages)
        {
            _allRepoProductDetail = allRepoProductDetail;
            _allRepoMaterial = allRepoMaterial;
            _allRepoTypeProduct = allRepoTypeProduct;
            _allRepoColor = allRepoColor;
            _allRepoProduct = allRepoProduct;
            _allRepoSize = allRepoSize;
            _mapper = mapper;
            _allImages = allImages;
        }

        private ProductDetailResponseVM CreateDetailProductResponseVM(ProductDetails item)
        {
            var lstBienThe = _allRepoProductDetail.GetAll()
                .Where(product => product.TrangThai == 0 &&
                    product.IdProduct == item.IdProduct &&
                    product.IdMaterial == item.IdMaterial &&
                    product.IdTypeProduct == item.IdTypeProduct).ToList();

            var listMauSac = lstBienThe
                .Join(
                    _allRepoColor.GetAll().Where(x => x.TrangThai == 0).ToList(),
                    pr => pr.IdColor,
                    cl => cl.Id,
                    (pr, cl) => cl.Ten
                )
                .Distinct()
                .ToList();

            var listSize = lstBienThe.Where(it => it.IdColor == item.IdColor)
                .Join(
                    _allRepoSize.GetAll().Where(x => x.TrangThai == 0).ToList(),
                    pr => pr.IdSize,
                    s => s.Id,
                    (pr, s) => s.Size1
                )
                .Distinct().OrderBy(x => x)
                .ToList();
            var sevenDaysAgo = DateTime.Now.AddDays(-7);
            return new ProductDetailResponseVM
            {
                Id = item.Id,
                ChatLieu = _allRepoMaterial.GetAll().FirstOrDefault(x => x.Id == item.IdMaterial)?.Ten,
                GiaBan = item.GiaBan,
                GiaNhap = item.GiaNhap,
                Loai = _allRepoTypeProduct.GetAll().FirstOrDefault(lo => lo.Id == item.IdTypeProduct)?.Ten,
                MauSac = _allRepoColor.GetAll().FirstOrDefault(co => co.Id == item.IdColor)?.Ten,
                MoTa = item.MoTa,
                NameProduct = _allRepoProduct.GetAll().FirstOrDefault(na => na.Id == item.IdProduct)?.Ten,
                Size = _allRepoSize.GetAll().FirstOrDefault(si => si.Id == item.IdSize)?.Size1,
                SoLuongTon = item.SoLuongTon,
                TrangThai = item.TrangThai,
                IsNoiBat = item.IsNoiBat,
                IsNew = item.NgayTao >= sevenDaysAgo,
                SoLuongDaBan = item.SoLuongDaBan,
                LstTenAnh = _allImages.GetAll()
                                .Where(img => img.TrangThai == 1 && img.IdProductDetail == item.Id)
                                .Select(x => x.DuongDan)
                                .ToList(),
                ListSize = listSize
            };
        }

        [HttpPost("get-detail-Product-respo")]
        public ProductDetailResponseVM? GetProductDetailRespo(DataProductDetailVm dataProductDetailVm)
        {
            var listProduct = _allRepoProductDetail.GetAll().Where(x => x.TrangThai == 0).ToList();
            var productDetailRes = new ProductDetailResponseVM();
            var listProductDetailResponseVM = listProduct.Select(item => CreateDetailProductResponseVM(item)).ToList();

            if (dataProductDetailVm.Size == null)
            {
                listProductDetailResponseVM = listProductDetailResponseVM.
                    Where(x => x.NameProduct == dataProductDetailVm.NameProduct && 
                    x.ChatLieu == dataProductDetailVm.ChatLieu && 
                    x.Loai == dataProductDetailVm.Loai && 
                    x.MauSac == dataProductDetailVm.MauSac).ToList();
                productDetailRes = listProductDetailResponseVM.FirstOrDefault();
                return productDetailRes;
            }
            
            productDetailRes = listProductDetailResponseVM.FirstOrDefault(x => x.NameProduct == dataProductDetailVm.NameProduct && x.ChatLieu == dataProductDetailVm.ChatLieu && x.Size == dataProductDetailVm.Size && x.Loai == dataProductDetailVm.Loai && x.MauSac == dataProductDetailVm.MauSac);
            return productDetailRes;
        }

        private ProductDetailVM CreateDetailProduct(ProductDetails item)
        {
            var lstBienThe = _allRepoProductDetail.GetAll()
                .Where(product => product.TrangThai == 0 &&
                    product.IdProduct == item.IdProduct &&
                    product.IdMaterial == item.IdMaterial &&
                    product.IdTypeProduct == item.IdTypeProduct).ToList();

            var listMauSac = lstBienThe
                .Join(
                    _allRepoColor.GetAll().Where(x => x.TrangThai == 0).ToList(),
                    pr => pr.IdColor,
                    cl => cl.Id,
                    (pr, cl) => cl.Ten
                )
                .Distinct()
                .ToList();

            var listSize = lstBienThe.Where(it => it.IdColor == item.IdColor)
                .Join(
                    _allRepoSize.GetAll().Where(x => x.TrangThai == 0).ToList(),
                    pr => pr.IdSize,
                    s => s.Id,
                    (pr, s) => s.Size1
                )
                .Distinct().OrderBy(x => x)
                .ToList();
            var sevenDaysAgo = DateTime.Now.AddDays(-7);
            return new ProductDetailVM
            {
                Id = item.Id,
                ChatLieu = _allRepoMaterial.GetAll().FirstOrDefault(x => x.Id == item.IdMaterial)?.Ten,
                GiaBan = item.GiaBan,
                Loai = _allRepoTypeProduct.GetAll().FirstOrDefault(lo => lo.Id == item.IdTypeProduct)?.Ten,
                MauSac = _allRepoColor.GetAll().FirstOrDefault(co => co.Id == item.IdColor)?.Ten,
                MoTa = item.MoTa,
                NameProduct = _allRepoProduct.GetAll().FirstOrDefault(na => na.Id == item.IdProduct)?.Ten,
                Size = _allRepoSize.GetAll().FirstOrDefault(si => si.Id == item.IdSize)?.Size1,
                SoLuongTon = item.SoLuongTon,
                TrangThai = item.TrangThai,
                IsNoiBat = item.IsNoiBat,
                IsNew = item.NgayTao >= sevenDaysAgo,
                SoLuongDaBan = item.SoLuongDaBan,
                LstTenAnh = _allImages.GetAll()
                                .Where(img => img.TrangThai == 1 && img.IdProductDetail == item.Id)
                                .Select(x => x.DuongDan)
                                .ToList(),
                ListMauSac = listMauSac,
                ListSize = listSize
            };

        }

        [HttpPost("create-productdetail")]
        public IActionResult CreateProductDetail([FromBody] ProductDetailDTO productDetailDTO)
        {
            var productDetail = _mapper.Map<ProductDetails>(productDetailDTO);
            productDetail.TrangThai = 0;
            productDetail.SoLuongDaBan = 0;
            productDetail.NgayTao = DateTime.Now;
            return new OkObjectResult(new { success = _allRepoProductDetail.AddItem(productDetail), id = productDetail.Id });
        }

        private ProductViewModel? CreatProductViewModel(ProductDetails item)
        {
            var sevenDaysAgo = DateTime.Now.AddDays(-7);
            return new ProductViewModel
            {
                Id = item.Id,
                ChatLieu = _allRepoMaterial.GetAll().FirstOrDefault(x => x.Id == item.IdMaterial)?.Ten,
                GiaBan = item.GiaBan,
                GiaNhap = item.GiaNhap,
                Loai = _allRepoTypeProduct.GetAll().FirstOrDefault(lo => lo.Id == item.IdTypeProduct)?.Ten,
                MauSac = _allRepoColor.GetAll().FirstOrDefault(co => co.Id == item.IdColor)?.Ten,
                MoTa = item.MoTa,
                NameProduct = _allRepoProduct.GetAll().FirstOrDefault(na => na.Id == item.IdProduct)?.Ten,
                Size = _allRepoSize.GetAll().FirstOrDefault(si => si.Id == item.IdSize)?.Size1,
                SoLuongTon = item.SoLuongTon,
                TrangThai = item.TrangThai,
                IsNoiBat = item.IsNoiBat,
                IsNew = item.NgayTao >= sevenDaysAgo,
                SoLuongDaBan = item.SoLuongDaBan,
                LstTenAnh = _allImages.GetAll()
                                .Where(img => img.TrangThai == 1 && img.IdProductDetail == item.Id)
                                .Select(x => x.DuongDan)
                                .ToList()
            };
        }

        private ProductItemShopVM CreatProductShop(ProductDetails item)
        {
            var sevenDaysAgo = DateTime.Now.AddDays(-7);
            return new ProductItemShopVM
            {
                Id = item.Id,
                GiaBan = item.GiaBan,
                Loai = _allRepoTypeProduct.GetAll().FirstOrDefault(lo => lo.Id == item.IdTypeProduct)?.Ten,
                NameProduct = _allRepoProduct.GetAll().FirstOrDefault(na => na.Id == item.IdProduct)?.Ten,
                SoLuongTon = item.SoLuongTon,
                IsNoiBat = item.IsNoiBat,
                IsNew = item.NgayTao >= sevenDaysAgo,
                SoLuongDaBan = item.SoLuongDaBan,
                LstTenAnh = _allImages.GetAll()
                                .Where(img => img.TrangThai == 1 && img.IdProductDetail == item.Id).Take(2).Select(item => item.DuongDan).ToList(),
            };
        }

        [HttpGet("get-productdetail/{id}")]
        public ProductDetails? GetProductDetail(Guid id)
        {
            return _allRepoProductDetail.GetAll().FirstOrDefault(pro => pro.Id == id);
        }

        [HttpGet("get-productViewModel/{id}")]
        public ProductViewModel? GetProductVM(Guid id)
        {
            var item = _allRepoProductDetail.GetAll().FirstOrDefault(pro => pro.Id == id);
            if (item == null) return null;
            return CreatProductViewModel(item!);
        }

        [HttpGet("get-detail-product/{id}")]
        public ProductDetailVM? GetDetailProductVM(Guid id)
        {
            var item = _allRepoProductDetail.GetAll().FirstOrDefault(pro => pro.Id == id);
            if (item == null) return null;
            return CreateDetailProduct(item!);
        }

        [HttpGet("get-list-ProductViewModel-Ngung-kinh-doanh")]
        public List<ProductViewModel> GetListProductNgungKinhDoanh()
        {
            var listProductDetailViewModelNgungKinhDoanh = _allRepoProductDetail.GetAll().Where(sp => sp.TrangThai == 1).Select(item => CreatProductViewModel(item)).ToList();
            return listProductDetailViewModelNgungKinhDoanh!;

        }

        [HttpGet("get-list-productdetail")]
        public IActionResult GetListProductDetail()
        {
            var listProductDetailViewModel = _allRepoProductDetail.GetAll().Where(sp=>sp.TrangThai==0).Select(item => CreatProductViewModel(item)).ToList();
            return new OkObjectResult(listProductDetailViewModel);
        }

        [HttpGet("get-list-productItemShop")]
        public List<ProductItemShopVM> GetListProductItemShop()
        {
            var listProductVM = _allRepoProductDetail.GetAll().Where(item=>item.TrangThai==0).GroupBy(x => new { x.IdMaterial, x.IdProduct, x.IdTypeProduct }).Select(gr => gr.First()).ToList();
            var lstItemShop = listProductVM.Select(item => CreatProductShop(item)).ToList();
            return lstItemShop;
        }

        [HttpPost("get-add-or-update")]
        public IActionResult GetProductDetailForAddOrUpdate([FromBody] ProductDetailDTO productDetailDTO)
        {
            var productDetails = _allRepoProductDetail.GetAll()
                .FirstOrDefault(pro => pro.IdProduct == productDetailDTO.IdProduct && pro.IdColor == productDetailDTO.IdColor && pro.IdSize == productDetailDTO.IdSize && pro.IdTypeProduct == productDetailDTO.IdTypeProduct && pro.IdMaterial == productDetailDTO.IdMaterial);

            if (productDetails != null)
            {
                var productDetaiDTOMap = _mapper.Map<ProductDetailDTO>(productDetails);
                productDetaiDTOMap.LstTenAnh = _allImages.GetAll()
                                .Where(img => img.TrangThai == 1 && img.IdProductDetail == productDetaiDTOMap.Id)
                                .Select(x => x.DuongDan)
                                .ToList();
                return new OkObjectResult(new { success = true, data = productDetaiDTOMap });
            }

            return new OkObjectResult(new { success = false });
        }

        [HttpDelete("delete-lst-product")]
        public bool DeleteListProductDetail(List<Guid> lstGuid)
        {
            try
            {
                foreach (var id in lstGuid)
                {
                    var productDetailRemove = _allRepoProductDetail.GetAll().FirstOrDefault(pro => pro.Id == id);
                    productDetailRemove!.TrangThai = 0;
                    _allRepoProductDetail.EditItem(productDetailRemove);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        [HttpDelete("delete-product-detail/{id}")]
        public bool DeleteProductDetail(Guid id)
        {
            try
            {
                var productDetailRemove = _allRepoProductDetail.GetAll().FirstOrDefault(pro => pro.Id == id);
                productDetailRemove!.TrangThai = 1;
                return _allRepoProductDetail.EditItem(productDetailRemove);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        [HttpPut("update")]
        public bool Update([FromBody] ProductDetailDTO productDetailDTO)
        {
            try
            {
                var existingProductDetail = GetProductDetail(productDetailDTO.Id);
                if (existingProductDetail == null)
                {
                    return false;
                }
                _mapper.Map(productDetailDTO, existingProductDetail);
                return _allRepoProductDetail.EditItem(existingProductDetail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

        }

        [HttpPut("update-productDTO")]
        public bool UpdateListProductDTO([FromBody] ProductUpdateDTO productUpdateDetailDTO)
        {
            try
            {
                var existingProductDetail = GetProductDetail(productUpdateDetailDTO.Id);
                if (existingProductDetail == null)
                {
                    return false;
                }
                _mapper.Map(productUpdateDetailDTO, existingProductDetail);
                return _allRepoProductDetail.EditItem(existingProductDetail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

        }

        //Add sản phẩm
        [HttpGet("create-product/{productName}")]
        public Product? CreateProduct(string productName)
        {
            try
            {
                var product = new Product()
                {
                    Id = Guid.NewGuid(),
                    Ma = _allRepoProduct.GetAll().Any() ? "SP1" : "SP" + (_allRepoProduct.GetAll().Count() + 1),
                    ProductDetails = null,
                    Ten = productName,
                    TrangThai = 0
                };
                if (_allRepoProduct.AddItem(product))
                {
                    return product;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }


    }
}
