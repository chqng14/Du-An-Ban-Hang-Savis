using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Data.ViewModels.Filter
{
    public class PagingInfor
    {
        public int TongSoItem { get; set; }
        public int SoItemTrenMotTrang { get; set; }
        public int TrangHienTai { get; set; } = 1;
        public int SoTrang  => (int)Math.Ceiling((double)TongSoItem/SoItemTrenMotTrang);
        
    }
}
