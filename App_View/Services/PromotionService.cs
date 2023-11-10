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
                .Where(p => p.NgayKetThuc <= DateTime.Now && p.TrangThai != (int)TrangThaiSale.KhongHoatDong)
                .ToList();
            var ngaySale = _dbContext.Sales
                .Where(p => p.NgayKetThuc >= DateTime.Now && p.TrangThai == (int)TrangThaiSale.KhongHoatDong )
                .ToList();
            foreach (var sale in ngayKetThucSale)
            {
                sale.TrangThai = (int)TrangThaiSale.KhongHoatDong;
            }
            foreach (var sale in ngaySale)
            {
                sale.TrangThai = (int)TrangThaiSale.HoatDong;
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
        public void CapNhatTrangThaiSaleDetail()
        {
            var lstHetHan = _dbContext.Sales.Where(x => x.TrangThai == 0 || x.TrangThai == 3).ToList();
            var lstDangKhuyenMai = _dbContext.Sales.Where(x => x.TrangThai == 1).ToList();
            var lstKMCT = _dbContext.SaleDetails.ToList();
            foreach (var a in lstHetHan)
            {
                foreach (var b in lstKMCT)
                {
                    if (b.IdSale == a.Id)
                    {
                        b.TrangThai = 0;
                    }
                }
            }
            foreach (var a in lstDangKhuyenMai)
            {
                foreach (var b in lstKMCT)
                {
                    if (b.IdSale == a.Id)
                    {
                        b.TrangThai = 1;
                    }
                }
            }
            _dbContext.SaveChanges();
        }
        public void CapNhatGiaBanThucTe()
        {
            var saleDTs = _dbContext.SaleDetails.AsNoTracking().ToList();
            var lstKhuyenMaiDangHoatDong = _dbContext.SaleDetails.Where(x => x.TrangThai == 1).AsNoTracking().ToList();
            var lstCTSP = _dbContext.ProductDetails.Where(x => x.TrangThai == 2).ToList();
            foreach (var ctsp in lstCTSP)
            {

                var giaThucTe = saleDTs.Where(x => x.IdChiTietSp == ctsp.Id).ToList();
                if (giaThucTe.Any())
                {
                    int[] mangKhuyenMai = new int[giaThucTe.Count()];
                    int temp = 0;
                    foreach (var khuyenMai in giaThucTe)
                    {
                        var a = _dbContext.Sales.FirstOrDefault(x => x.Id == khuyenMai.IdSale);
                        mangKhuyenMai[temp] = Convert.ToInt32(a.MucGiam);
                        temp++;
                    }
                    ctsp.GiaThucTe = ctsp.GiaBan - (ctsp.GiaBan * mangKhuyenMai.Max() / 100);
                    //_dbContext.ProductDetails.Update(ctsp);
                    //_dbContext.SaveChanges();

                }
                else
                {
                    ctsp.GiaThucTe = ctsp.GiaBan;
                    //_dbContext.ProductDetails.Update(ctsp);
                    //_dbContext.SaveChanges();
                }
                _dbContext.ProductDetails.UpdateRange(lstCTSP);
                _dbContext.SaveChanges();
                //foreach (var kmct in lstKhuyenMaiDangHoatDong)
                //{
                //    if (kmct.IdChiTietSp == ctsp.Id)
                //    {

                //        int[] mangKhuyenMai = new int[giaThucTe.Count()];
                //        int temp = 0;
                //        foreach (var khuyenMai in giaThucTe)
                //        {
                //            var a = _dbContext.Sales.FirstOrDefault(x => x.Id == khuyenMai.IdSale);
                //            mangKhuyenMai[temp] = Convert.ToInt32(a.MucGiam);
                //            temp++;
                //        }
                //        ctsp.GiaThucTe = ctsp.GiaBan - (ctsp.GiaBan * mangKhuyenMai.Max() / 100);
                //        _dbContext.ProductDetails.Update(ctsp);
                //        _dbContext.SaveChanges();
                //        check = true;
                //        break;
                //    }
                //}


                //if (!check)
                //{
                //    ctsp.GiaThucTe = ctsp.GiaBan;
                //    _dbContext.ProductDetails.Update(ctsp);
                //    _dbContext.SaveChanges();
                //}
            }

        }
    }
}
