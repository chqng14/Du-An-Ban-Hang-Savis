using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Data.ViewModels.Filter
{
    public class FilterDaTa
    {
        public int TrangHienTai { get; set; } = 1;
        public decimal GiaMin { get; set; }
        public decimal GiaMax { get; set; }
        public List<string>? ListCorlor{ get; set; }
        public List<string>? ListSize{ get; set; }
        public string? Sort{ get; set; }
    }
}
