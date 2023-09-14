using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Data.Models
{
    public class Size
    {
        public Guid Id { get; set; }
        public string Ma { get; set; }
        public string Size1 { get; set; }
        public decimal Cm { get; set; }
        public int TrangThai { get; set; }
        public virtual List<ProductDetails> ProductDetails { get; set; }
    }
}
