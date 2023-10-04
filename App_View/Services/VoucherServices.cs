using App_Data.Models;
using App_View.IServices;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;

namespace App_View.Services
{
    public class VoucherServices : IVoucherServices
    {
        public async Task<bool> AddVoucherAsync(Voucher item)
        {
            try
            {
                string apiUrl = $"https://localhost:7165/api/Voucher/AddVoucher?ten={item.Ten}&loaihinhkm={item.LoaiHinhKm}&mucuudai={item.MucUuDai}&phamvi={item.PhamVi}&dieukien={item.DieuKien}&soluongton={item.SoLuongTon}&ngaybatdau={item.NgayBatDau}&ngayketthuc={item.NgayKetThuc}";
                var httpclient = new HttpClient();
                var response = await httpclient.PostAsync(apiUrl, null);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine($"Yêu cầu API AddVoucher thất bại với mã trạng thái: {response.StatusCode}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi xảy ra: {ex}");
                throw;
            }
        }

        public async Task<bool> EditVoucher(Voucher item)
        {
            try
            {
                string apiUrl = $"https://localhost:7165/api/Voucher/{item.Id}?ten={item.Ten}&ma={item.Ma}&loaihinhkm={item.LoaiHinhKm}&mucuudai={item.MucUuDai}&phamvi={item.PhamVi}&dieukien={item.DieuKien}&soluongton={item.SoLuongTon}&solansudung={item.SoLanSuDung}&ngaybatdau={item.NgayBatDau}&ngayketthuc={item.NgayKetThuc}&trangthai={item.TrangThai}";
                var httpclient = new HttpClient();
                var response = await httpclient.PutAsync(apiUrl, null);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine($"Yêu cầu API sửa Voucher thất bại với mã trạng thái: {response.StatusCode}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi xảy ra: {ex}");
                throw;
            }
        }

        public async Task<List<Voucher>> GetAllAsync()
        {
            try
            {
                string apiUrl = "https://localhost:7165/api/Voucher/GetVoucher";
                var httpclient = new HttpClient();
                var response = await httpclient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string apidata = await response.Content.ReadAsStringAsync();
                    List<Voucher> voucherslst = JsonConvert.DeserializeObject<List<Voucher>>(apidata);
                    return voucherslst;
                }
                else
                {
                    Console.WriteLine($"Yêu cầu API GetAllVoucher thất bại với mã trạng thái: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi xảy ra: {ex}");
                return null;
            }

        }

        public async Task<Voucher> GetVoucherAsync(string item)
        {
            try
            {
                string apiUrl = $"https://localhost:7165/api/Voucher/GetVoucherByMa?ma={item}";
                var httpclient = new HttpClient();
                var response = await httpclient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string apidata = await response.Content.ReadAsStringAsync();
                    Voucher voucher = JsonConvert.DeserializeObject<Voucher>(apidata);
                    return voucher;
                }
                else
                {
                    Console.WriteLine($"Yêu cầu API GetVoucher thất bại với mã trạng thái: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi xảy ra: {ex}");
                return null;
            }

        }

        public async Task<bool> RemoveVoucher(Voucher item)
        {
            try
            {
                var httpClient = new HttpClient();
                string apiUrl = $"https://localhost:7165/api/Voucher/{item.Id}";
                var response = await httpClient.DeleteAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine($"Yêu cầu API Voucher xóa thất bại với mã trạng thái: {response.StatusCode}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi xảy ra: {ex}");
                return false;
            }

        }
    }
}
