using App_Data.Configurations;
using App_Data.IRepositories;
using App_Data.Models;
using App_Data.ViewModels.ProductDetail;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace App_Api.Controllers
{
    public class ProductDetailController
    {
        private readonly IAllRepo<ProductDetails> _allRepoProductDetail;
        private readonly IAllRepo<Material> _allRepoMaterial;
        private readonly IAllRepo<TypeProduct> _allRepoTypeProduct;
        private readonly IAllRepo<Color> _allRepoColor;
        private readonly IAllRepo<Product> _allRepoProduct;
        private readonly IAllRepo<Size> _allRepoSize;
        public ProductDetailController(IAllRepo<ProductDetails> allRepoProductDetail, IAllRepo<Material> allRepoMaterial, IAllRepo<TypeProduct> allRepoTypeProduct, IAllRepo<Color> allRepoColor, IAllRepo<Product> allRepoProduct, IAllRepo<Size> allRepoSize)
        {
            _allRepoProductDetail = allRepoProductDetail;
            _allRepoMaterial = allRepoMaterial;
            _allRepoTypeProduct = allRepoTypeProduct;
            _allRepoColor = allRepoColor;
            _allRepoProduct = allRepoProduct;
            _allRepoSize = allRepoSize;
        }

        [HttpPost("create-productdetail")]
        public IActionResult CreateProductDetail([FromBody] ProductDetailDTO productDetailDTO)
        {
            productDetailDTO.TrangThai = 1;
            var productDetail = new MapperConfiguration(cfg =>
                cfg.CreateMap<ProductDetailDTO, ProductDetails>()
            ).CreateMapper().Map<ProductDetails>(productDetailDTO);

            return new OkObjectResult(new { success = _allRepoProductDetail.AddItem(productDetail), id = productDetail.Id });
        }

        [HttpGet("get-productdetail")]
        public ProductDetails? GetProductDetail(Guid idProductDetail)
        {
            return _allRepoProductDetail.GetAll().FirstOrDefault(pro => pro.Id == idProductDetail);
        }

        [HttpGet("get-list-productdetail")]
        public IActionResult GetListProductDetail()
        {
            var listProductDetailViewModel = _allRepoProductDetail.GetAll().Select(item => new ProductViewModel
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

            }).ToList();
            return new OkObjectResult(listProductDetailViewModel);
        }

        [HttpDelete("delete")]
        public bool DeleteProductDetail(List<Guid> lstGuid)
        {
            try
            {
                foreach (var id in lstGuid)
                {
                    _allRepoProductDetail.RemoveItem(_allRepoProductDetail.GetAll().FirstOrDefault(pro => pro.Id == id));
                }
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
                var productDetail = new MapperConfiguration(cfg =>
                cfg.CreateMap<ProductDetailDTO, ProductDetails>()
            ).CreateMapper().Map<ProductDetails>(productDetailDTO);
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
