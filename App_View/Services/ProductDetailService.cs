using App_Data.Models;
using App_Data.ViewModels.ProductDetail;
using App_View.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Controller;
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

        public async Task<HttpResponseMessage> CreatProductDetailAsync(ProductDetailDTO productDetailDTO)
        {
            return await _httpClient.PostAsJsonAsync("/api/ProductDetail/create-productdetail", productDetailDTO);
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

        public async Task UpdateProductDetailAsync(ProductDetailDTO productDetailDTO)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("/api/ProductDetail/update", productDetailDTO);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Delete successful. Response: " + responseContent);
                }
                else
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Delete failed. Response: " + responseContent);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
        }

        public async Task DeleteProductDetail(Guid id)
        {
            await _httpClient.DeleteAsync($"/api/ProductDetail/delete-product-detail/{id}");
        }

        public async Task<HttpResponseMessage> GetProductDetailForUpdateOrAdd(ProductDetailDTO productDetailDTO)
        {
            return await _httpClient.PostAsJsonAsync("/api/ProductDetail/get-add-or-update", productDetailDTO);

        }


        public async Task<List<ProductItemShopVM>> GetProductItemShopVMsAsync()
        {
            return (await _httpClient.GetFromJsonAsync<List<ProductItemShopVM>>("/api/ProductDetail/get-list-productItemShop"))!;
        }

        public async Task<ProductViewModel> GetProductVMsAsync(Guid id)
        {
            return (await _httpClient.GetFromJsonAsync<ProductViewModel>($"/api/ProductDetail/get-productViewModel/{id}"))!;
        }

        public async Task<ProductDetailVM> GetDetailProductAsync(Guid id)
        {
            return (await _httpClient.GetFromJsonAsync<ProductDetailVM>($"/api/ProductDetail/get-detail-product/{id}"))!;
        }

        public async Task<ProductDetailResponseVM?> GetProductDetailRespoAsync(DataProductDetailVm dataProductDetailVm)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"/api/ProductDetail/get-detail-Product-respo", dataProductDetailVm);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<ProductDetailResponseVM>();
                }
                else
                {
                    Console.WriteLine(await response.Content.ReadAsStringAsync());
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            
        }

        public Task<Product?> CreateProductAsynsc(string nameSanPham)
        {
            return _httpClient.GetFromJsonAsync<Product?>($"/api/ProductDetail/create-product/{nameSanPham}");
        }

        public async Task UpdateProductAsync(ProductUpdateDTO productUpdateDTO)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("/api/ProductDetail/update-productDTO", productUpdateDTO);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Delete successful. Response: " + responseContent);
                }
                else
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Delete failed. Response: " + responseContent);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
        }
    }
}
