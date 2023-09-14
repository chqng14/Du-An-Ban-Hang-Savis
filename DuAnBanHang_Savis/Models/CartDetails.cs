using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Data.Models
{
    public class CartDetails
    {
        public Guid ID { get; set; }
        public Guid IDUser { get; set; }
        public Guid IDCTSP { get; set; }
        public int SoLuong { get; set; }
        public decimal? GiaKhuyenMai { get; set; }
        public int TrangThai { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual ProductDetails ProductDetail { get; set; }
    }
}
