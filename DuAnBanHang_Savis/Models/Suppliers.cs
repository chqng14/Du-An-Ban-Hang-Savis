using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Data.Models
{
    public class Suppliers
    {
        public Guid Id { get; set; }
        [MaxLength(250)]
        public string Ten { get; set; }
        public string TenLienLac { get; set; }

        public virtual IEnumerable<ProductDetails> ProductDetails { get; set; }
    }
}
