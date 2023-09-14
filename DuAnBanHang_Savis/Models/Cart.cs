using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public virtual User Users { get; set; }
    }
}
