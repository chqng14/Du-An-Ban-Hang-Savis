using App_Data.IRepositories;
using App_Data.Models;
using App_Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : Controller
    {
        private readonly IAllRepo<Voucher> allRepo;
        DbContextModel DbContextModel = new DbContextModel();
        DbSet<Voucher> vouchers;

        public VoucherController()
        {
            vouchers = DbContextModel.Vouchers;
            AllRepo<Voucher> all = new AllRepo<Voucher>(DbContextModel, vouchers);
            allRepo = all;
        }
        [HttpGet("GetVoucher")]
        public IEnumerable<Voucher> GetAll()
        {
            return allRepo.GetAll();
        }
        [HttpGet("GetVoucherByMa")]
        public Voucher GetAll(string ma)
        {
            return allRepo.GetAll().FirstOrDefault(c => c.Ma == ma);
        }

        private string GenerateRandomVoucherCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();

            string voucherCode = new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return voucherCode;
        }


        [HttpPost("AddVoucher")]
        public bool AddVoucher(string ten, string loaihinhkm, decimal mucuudai, string phamvi, string dieukien, int soluongton, int solansudung, DateTime ngaybatdau, DateTime ngayketthuc, int trangthai)
        {
            string randomVoucherCode = GenerateRandomVoucherCode();

            var voucher = new Voucher()
            {
                Id = Guid.NewGuid(),
                Ten = ten,
                Ma = randomVoucherCode,
                LoaiHinhKm = loaihinhkm,
                MucUuDai = mucuudai,
                PhamVi = phamvi,
                DieuKien = dieukien,
                SoLuongTon = soluongton,
                SoLanSuDung = solansudung,
                NgayBatDau = ngaybatdau,
                NgayKetThuc = ngayketthuc,
                TrangThai = trangthai
            };
            return allRepo.AddItem(voucher);
        }
        [HttpPut("{id}")]
        public bool UpdateVoucher(Guid id, string ten, string ma, string loaihinhkm, decimal mucuudai, string phamvi, string dieukien, int soluongton, int solansudung, DateTime ngaybatdau, DateTime ngayketthuc, int trangthai)
        {
            var voucher = new Voucher()
            {
                Id = id,
                Ten = ten,
                Ma = ma,
                LoaiHinhKm = loaihinhkm,
                MucUuDai = mucuudai,
                PhamVi = phamvi,
                DieuKien = dieukien,
                SoLuongTon = soluongton,
                SoLanSuDung = solansudung,
                NgayBatDau = ngaybatdau,
                NgayKetThuc = ngayketthuc,
                TrangThai = trangthai
            };
            return allRepo.EditItem(voucher);
        }
        [HttpDelete("{id}")]
        public bool DeleteVoucher(Guid id)
        {
            return allRepo.RemoveItem(allRepo.GetAll().FirstOrDefault(c => c.Id == id));
        }
    }
}
