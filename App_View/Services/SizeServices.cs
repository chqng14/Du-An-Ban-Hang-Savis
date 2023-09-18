using App_Data.Models;
using App_View.IServices;
using Newtonsoft.Json;

namespace App_View.Services
{
    public class SizeServices : ISizeServices
    {
        HttpClient httpClient;
        public SizeServices()
        {
            httpClient = new HttpClient();
        }
        public async Task<bool> AddSize(Size size)
        {
            string url = $"https://localhost:7165/api/Size/createSize?tenSize={size.Size1}&CM={size.Cm}";
            await httpClient.PostAsJsonAsync(url, size);
            return true;
        }

        public async Task<bool> DeleteSize(Guid id)
        {
            string apiUrl = $"https://localhost:7165/api/Size/DeleteSize?id={id}";
            var response = await httpClient.DeleteAsync(apiUrl);
            return true;
        }

        public async Task<bool> EditSize(Size size)
        {
            string url = $"https://localhost:7165/api/Size/EditSize?id={size.Id}&ten={size.Size1}&CM={size.Cm}&trangthai={size.TrangThai}";
            await httpClient.PutAsJsonAsync(url, size);
            return true;
        }

        public async Task<List<Size>> GetAllSize()
        {
            var httpClient = new HttpClient();
            string apiUrl = "https://localhost:7165/api/Size/GetAllSize";
            var response = await httpClient.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var sizes = JsonConvert.DeserializeObject<List<Size>>(apiData);
            return sizes;
        }
    }
}
