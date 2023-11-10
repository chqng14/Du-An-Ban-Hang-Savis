using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Data.ViewModels.ProductDetail
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string? NameProduct { get; set; }
        public string? MauSac { get; set; }
        public string? Size { get; set; }
        public string? Loai { get; set; }
        public string? ChatLieu { get; set; }
        public string? BaoHanh { get; set; }
        public string? MoTa { get; set; }
        public int SoLuongTon { get; set; }
        public bool IsNoiBat { get; set; }
        public int? SoLuongDaBan { get; set; }
        public bool IsNew { get; set; }
        public decimal? GiaNhap { get; set; }
        public decimal? GiaBan { get; set; }
        public decimal? GiaThucTe { get; set; }
        public int? TrangThai { get; set; }
        public List<string>? LstTenAnh { get; set; }

    }
}
