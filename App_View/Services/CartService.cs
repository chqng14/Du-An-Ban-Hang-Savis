using App_Data.Models;
using App_View.IServices;
using Newtonsoft.Json;

namespace App_View.Services
{
    public class CartService : ICartService
    {
        
        public async Task<bool> CreateCartAsync(Cart cart)
        {
            try
            {
                var httpClient = new HttpClient();
                string apiURL = $"https://localhost:7165/api/Cart?IdUser={cart.IdUser}&TrangThai={cart.Trangthai}";
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

        //public async Task<bool> DeleteCartAsync(Guid IdUser)
        //{
        //    try
        //    {
        //        var response = await httpClient.DeleteAsync($"https://localhost:7165/api/Cart/{id}");
        //        if (response.IsSuccessStatusCode) return true;
        //        else
        //        {
        //            Console.WriteLine($"Yêu cầu API DELETE thất bại với mã trạng thái: {response.StatusCode}");
        //            return false;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine($"Có ngoại lệ xảy ra: {e}");
        //        return false;
        //    }
        //}

        public async Task<Cart> GetCartByIdAsync(Guid IdUser)
        {
            var httpClient = new HttpClient();
            return await httpClient.GetFromJsonAsync<Cart>($"https://localhost:7165/api/Cart/{IdUser}");
        }

        public async Task<List<Cart>> GetCartsAsync()
        {
            try
            {
                var httpClient = new HttpClient();
                string apiURL = "https://localhost:7165/api/Cart";
                var response = await httpClient.GetAsync(apiURL);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var carts = JsonConvert.DeserializeObject<List<Cart>>(content);
                    return carts;
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

        public async Task<bool> UpdateCartAsync(Guid IdUser, Cart cart)
        {
            try
            {
                var httpClient = new HttpClient();
                string apiURL = $"https://localhost:7165/api/Cart/{IdUser}?TrangThai={cart.Trangthai}";
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
    }
}
