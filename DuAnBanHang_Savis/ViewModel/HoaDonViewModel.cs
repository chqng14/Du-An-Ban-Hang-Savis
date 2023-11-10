using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Data.ViewModel
{
    public class HoaDonViewModel
    {
        public Guid? IdHoaDon { get; set; }
        public Guid? IdNguoiDung { get; set; }
        public Guid? IdKhachHang { get; set; }
        public Guid? IdVoucher { get; set; }
        public Guid? IdThongTinGH { get; set; }
        public string? MaHoaDon { get; set; }
        public string? MaVoucher { get; set; }
        public DateTime? NgayTao { get; set; }
        public DateTime? NgayThanhToan { get; set; }
        public DateTime? NgayShip { get; set; }
        public DateTime? NgayNhan { get; set; }
        public DateTime? NgayGiaoDuKien { get; set; }
        public decimal? TienShip { get; set; }
        public decimal? TienGiam { get; set; }
        public decimal? TongTien { get; set; }
        public string? MoTa { get; set; }
        public int? TrangThaiGiaoHang { get; set; }
        public int? TrangThaiThanhToan { get; set; }
        public string? LoaiThanhToan { get; set; }

        //public string? IdThongTinGH { get; set; }
        //public string? IdNguoiDung { get; set; }
        public string? TenNguoiNhan { get; set; }
        public string? SDT { get; set; }
        public string? DiaChi { get; set; }
        public int? TrangThai { get; set; }
    }
}
