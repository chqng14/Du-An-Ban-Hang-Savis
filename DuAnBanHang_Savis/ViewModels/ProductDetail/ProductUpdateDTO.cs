using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Data.ViewModels.ProductDetail
{
    public class ProductUpdateDTO
    {
        public Guid Id { get; set; }
        public string? MoTa { get; set; }
        public int SoLuongTon { get; set; }
        public bool IsNoiBat { get; set; }
        public int? SoLuongDaBan { get; set; }
        public bool IsNew { get; set; }
        public decimal? GiaNhap { get; set; }
        public decimal? GiaBan { get; set; }
        public int? TrangThai { get; set; }
    }
}
