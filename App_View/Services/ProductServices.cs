using App_Data.Models;
using App_View.IServices;
using Newtonsoft.Json;

namespace App_View.Services
{
    public class ProductServices : IProductServices
    {
        HttpClient httpClient;
        public ProductServices()
        {
            httpClient = new HttpClient();
        }
        public async Task<bool> AddProduct(Product product)
        {
            string url = $"https://localhost:7165/api/Product/CreateProduct?ma={product.Ma}&ten={product.Ten}&trangthai={product.TrangThai}";
            await httpClient.PostAsJsonAsync(url, product);
            return true;
        }

        public async Task<bool> DeleteProduct(Guid id)
        {
            string apiUrl = $"https://localhost:7165/api/Product/DeleteProduct?id={id}";
            var response = await httpClient.DeleteAsync(apiUrl);
            return true;
        }

        public async Task<bool> EditProduct(Product product)
        {
            string url = $"https://localhost:7165/api/Product/UpdateProduct?id={product.Id}&ma={product.Ma}&ten={product.Ten}&trangthai={product.TrangThai}";
            await httpClient.PutAsJsonAsync(url, product);
            return true;
        }

        public async Task<List<Product>> GetAllProduct()
        {
            var httpClient = new HttpClient();
            string apiUrl = "https://localhost:7165/api/Product/GetAllProduct";
            var response = await httpClient.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<List<Product>>(apiData);
            return products;
        }

        public Task<Product> GetProductById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
