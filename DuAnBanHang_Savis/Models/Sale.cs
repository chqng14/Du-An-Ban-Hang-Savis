using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App_Data.Models
{
    public partial class Sale
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mã")]
        public string? Ma { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên")]
        public string? Ten { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập ngày bắt đầu")]
        public DateTime? NgayBatDau { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập ngày kết thúc")]
        [Compare(nameof(NgayBatDau), ErrorMessage = "Ngày kết thúc không được nhỏ hơn ngày bắt đầu")]
        public DateTime? NgayKetThuc { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập loại khuyến mãi")]
        public string? LoaiHinhKm { get; set; }
        [Range(0,90, ErrorMessage ="Mức giảm từ 0->90")]
        public decimal? MucGiam { get; set; }
        public string? MoTa { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập trạng thái")]
        public int? TrangThai { get; set; }
        public virtual ICollection<SaleDetail> DetailSales { get; set; }
    }
}
