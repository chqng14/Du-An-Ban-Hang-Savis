using App_Data.ViewModels.ProductDetail;
using App_View.IServices;
using Microsoft.AspNetCore.Mvc;

namespace App_View.Models
{
    public class FeaturedProducts : ViewComponent
    {
        private readonly IProductDetailService _productDetailSer;

        public FeaturedProducts(IProductDetailService productDetailSer)
        {
            _productDetailSer = productDetailSer;
        }

        public async Task<FeaturedProductViewModel> GetListView()
        {
            List<ProductViewModel> getAllProductViewModel = (await _productDetailSer.GetListProductViewModelAsync())!.Where(pr => pr.TrangThai==0).ToList();
            var getProductBestSeller = getAllProductViewModel.Where(it=>it.SoLuongDaBan>0).OrderByDescending(x => x.SoLuongDaBan).Take(10).ToList();
            return new FeaturedProductViewModel()
            {
                ListProductBestFeatured = getAllProductViewModel!.Where(pr => pr.IsNoiBat == true).ToList(),
                ListProductBestSellers = getProductBestSeller,
                ListProductNew = getAllProductViewModel!.Where(pr => pr.IsNew == true).ToList(),
            };
        }

        public IViewComponentResult Invoke()
        {
            return View(GetListView().Result);
        }
    }
}
