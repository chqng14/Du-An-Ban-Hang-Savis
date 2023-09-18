using App_Data.Models;
using App_View.IServices;
using Newtonsoft.Json;
using System.Drawing;

namespace App_View.Services
{
    public class MaterialServices : IMaterialServices
    {
        HttpClient httpClient;
        public MaterialServices()
        {
            httpClient = new HttpClient();
        }
        public async Task<bool> AddMaterial(Material material)
        {
            string url = $"https://localhost:7165/api/Material/createMaterial?ten={material.Ten}";
            await httpClient.PostAsJsonAsync(url, material);
            return true;
        }

        public async Task<bool> Edit(Material material)
        {
            string url = $"https://localhost:7165/api/Material/EditMaterial?id={material.Id}&ten={material.Ten}&trangthai={material.TrangThai}";
            await httpClient.PutAsJsonAsync(url, material);
            return true;

        }

        public async Task<List<Material>> GetAllMateroal()
        {
            var httpClient = new HttpClient();
            string apiUrl = "https://localhost:7165/api/Material/GetAllMaterial";
            var response = await httpClient.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var materials = JsonConvert.DeserializeObject<List<Material>>(apiData);
            return materials;
        }

        public async Task<bool> RemoveMaterial(Guid id)
        {
            string apiUrl = $"https://localhost:7165/api/Material/DeleteMaterial?id={id}";
            var response = await httpClient.DeleteAsync(apiUrl);
            return true;
        }
    }
}
