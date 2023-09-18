using App_Data.Models;
using App_View.IServices;

namespace App_View.Services
{
    public class SaleDetailService : ISaleDetailService
    {
        public async Task<bool> CreateDetaiSale(SaleDetail p)
        {
            try
            {
                string apiUrl = $"https://localhost:7165/api/SaleDetail?mota={p.MoTa}&trangthai={p.TrangThai}&IdSale={p.IdSale}&IdChiTietSp={p.IdChiTietSp}";
                var httpClient = new HttpClient();
                var response = await httpClient.PostAsync(apiUrl, null);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine(response.StatusCode);
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<bool> DeleteDetaiSale(Guid id)
        {
            try
            {
                string apiUrl = $"https://localhost:7165/api/SaleDetail/{id}";
                var httpClient = new HttpClient();
                var response = await httpClient.DeleteAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine(response.StatusCode);
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<bool> EditDetaiSale(SaleDetail p)
        {
            try
            {
                string apiUrl = $"https://localhost:7165/api/SaleDetail/{p.Id}?mota={p.MoTa}&trangthai={p.TrangThai}&IdSale={p.IdSale}&IdChiTietSp={p.IdChiTietSp}";
                var httpClient = new HttpClient();
                var response = await httpClient.PutAsync(apiUrl, null);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine(response.StatusCode);
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<List<SaleDetail>> GetAllDetaiSale()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetFromJsonAsync<List<SaleDetail>>("https://localhost:7165/api/SaleDetail");
            return response;
        }
    }
}
