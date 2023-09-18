using App_Data.Models;
using App_View.IServices;
using Newtonsoft.Json;

namespace App_View.Services
{
    public class SaleService : ISaleService
    {
        public async Task<bool> CreateSale(Sale p)
        {
            try
            {
                string apiUrl = $"https://localhost:7165/api/Sale?ma={p.Ma}&ten={p.Ten}&ngaybatdau={p.NgayBatDau}&ngayketthuc={p.NgayKetThuc}&LoaiHinhKm={p.LoaiHinhKm}&mota={p.MoTa}&mucgiam={p.MucGiam}&trangthai={p.TrangThai}";
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

        public async Task<bool> DeleteSale(Guid id)
        {
            try
            {
                string apiUrl = $"https://localhost:7165/api/Sale/{id}";
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

        public async Task<bool> EditSale(Sale p)
        {
            try
            {
                string apiUrl = $"https://localhost:7165/api/Sale/{p.Id}?ma={p.Ma}&ten={p.Ten}&ngaybatdau={p.NgayBatDau}&ngayketthuc={p.NgayKetThuc}&LoaiHinhKm={p.LoaiHinhKm}&mota={p.MoTa}&mucgiam={p.MucGiam}&trangthai={p.TrangThai}";
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

        public async Task<List<Sale>> GetAllSale()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetFromJsonAsync<List<Sale>>("https://localhost:7165/api/Sale");
            return response;
        }
    }
}
