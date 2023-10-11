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
        [Required(ErrorMessage = "Phải nhập tên")]
        public string? Ten { get; set; }
        [Required(ErrorMessage = "Phải chọn loại hình")]
        public int? LoaiHinhKm { get; set; }

        //[CustomMucUuDaiValidation]
        public decimal MucUuDai { get; set; }
        [Required(ErrorMessage = "Điều kiện là trường bắt buộc.")]
        [Range(1, int.MaxValue, ErrorMessage = "Điều kiện phải là số nguyên không âm.")]
        public int? DieuKien { get; set; }
        [Required(ErrorMessage = "Điều kiện là trường bắt buộc.")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng tồn phải lớn hơn hoặc bằng 0.")]
        public int? SoLuongTon { get; set; }


        [Display(Name = "Ngày bắt đầu")]
        [Required(ErrorMessage = "Ngày bắt đầu không được để trống.")]
        public DateTime? NgayBatDau { get; set; }

        [Required(ErrorMessage = "Ngày kết thúc là trường bắt buộc.")]
        //[CustomNgayKetThucValidation(ErrorMessage = "Ngày kết thúc phải lớn hơn hoặc bằng ngày bắt đầu.")]
        public DateTime? NgayKetThuc { get; set; }

        public int? TrangThai { get; set; }

    }


}
