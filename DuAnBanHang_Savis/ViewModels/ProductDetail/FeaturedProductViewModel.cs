using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Data.ViewModels.ProductDetail
{
    public class FeaturedProductViewModel
    {
        public List<ProductViewModel>? ListProductNew { get; set; }
        public List<ProductViewModel>? ListProductBestSellers { get; set; }
        public List<ProductViewModel>? ListProductBestFeatured { get; set; }
    }
}
