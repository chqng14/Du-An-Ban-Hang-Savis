using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Data.Models
{
    public class Cart
    {
        [Key]

        public Guid IdUser { get; set; }
        public int Trangthai { get; set; }
        [ForeignKey("IdUser")]
        public virtual User Users { get; set; }
        public virtual IEnumerable<CartDetails> CartDetail { get; set; }
    }
}
