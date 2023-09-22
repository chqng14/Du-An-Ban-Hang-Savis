using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace App_Data.Models
{
    public class ProductDetails
    {
        public Guid Id { get; set; }
        public Guid? IdProduct { get; set; }
        public Guid? IdColor { get; set; }
        public Guid? IdSize { get; set; }
        public Guid? IdTypeProduct { get; set; }
        public Guid? IdMaterial { get; set; }
        public string? BaoHanh { get; set; }
        public string? MoTa { get; set; }
        public int SoLuongTon { get; set; }
        public int SoLuongDaBan { get; set; }
        public DateTime NgayTao { get; set; }
        public bool IsNoiBat { get; set; }  
        public decimal? GiaNhap { get; set; }
        public decimal? GiaBan { get; set; }
        public int? TrangThai { get; set; }

        public virtual Product Products { get; set; }

        public virtual Color Color { get; set; }

        public virtual Size Size { get; set; }

        public virtual TypeProduct TypeProduct { get; set; }

        public virtual Material Material { get; set; }

        public virtual IEnumerable<BillDetails> BillDetail { get; set; }

        public virtual IEnumerable<SaleDetail> DetailSale { get; set; }

        public virtual IEnumerable<CartDetails> CartDetail { get; set; }
        public virtual IEnumerable<Images> Image { get; set; }
    }
}
