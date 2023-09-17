using App_Data.Models;
using App_Data.Repositories;
using App_View.IServices;
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
            string url = "https://localhost:7165/api/Color/createColor";
            await httpClient.PostAsJsonAsync(url,color);
            return true;
        }

        public async Task<bool> DeleteColor(Guid id)
        {
            var httpClient = new HttpClient();
            string apiUrl = $"https://localhost:7165/api/Color/DeleteColor?id={id}";
            var response = await httpClient.DeleteAsync(apiUrl);
            return true;
        }

        public Task<bool> EditColor(Color color)
        {
            throw new NotImplementedException();
        }

        public Task<List<Color>> GetAllColor()
        {
            throw new NotImplementedException();
        }
    }
}
