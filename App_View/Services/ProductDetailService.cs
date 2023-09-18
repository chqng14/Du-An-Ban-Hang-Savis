using App_Data.ViewModels.ProductDetail;
using App_View.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace App_View.Services
{
    public class ProductDetailService : IProductDetailService
    {
        private readonly HttpClient _httpClient;

        public ProductDetailService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> CreatProductDetailAsync(ProductDetailDTO productDetailDTO)
        {
            var respose = await _httpClient.PutAsJsonAsync("/api/ProductDetail/create-productdetail", productDetailDTO);
            return await respose.Content.ReadAsAsync<OkObjectResult>();
        }

        public async Task DeleteListProductDetailAsync(List<Guid> lstIdProductDetailRemove)
        {
            var deleteUrl = "/api/ProductDetail/delete-lst-product";
            var content = new StringContent(JsonConvert.SerializeObject(lstIdProductDetailRemove), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Delete, deleteUrl);
            request.Content = content;
            await _httpClient.SendAsync(request);
        }

        public async Task<ProductDetailDTO?> GetProductDTOByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<ProductDetailDTO>($"/api/ProductDetail/get-productdetail/{id}");
        }

        public async Task<List<ProductViewModel>?> GetListProductViewModelAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ProductViewModel>>("/api/ProductDetail/get-list-productdetail");
        }

        public async Task UpdateProdutDetailAsync(ProductDetailDTO productDetailDTO)
        {
            await _httpClient.PutAsJsonAsync("/api/ProductDetail/update", productDetailDTO);
        }

        public async Task DeleteProductDetail(Guid id)
        {
            await _httpClient.DeleteAsync($"/api/ProductDetail/delete-product-detail/{id}");
        }

        public async Task<IActionResult> GetProductDetailForUpdateOrAdd(ProductDetailDTO productDetailDTO)
        {
            return (await _httpClient.GetFromJsonAsync<OkObjectResult>("/api/ProductDetail/get-add-or-update"))!;
        }
    }
}
