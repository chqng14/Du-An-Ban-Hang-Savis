using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Data.Models
{
    public class Blog
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập")]
        public string? Ma { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập")]
        public string? TenBlog { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập")]
        public string? NoiDung { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập")]
        public string? MoTa { get; set; }
    }
}
