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
                .Where(p => p.NgayKetThuc <= DateTime.Now && p.TrangThai != 0)
                .ToList();

            foreach (var sale in ngayKetThucSale)
            {
                sale.TrangThai = 0;
            }
            _dbContext.SaveChanges();
        }
        public void UpdateExpiredVouchers()
        {
            var currentDate = DateTime.Today;
            var expiredVouchers = _dbContext.Vouchers.Where
                (v => v.SoLuongTon == 0 && v.TrangThai == (int)TrangThaiVoucher.HoatDong || v.SoLuongTon < 0 && v.TrangThai == (int)TrangThaiVoucher.HoatDong || v.NgayKetThuc < currentDate && v.TrangThai == (int)TrangThaiVoucher.HoatDong)
                .ToList();
            foreach (var item in expiredVouchers)
            {
                _voucherRepo.EditAllVoucher(expiredVouchers);
            }
        }
        public void CapNhatGiaBanThucTe()
        {
            var lstKhuyenMaiDangHoatDong = _dbContext.SaleDetails.Where(x => x.TrangThai == 1).ToList();
            var lstCTSP = _dbContext.ProductDetails.ToList();
            foreach (var ctsp in lstCTSP)
            {
                bool check = false;

                foreach (var kmct in lstKhuyenMaiDangHoatDong)
                {
                    if (kmct.IdChiTietSp == ctsp.Id)
                    {
                        var giaThucTe = _dbContext.SaleDetails.Where(x => x.IdChiTietSp == ctsp.Id).ToList();
                        int[] mangKhuyenMai = new int[giaThucTe.Count()];
                        int temp = 0;
                        foreach (var khuyenMai in giaThucTe)
                        {
                            var a = _dbContext.Sales.FirstOrDefault(x => x.Id == khuyenMai.IdSale);
                            mangKhuyenMai[temp] = Convert.ToInt32(a.MucGiam);
                            temp++;
                        }
                        ctsp.GiaThucTe = ctsp.GiaBan - (ctsp.GiaBan * mangKhuyenMai.Max() / 100);
                        _dbContext.ProductDetails.Update(ctsp);
                        check = true;
                        break;
                    }
                }


                if (!check)
                {
                    ctsp.GiaThucTe = ctsp.GiaBan;
                    _dbContext.ProductDetails.Update(ctsp);
                }
            }
            _dbContext.SaveChanges();
        }
    }
}
