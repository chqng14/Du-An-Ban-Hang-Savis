using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Data.Models
{
    public class Images
    {
        public Guid Id { get; set; }
        public Guid IdProductDetail { get; set; }
        public string TenAnh { get; set; }
        public string DuongDan { get; set; }
        public int TrangThai { get; set; }

        public virtual ProductDetails ProductDetails { get; set; }
    }
}
