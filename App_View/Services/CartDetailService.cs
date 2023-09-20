using App_Data.Models;
using App_View.IServices;
using Newtonsoft.Json;

namespace App_View.Services
{
    public class CartDetailService : ICartDetailService
    {
      public async  Task<bool> CreateCartDetailAsync(CartDetails cartDetails)
        {
            try
            {
                var httpClient = new HttpClient(); 
                string apiURL = $"https://localhost:7165/api/CartDetail?IDUSer={cartDetails.IDUser}&IDCTSP={cartDetails.IDCTSP}&SoLuong={cartDetails.SoLuong}&TrangThai={cartDetails.TrangThai}";
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

        public async Task<bool> DeleteCartDetailAsync(Guid id)
        {
            try
            {
                var httpClient = new HttpClient();
                string apiURL = $"https://localhost:7165/api/CartDetail/{id}";
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

        public async Task<bool> EditCartDetailAsync(Guid id, CartDetails cartDetails)
        {
            try
            {
                var httpClient = new HttpClient();
                string apiURL = $"https://localhost:7165/api/CartDetail/{id}?SoLuong={cartDetails.SoLuong}&GiaKhuyenMai={cartDetails.GiaKhuyenMai}&TrangThai={cartDetails.TrangThai}";
                var response = await httpClient.PutAsync(apiURL, null);
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

        public async Task<CartDetails> GetCartDetailByIdAsync(Guid id)
        {
            var httpClient = new HttpClient();
            return await httpClient.GetFromJsonAsync<CartDetails>($"https://localhost:7165/api/CartDetail/{id}");
        }

        public async Task<List<CartDetails>> GetCartDetailsAsync()
        {
            try
            {
                var httpClient = new HttpClient();
                string apiURL = "https://localhost:7165/api/CartDetail";
                var response = await httpClient.GetAsync(apiURL);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var cartDetails = JsonConvert.DeserializeObject<List<CartDetails>>(content);
                    return cartDetails;
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
