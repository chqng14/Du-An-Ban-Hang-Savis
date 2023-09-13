using System;
using System.Collections.Generic;

namespace App_Data.Models
{
    public partial class Sale
    {
        public Guid Id { get; set; }
        public string? Ma { get; set; }
        public string? Ten { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string? LoaiHinhKm { get; set; }
        public decimal? MucGiam { get; set; }
        public string? MoTa { get; set; }
        public int? TrangThai { get; set; }
        public virtual ICollection<SaleDetail> DetailSales { get; set; }
    }
}
