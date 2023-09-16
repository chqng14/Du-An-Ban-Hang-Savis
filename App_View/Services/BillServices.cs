using App_Data.Models;
using App_View.IServices;
using Newtonsoft.Json;

namespace App_View.Services
{
    public class BillServices : IBillServices
    {
        public async Task<bool> CreateBillAsync(Bill obj)
        {
            try
            {
                var httpClient = new HttpClient();
                string apiUrl = $"https://localhost:7165/api/Bill?id={obj.Id}&idUser={obj.IdUser}&idVoucher={obj.IdVoucher}&ngayTao={obj.NgayTao}&ngayThanhToan={obj.NgayThanhToan}&ngayShip={obj.NgayShip}&ngayNhan={obj.NgayNhan}" +
                    $"&tenNguoiNhan={obj.TenNguoiNhan}&diaChi={obj.DiaChi}&sdt={obj.Sdt}&tongTien={obj.TongTien}&soTienGiam={obj.SoTienGiam}&tienShip={obj.TienShip}&moTa={obj.MoTa}&trangThai={obj.TrangThai}";

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

        public async Task<bool> DeleteBillAsync(Guid id)
        {
            try
            {
                var httpClient = new HttpClient();
                string apiUrl = $"https://localhost:7165/api/Bill/{id}";
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
                // Ghi nhật ký ngoại lệ để giúp gỡ lỗi
                Console.WriteLine($"Có ngoại lệ xảy ra: {ex}");
                return false;
            }

        }

        public async Task<List<Bill>> GetAllBillsAsync()
        {
            try
            {
                string apiUrl = "https://localhost:7165/api/Bill";
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string apiData = await response.Content.ReadAsStringAsync();
                    var bills = JsonConvert.DeserializeObject<List<Bill>>(apiData);
                    return bills;
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

        public Bill GetBillById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Bill>> GetBillsByDateRangeAsync(DateTime startDate, DateTime endDate, string ma)
        {

            var bills = (await GetAllBillsAsync())
                .Where(b => b.NgayTao >= startDate && b.NgayTao <= endDate && b.Ma.Contains(ma))
                .ToList();

            return bills;
        }

        public async Task<List<Bill>> GetBillsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var bills = (await GetAllBillsAsync())
                 .Where(b => b.NgayTao >= startDate && b.NgayTao <= endDate)
                 .ToList();

            return bills;
        }

        public List<Bill> GetBillsByMa(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateBillAsync(Bill obj)
        {
            try
            {
                var httpClient = new HttpClient();
                string apiUrl = $"https://localhost:7165/api/Bill/{obj.Id}?idUser={obj.IdUser}&idVoucher={obj.IdVoucher}&ma={obj.Ma}&ngayTao={obj.NgayTao}&ngayThanhToan={obj.NgayThanhToan}&ngayShip={obj.NgayShip}&ngayNhan={obj.NgayNhan}" +
                        $"&tenNguoiNhan={obj.TenNguoiNhan}&diaChi={obj.DiaChi}&sdt={obj.Sdt}&tongTien={obj.TongTien}&soTienGiam={obj.SoTienGiam}&tienShip={obj.TienShip}&moTa={obj.MoTa}&trangThai={obj.TrangThai}";
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
    }
}
