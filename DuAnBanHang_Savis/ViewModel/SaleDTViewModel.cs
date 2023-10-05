using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Data.ViewModel
{
    public class SaleDTViewModel
    {
        public Guid Id { get; set; }
        public string? Sale { get; set; }
        public string? Product { get; set; }
        public string? MoTa { get; set; }
        public int? TrangThai { get; set; }
    }
}
