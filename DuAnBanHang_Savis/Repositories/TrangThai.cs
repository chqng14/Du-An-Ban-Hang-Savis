using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Data.Repositories
{
    public enum TrangThaiVoucher
    {
        [Description("Hoạt động")]
        HoatDong = 0,
        [Description("Không hoạt động")]
        KhongHoatDong = 1,
        [Description("Chưa bắt đầu")]
        ChuaBatDau = 2,
        [Description("Số lượng voucher đã hết")]
        HetVoucher = 3,

    }
    public enum TrangThaiSale 
    {
        [Description("Hoạt động")]
        HoatDong = 0,
        [Description("Không hoạt động")]
        KhongHoatDong = 1
    }
    
}
