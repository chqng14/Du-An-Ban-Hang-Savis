using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Data.Models
{
    public class ProductDetail
    {
        public Guid Id { get; set; }
        public Guid IdProduct { get; set; }
        public Guid IdColor { get; set; }
        public Guid IdSize { get; set; }
        public Guid IdTypeProduct { get; set; }
        public Guid IdMaterial { get; set; }
        [Column(TypeName = "decimal(15,0)")]
        public int SoLuongTon { get; set; }
        [Column(TypeName = "decimal(15,0)")]
        public int SoLuongDaBan { get; set; }
        public string MoTa { get; set; }
        [MaxLength(250)]
        public string BaoHanh { get; set; }
        public decimal GiaNhap { get; set; }
        public decimal GiaBan { get; set; }
        public int TrangThai { get; set; }

        //public virtual SaleDetail SaleDetail { get; set; }
    }
}
