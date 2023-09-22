using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Data.ViewModels.ProductDetail
{
    public class ProductDetailVM:ProductViewModel
    {
        public List<string>? ListMauSac { get; set; }
        public List<string>? ListSize { get; set; }
    }
}
