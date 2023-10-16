using App_Data.Models;
using App_Data.ViewModels.ProductDetail;
using Microsoft.AspNetCore.Mvc;

namespace App_View.IServices
{
    public interface IProductDetailService
    {
        Task<List<ProductViewModel>?> GetListProductViewModelAsync();
        Task<HttpResponseMessage> CreatProductDetailAsync(ProductDetailDTO productDetailDTO);
        Task UpdateProductDetailAsync(ProductDetailDTO productDetailDTO);
        Task UpdateProductAsync(ProductUpdateDTO productUpdateDTO);
        Task DeleteListProductDetailAsync(List<Guid> lstIdProductDetailRemove);
        Task DeleteProductDetail(Guid id);
        Task<ProductDetailDTO?> GetProductDTOByIdAsync(Guid id);
        Task<HttpResponseMessage> GetProductDetailForUpdateOrAdd(ProductDetailDTO productDetailDTO);
        Task<List<ProductItemShopVM>> GetProductItemShopVMsAsync();
        Task<ProductViewModel> GetProductVMsAsync(Guid id);
        Task<ProductDetailVM> GetDetailProductAsync(Guid id);
        Task<ProductDetailResponseVM?> GetProductDetailRespoAsync(DataProductDetailVm dataProductDetailVm);
        Task<Product?> CreateProductAsynsc(string nameSanPham);
        Task<List<ProductViewModel>> GetLstProductDetailViewModelNgungKinhDoanhAynsc();
    }
}
