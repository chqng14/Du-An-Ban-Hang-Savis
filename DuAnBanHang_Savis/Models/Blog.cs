using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Data.Models
{
    public class Blog
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập")]
        [Column(TypeName = "Varchar(50)")]
        public string? Ma { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập")]
        [Column(TypeName = "Nvarchar(250)")]
        public string? TieuDe { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập")]
        [Column(TypeName = "Nvarchar(max)")]
        public string? NoiDung { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập")]
        [Column(TypeName = "Nvarchar(250)")]
        public string? MoTaNgan { get; set; }
        public DateTime? NgayTao {  get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Vui lòng nhập")]
        [Column(TypeName = "Nvarchar(max)")]
        public string? TenAnh { get; set; }
    }
}
