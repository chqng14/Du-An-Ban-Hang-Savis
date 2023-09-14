using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Data.Models
{
    public class BillDetails
    {
        public Guid Id { get; set; }
        public Guid? IdBill { get; set; }
        public Guid? IdProductDetail { get; set; }
        public int? SoLuong { get; set; }
        public decimal? DonGia { get; set; }
        public int? TrangThai { get; set; }
        public virtual Bill Bill { get; set; }
        public virtual ProductDetails ProductDetail { get; set; }
    }
}
