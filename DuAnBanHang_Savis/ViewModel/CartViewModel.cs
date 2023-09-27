using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Data.ViewModel
{
    public class CartViewModel
    {
        public Guid Id { get; set; }
        public Guid IdUser { get; set; }
        public Guid IdProduct { get; set; }
        public string? Name { get; set; }
        public string? Color { get; set; }
        public string? Size { get; set; }
        public string? TypeProduct { get; set; }
        public string? Material { get; set; }
        public int SoLuongCart { get; set; }
        public decimal? GiaBan { get; set; }
        public int? TrangThai { get; set; }
        public string? LinkImage { get; set; }
    }
}
