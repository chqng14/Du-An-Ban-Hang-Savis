using App_Data.Models;
using App_View.IServices;
using Newtonsoft.Json;
using System.Drawing;

namespace App_View.Services
{
    public class TypeProductServices : ITypeProductServices
    {
        HttpClient httpClient;
        public TypeProductServices()
        {
            httpClient = new HttpClient();
        }
        public async Task<bool> AddTypeProduct(TypeProduct typeProduct)
        {
            string url = $"https://localhost:7165/api/TypeProduct/CreateTypeProduct?ten={typeProduct.Ten}&trangthai={typeProduct.TrangThai}";
            await httpClient.PostAsJsonAsync(url, typeProduct);
            return true;
        }

        public async Task<bool> DeleteTypeProduct(Guid id)
        {
            string apiUrl = $"https://localhost:7165/api/TypeProduct/DeleteTypeProduct?id={id}";
            var response = await httpClient.DeleteAsync(apiUrl);
            return true;
        }

        public Task<bool> EditTypeProduct(TypeProduct typeProduct)
        {
            string url = $"https://localhost:7165/api/TypeProduct/UpdateTypeProduct?id={typeProduct.Id}&ten={typeProduct.Ten}&ma={typeProduct.Ma}&trangthai={typeProduct.TrangThai}";
            await httpClient.PutAsJsonAsync(url, typeProduct);
            return true;
        }

        public Task<List<TypeProduct>> GetAllTypeProduct()
        {
            var httpClient = new HttpClient();
            string apiUrl = "https://localhost:7165/TypeProduct/Size/GetAllSize";
            var response = await httpClient.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var typeProducts = JsonConvert.DeserializeObject<List<TypeProduct>>(apiData);
            return typeProducts;
        }
    }
}
