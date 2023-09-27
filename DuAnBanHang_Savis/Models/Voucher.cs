using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App_Data.Models
{
    public partial class Voucher
    {
        public Guid Id { get; set; }
        public string? Ten { get; set; }
        public string? Ma { get; set; }
        [Required(ErrorMessage = "Phải chọn loại hình")]
        public string? LoaiHinhKm { get; set; }
        [Required(ErrorMessage = "Mức yêu đãi phải nhập")]
        [CustomValidation(typeof(Voucher), "ValidateMucUuDai")]
        public decimal MucUuDai { get; set; }
        public string? PhamVi { get; set; }
        public string? DieuKien { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Số lượng tồn phải lớn hơn hoặc bằng 0.")]
        public int? SoLuongTon { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Số lần sử dụng phải lớn hơn hoặc bằng 0.")]
        public int? SoLanSuDung { get; set; }

        [Display(Name = "Ngày bắt đầu")]
        [Required(ErrorMessage = "Ngày bắt đầu không được để trống.")]
        public DateTime? NgayBatDau { get; set; }

        [Display(Name = "Ngày kết thúc")]
        [Required(ErrorMessage = "Ngày kết thúc không được để trống.")]
        [Compare(nameof(NgayBatDau), ErrorMessage = "Ngày kết thúc không được nhỏ hơn ngày bắt đầu")]
        public DateTime? NgayKetThuc { get; set; }

        public int? TrangThai { get; set; }
        public virtual List<Bill> Bills { get; set; }



        //Validate MucUuDai
        public static ValidationResult ValidateMucUuDai(decimal mucUuDai, ValidationContext context)
        {
            var voucher = context.ObjectInstance as Voucher;

            if (voucher != null)
            {
                if (voucher.LoaiHinhKm == "Giamtheo%")
                {
                    if (mucUuDai < 1 || mucUuDai > 100)
                    {
                        return new ValidationResult("Mức ưu đãi phải nằm trong khoảng từ 1 đến 100% khi giảm theo %.");
                    }
                }
                else if (voucher.LoaiHinhKm == "Giamtheotien" || voucher.LoaiHinhKm == "FreeShip")
                {
                    if (mucUuDai < 0)
                    {
                        return new ValidationResult("Mức ưu đãi phải lớn hơn hoặc bằng 0 khi giảm theo tiền hoặc FreeShip.");
                    }
                }
            }

            return ValidationResult.Success;
        }
    }


}
