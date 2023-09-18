using App_Data.Models;
using App_View.IServices;
using Newtonsoft.Json;

namespace App_View.Services
{
    public class RoleService : IRoleService
    {
       
        public async Task<bool> CreateRoleAsync(Role role)
        {
            try
            {
                var httpClient = new HttpClient();
                string apiURL = $"https://localhost:7165/api/Role?TenRole={role.Ten}";
                var response = await httpClient.PostAsync(apiURL, null);
                if (response.IsSuccessStatusCode) return true;
                else
                {
                    Console.WriteLine($"Yêu cầu API POST thất bại với mã trạng thái: {response.StatusCode}");
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Có ngoại lệ xảy ra: {e}");
                return false;
            }
        }

        public async Task<bool> DeleteRoleAsync(Guid id)
        {
            try
            {
                var httpClient = new HttpClient();
                var response = await httpClient.DeleteAsync($"https://localhost:7165/api/Role/{id}");
                if (response.IsSuccessStatusCode) return true;
                else
                {
                    Console.WriteLine($"Yêu cầu API DELETE thất bại với mã trạng thái: {response.StatusCode}");
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Có ngoại lệ xảy ra: {e}");
                return false;
            }
        }

        public async Task<bool> EditRoleAsync(Guid id, Role role)
        {
            try
            {
                var httpClient = new HttpClient();
                string apiURL = $"https://localhost:7165/api/Role/{id}?TenRole={role.Ten}&TrangThai={role.TrangThai}";
                var response = await httpClient.PutAsync(apiURL, null);
                if (response.IsSuccessStatusCode) return true;
                else
                {
                    Console.WriteLine($"Yêu cầu API PUT thất bại với mã trạng thái: {response.StatusCode}");
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Có ngoại lệ xảy ra: {e}");
                return false; 
            }
        }

        public async Task<Role> GetRoleByIdAsync(Guid id)
        {
            var httpClient = new HttpClient();
            string apiURL = $"https://localhost:7165/api/Role/{id}";
            return await httpClient.GetFromJsonAsync<Role>(apiURL);
        }

        public async Task<List<Role>> GetRolesAsync()
        {
            try
            {
                var httpClient = new HttpClient();
                string apiURL = "https://localhost:7165/api/Role";
                var response = await httpClient.GetAsync(apiURL);
                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var roles = JsonConvert.DeserializeObject<List<Role>>(content);
                    return roles;
                }
                else
                {
                    Console.WriteLine($"Yêu cầu API GET thất bại với mã trạng thái: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Có ngoại lệ xảy ra: {e}");
                return null;
            }
           
        }
    }
}
