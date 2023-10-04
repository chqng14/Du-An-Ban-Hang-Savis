using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Data.Models
{
    public partial class User
    {
        public Guid Id { get; set; }
        public Guid IdRole { get; set; }
        public string Ma { get; set; }
        [Required(ErrorMessage = "Mời nhập tên tên người dùng !")]
        public string Ten { get; set; }
        [Required(ErrorMessage = "Mời nhập giới tính !")]
        public int GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string DiaChi { get; set; }
        [Required(ErrorMessage = "Mời nhập số điện thoại !")]
        [RegularExpression(@"^0[0-9]{9,10}$", ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string Sdt { get; set; }
        [Required(ErrorMessage = "Mời nhập mật khẩu !")]
        [MinLength(4, ErrorMessage = "Mật khẩu không được dưới 4 ký tự")]
        [StringLength(20, ErrorMessage = "Mật khẩu không được quá 20 ký tự !")]
        public string MatKhau { get; set; }
        [Required(ErrorMessage = "Mời nhập email !")]
        [EmailAddress(ErrorMessage = "Mời nhập đúng định dạng email !")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Mời nhập tài khoản !")]
        [StringLength(16, ErrorMessage = "Tên tài khoản không được quá 16 ký tự !")]
        [MinLength(4, ErrorMessage = "Tên tài khoản không được nhỏ hơn 4 ký tự !")]
        public string TaiKhoan { get; set; }
        [Required(ErrorMessage = "Mời nhập trạng thái !")]
        public int TrangThai { get; set; }
        public virtual Role Roles { get; set; }
        public virtual List<Bill> Bills { get; set; }

        public virtual Cart Carts { get; set; }
    }
}
