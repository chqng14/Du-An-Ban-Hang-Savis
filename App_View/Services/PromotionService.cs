using App_Data.IRepositories;
using App_Data.Models;
using App_Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace App_View.Services
{
    public class PromotionService
    {
        DbContextModel _dbContext = new DbContextModel();
        private readonly IVoucherRepo _voucherRepo;
        public PromotionService()
        {
            _voucherRepo = new VoucherRepo();
            _dbContext = new DbContextModel();
        }

        public void CheckNgayKetThuc()
        {
            var ngayKetThucSale = _dbContext.Sales
                .Where(p => p.NgayKetThuc <= DateTime.Now && p.TrangThai != 5)
                .ToList();

            foreach (var sale in ngayKetThucSale)
            {
                sale.TrangThai = 5;
            }
            _dbContext.SaveChanges();
        }
        public void UpdateExpiredVouchers()
        {
            var currentDate = DateTime.Today;
            var expiredVouchers = _dbContext.Vouchers.Where
                (v => v.SoLuongTon == 0 && v.TrangThai == (int)TrangThaiVoucher.HoatDong || v.SoLanSuDung == v.SoLuongTon && v.TrangThai == (int)TrangThaiVoucher.HoatDong || v.NgayKetThuc < currentDate && v.TrangThai == (int)TrangThaiVoucher.HoatDong)
                .ToList();
            foreach (var item in expiredVouchers)
            {
                _voucherRepo.EditAllVoucher(expiredVouchers);
            }
        }
    }
}
