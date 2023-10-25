using App_Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static App_Data.Models.Voucher;

namespace App_Data.ViewModels.Voucher
{
    public class VoucherDTO
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Tên voucher là trường bắt buộc.")]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "Tên voucher phải có ít nhất 5 ký tự.")]
        [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "Tên voucher không được chứa ký tự đặc biệt, ngoại trừ dấu cách.")]

        public string? Ten { get; set; }
        [Required(ErrorMessage = "Loại hình khuyến mãi là trường bắt buộc.")]
        public int? LoaiHinhKm { get; set; }

        [CustomMucUuDaiValidation]
        public decimal MucUuDai { get; set; }
        [Required(ErrorMessage = "Điều kiện là trường bắt buộc.")]
        [Range(1, int.MaxValue, ErrorMessage = "Điều kiện phải là số nguyên không âm.")]
        public int? DieuKien { get; set; }
        [Required(ErrorMessage = "Số lượng là trường bắt buộc.")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lần sử dụng phải lớn hơn 0.")]
        public int? SoLuongTon { get; set; }


        [Display(Name = "Ngày bắt đầu")]
        [Required(ErrorMessage = "Ngày bắt đầu không được để trống.")]
        public DateTime? NgayBatDau { get; set; }

        [Required(ErrorMessage = "Ngày kết thúc là trường bắt buộc.")]
        [CustomNgayKetThucValidation(ErrorMessage = "Ngày kết thúc phải lớn hơn hoặc bằng ngày bắt đầu.")]
        public DateTime? NgayKetThuc { get; set; }
        public int? TrangThai { get; set; }


    }


}
