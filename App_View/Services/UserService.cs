using App_Data.Models;
using App_View.IServices;
using Newtonsoft.Json;

namespace App_View.Services
{
    public class UserService : IUserService
    {
        
        public async Task<bool> CreateUserAsync(User user)
        {
            try
            {
                var httpClient = new HttpClient();
                string apiURL = $"https://localhost:7165/api/User?IdRole={user.IdRole}&Ten={user.Ten}&GioiTinh={user.GioiTinh}&NgaySinh={user.NgaySinh}&DiaChi={user.DiaChi}&SDT={user.Sdt}&MatKhau={user.MatKhau}&Email={user.Email}&TaiKhoan={user.TaiKhoan}";
                var response = await httpClient.PostAsync(apiURL, null);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
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

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            try
            {
                var httpClient = new HttpClient();
                string apiURL = $"https://localhost:7165/api/User/{id}";
                var response = await httpClient.DeleteAsync(apiURL);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
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

        public async Task<bool> EditUserAsync(Guid id, User user)
        {
            try
            {
                var httpClient = new HttpClient();
                string apiURL = $"https://localhost:7165/api/User/{id}?IdRole={user.IdRole}&Ten={user.Ten}&GioiTinh={user.GioiTinh}&NgaySinh={user.NgaySinh}&DiaChi={user.DiaChi}&SDT={user.Sdt}&MatKhau={user.MatKhau}&Email={user.Email}&TrangThai={user.TrangThai}" ;
                var response = await httpClient.PutAsync(apiURL,null);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
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

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            var httpClient = new HttpClient();
            return await httpClient.GetFromJsonAsync<User>($"https://localhost:7165/api/User/{id}");
        }

        public async Task<List<User>> GetUsersAsync()
        {
            try
            {
                var httpClient = new HttpClient();
                string apiURL = "https://localhost:7165/api/User";
                var response = await httpClient.GetAsync(apiURL);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var users = JsonConvert.DeserializeObject<List<User>>(content);
                    return users;
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
