using App_Data.Models;

namespace App_View.IServices
{
    public interface IProductServices
    {
        public Task<List<Product>> GetAllProduct();
        public Task<bool> EditProduct(Product product);
        public Task<bool> DeleteProduct(Guid id);
        public Task<bool> AddProduct(Product product);
        public Task<Product> GetProductById(Guid id);
    }
}
