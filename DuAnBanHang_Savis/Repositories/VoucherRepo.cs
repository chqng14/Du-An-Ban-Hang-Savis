using App_Data.IRepositories;
using App_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Data.Repositories
{
    public class VoucherRepo : IVoucherRepo
    {
        private DbContextModel _dbContext;
        public VoucherRepo()
        {
            _dbContext = new DbContextModel();
        }
        public bool EditAllVoucher(List<Voucher> vouchers)
        {
            try
            {
                foreach (var voucher in vouchers)
                {
                    var voucherHH = _dbContext.Vouchers.FirstOrDefault(x => x.Id == voucher.Id);
                    if (voucherHH != null)
                    {
                        voucherHH.TrangThai = (int)TrangThaiVoucher.KhongHoatDong;
                        _dbContext.Vouchers.Update(voucherHH);
                    }
                }
                _dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
                // Trả về false nếu việc cập nhật không thành công
                return false;
            }
        }
    }
}
