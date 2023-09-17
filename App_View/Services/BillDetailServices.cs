using App_Data.Models;
using App_Data.ViewModel;
using App_View.IServices;
using Newtonsoft.Json;

namespace App_View.Services
{
    public class BillDetailServices : IBillDetailsServices
    {
        public async Task<bool> AddItemAsync(BillDetails item)
        {
            try
            {
                string apiUrl = $"https://localhost:7165/api/BillDetails?idBill={item.IdBill}&idProduct={item.IdProductDetail}&sl={item.SoLuong}&dongia={item.DonGia}&trangthai={item.TrangThai}";
                HttpClient httpClient = new HttpClient();
                var response = await httpClient.PostAsync(apiUrl, null);

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
            catch (Exception ex)
            {
                Console.WriteLine($"Có ngoại lệ xảy ra: {ex}");
                return false;
            }

        }

        public async Task<bool> EditItem(BillDetails item)
        {
            try
            {
        
                string apiUrl = $"https://localhost:7165/api/BillDetails/EditBillDetails{item.Id}?idBill={item.IdBill}&idProduct={item.IdProductDetail}&dongia={item.DonGia}&sl={item.SoLuong}&trangthai={item.TrangThai}";
                HttpClient httpClient = new HttpClient();

                var response = await httpClient.PutAsync(apiUrl, null);

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
            catch (Exception ex)
            {
                Console.WriteLine($"Có ngoại lệ xảy ra: {ex}");
                return false;
            }

        }

        public async Task<List<BillDetails>> GetAllAsync()
        {
            try
            {
                string apiUrl = "https://localhost:7165/api/BillDetails";
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var apidata = await response.Content.ReadAsStringAsync();
                    List<BillDetails> lstBill = JsonConvert.DeserializeObject<List<BillDetails>>(apidata);
                    return lstBill;
                }
                else
                {
                    Console.WriteLine($"Yêu cầu API GET thất bại với mã trạng thái: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Có ngoại lệ xảy ra: {ex}");
                return null;
            }

        }

        public async Task<List<BillDetailView>> GetByBill(Guid id)
        {
            try
            {
                string apiUrl = $"https://localhost:7165/api/BillDetails?idBill={id}";
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var apidata = await response.Content.ReadAsStringAsync();
                    List<BillDetailView> lstBill = JsonConvert.DeserializeObject<List<BillDetailView>>(apidata);
                    return lstBill;
                }
                else
                {
                    Console.WriteLine($"Yêu cầu API GET thất bại với mã trạng thái: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Có ngoại lệ xảy ra: {ex}");
                return null;
            }

        }

        public async Task<List<BillDetails>> GetById(Guid id)
        {
            try
            {
                string apiUrl = $"https://localhost:7165/api/BillDetails/{id}";
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var apidata = await response.Content.ReadAsStringAsync();
                    List<BillDetails> lstBill = JsonConvert.DeserializeObject<List<BillDetails>>(apidata);
                    return lstBill;
                }
                else
                {
                    Console.WriteLine($"Yêu cầu API GET thất bại với mã trạng thái: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Có ngoại lệ xảy ra: {ex}");
                return null;
            }

        }

        public async Task<bool> RemoveItem(BillDetails item)
        {
            try
            {
                string apiUrl = $"https://localhost:7165/api/BillDetails/{item.Id}";
                HttpClient httpClient = new HttpClient();

                // Gửi yêu cầu DELETE
                var response = await httpClient.DeleteAsync(apiUrl);

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
            catch (Exception ex)
            {
                Console.WriteLine($"Có ngoại lệ xảy ra: {ex}");
                return false; // hoặc trả về một giá trị mặc định khác, tùy thuộc vào tình huống
            }

        }
    }
}
