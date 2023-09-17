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
    public class ProductDetailController
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

        [HttpPost("create-productdetail")]
        public IActionResult CreateProductDetail([FromBody] ProductDetailDTO productDetailDTO)
        {
            productDetailDTO.TrangThai = 1;
            var productDetail = _mapper.Map<ProductDetails>(productDetailDTO);
            return new OkObjectResult(new { success = _allRepoProductDetail.AddItem(productDetail), id = productDetail.Id });
        }

        private ProductViewModel CreatProductViewModel(ProductDetails item)
        {
            return new ProductViewModel
            {
                Id = item.Id,
                BaoHanh = item.BaoHanh,
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
            };
        }

        [HttpGet("get-productdetail/{id}")]
        public ProductDetails? GetProductDetail(Guid idProductDetail)
        {
            return _allRepoProductDetail.GetAll().FirstOrDefault(pro => pro.Id == idProductDetail);
        }

        [HttpGet("get-list-productdetail")]
        public IActionResult GetListProductDetail()
        {
            var listProductDetailViewModel = _allRepoProductDetail.GetAll().Select(item => CreatProductViewModel(item)).ToList();
            return new OkObjectResult(listProductDetailViewModel);
        }

        [HttpGet("get-add-or-update")]
        public IActionResult GetProductDetailForAddOrUpdate([FromBody] ProductDetailDTO productDetailDTO)
        {
            var productDetails = _allRepoProductDetail.GetAll()
                .FirstOrDefault(pro => pro.IdProduct == productDetailDTO.IdProduct && pro.IdColor == productDetailDTO.IdColor && pro.IdSize == productDetailDTO.IdSize && pro.IdTypeProduct == productDetailDTO.IdTypeProduct && pro.IdMaterial == productDetailDTO.IdMaterial);

            if (productDetails != null)
            {
                var productDetaiDTOMap = _mapper.Map<ProductDetailDTO>(productDetails);
                productDetaiDTOMap.LstTenAnh = _allImages.GetAll()
                                .Where(img => img.IdProductDetail == productDetaiDTOMap.Id)
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
                productDetailRemove!.TrangThai = 0;
                _allRepoProductDetail.EditItem(productDetailRemove);
                return true;
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
                if (GetProductDetail(productDetailDTO.Id) == null)
                {
                    return false;
                }
                var productDetail = _mapper.Map<ProductDetails>(productDetailDTO);
                _allRepoProductDetail.EditItem(productDetail);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

        }
    }
}
