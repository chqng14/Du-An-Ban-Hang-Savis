using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App_Data.Models
{
    public partial class SaleDetail
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập")]
        public Guid? IdSale { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập")]
        public Guid? IdChiTietSp { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập")]
        public string? MoTa { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập")]
        public int? TrangThai { get; set; }
        public virtual ProductDetails? ProductDetail { get; set; }
        public virtual Sale? Sale { get; set; }  
        

    }
}
