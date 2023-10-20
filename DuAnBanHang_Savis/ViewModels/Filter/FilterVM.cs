using App_Data.ViewModels.ProductDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Data.ViewModels.Filter
{
    public class FilterVM
    {
        public List<ProductItemShopVM>? Items { get; set; } = new List<ProductItemShopVM>();
        public PagingInfor? PagingInfor { get; set; } = new PagingInfor();
    }
}
