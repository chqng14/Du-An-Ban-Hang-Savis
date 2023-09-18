using App_Data.Models;
using App_Data.Repositories;
using App_View.IServices;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;

namespace App_View.Services
{
    public class ColorServices : IColorServices
    {
        HttpClient httpClient;
        public ColorServices()
        {
            httpClient = new HttpClient();
        }
        public async Task<bool> AddColor(Color color)
        {
            string url = $"https://localhost:7165/api/Color/createColor?ten={color.Ten}";
            await httpClient.PostAsJsonAsync(url,color);
            return true;
        }

        public async Task<bool> DeleteColor(Guid id)
        {
            string apiUrl = $"https://localhost:7165/api/Color/DeleteColor?id={id}";
            var response = await httpClient.DeleteAsync(apiUrl);
            return true;
        }

        public async Task<bool> EditColor(Color color)
        {
            string url = $"https://localhost:7165/api/Color/EditColor?id={color.Id}&ma={color.Ma}&ten={color.Ten}&trangthai={color.TrangThai}";
            await httpClient.PutAsJsonAsync(url, color);
            return true;
        }

        public async Task<List<Color>> GetAllColor()
        {
            var httpClient = new HttpClient();
            string apiUrl = "https://localhost:7165/api/Color/GetAllColor";
            var response = await httpClient.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var Colors = JsonConvert.DeserializeObject<List<Color>>(apiData);
            return Colors;
        }
    }
}
