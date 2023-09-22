using App_View.IServices;
using App_View.Views.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace App_View.Models
{
    public class SideColorAndSize:ViewComponent
    {
        private readonly IProductDetailService _productDetailService;

        public SideColorAndSize(IProductDetailService productDetailService)
        {
            _productDetailService = productDetailService;
        }

        public IViewComponentResult Invoke()
        {
            var listProduct = (_productDetailService.GetListProductViewModelAsync().Result!).Where(x => x.TrangThai == 0).ToList();

            var result = new SideColorAndSizeViewModel
            {
                ListColorVM = listProduct.GroupBy(x => x.MauSac).Select(color => new SideColorViewModel
                {
                    Name = color.Key,
                    Count = color.Count()
                }).ToList(),

                ListSizeVM = listProduct.GroupBy(x => x.Size).Select(size => new SideSizeViewModel
                {
                    Name = size.Key,
                    Count = size.Count()
                }).ToList()
            };

            return View(result);
        }
    }
}
