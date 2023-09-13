using System;
using System.Collections.Generic;

namespace App_Data.Models
{
    public partial class Voucher
    {
        public Guid Id { get; set; }
        public string? Ma { get; set; }
        public string? LoaiHinhKm { get; set; }
        public decimal MucUuDai { get; set; }
        public string? PhamVi { get; set; }
        public string? DieuKien { get; set; }
        public int? SoLuongTon { get; set; }
        public int? SoLanSuDung { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public int? TrangThai { get; set; }
       
        //public virtual List<Bill> Bills { get; set; }
    }
}
