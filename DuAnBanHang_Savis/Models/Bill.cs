using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Data.Models
{
    public class Bill
    {
        
        public Guid Id { get; set; }
        public Guid? IdUser { get; set; }
        public Guid? IdVoucher { get; set; }
        public string? Ma { get; set; }
        public DateTime? NgayTao { get; set; }
        public DateTime? NgayThanhToan { get; set; }
        public DateTime? NgayShip { get; set; }
        public DateTime? NgayNhan { get; set; }
        public string? TenNguoiNhan { get; set; }
        public string? DiaChi { get; set; }
        public string? Sdt { get; set; }
        public decimal? TongTien { get; set; }
        public decimal? SoTienGiam { get; set; }
        public decimal? TienShip { get; set; }
        public string? MoTa { get; set; }
        public int? TrangThai { get; set; }
        public virtual User User { get; set; }
        public virtual Voucher Voucher { get; set; }
        public virtual List<BillDetails> BillDetails { get; set; }
    }
}
