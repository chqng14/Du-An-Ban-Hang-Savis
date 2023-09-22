using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Data.Models
{
    public partial class Role
    {
        public Guid Id { get; set; }
        public string Ma { get; set; }
        [Required(ErrorMessage = "Mời nhập tên chức vụ !")]
        public string Ten { get; set; }
        [Required(ErrorMessage = "Mời nhập trạng thái !")]
        public int TrangThai { get; set; }
        public virtual List<User> Users { get; set; }
    }
}
