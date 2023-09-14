using App_Data.IRepositories;
using App_Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace App_Api.Controllers
{
    public class ProductDetailController
    {
        private readonly IAllRepo<ProductDetails> _allRepoProduct;

        public ProductDetailController(IAllRepo<ProductDetails> allRepoProduct)
        {
            _allRepoProduct = allRepoProduct;
        }

        
    }
}
