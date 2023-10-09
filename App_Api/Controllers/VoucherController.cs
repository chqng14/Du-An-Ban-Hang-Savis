using App_Data.IRepositories;
using App_Data.Models;
using App_Data.Repositories;
using App_Data.ViewModels.Voucher;
using AutoMapper;
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
        private readonly IVoucherRepo voucherRepo;
        private readonly IMapper _mapper;
        DbContextModel DbContextModel = new DbContextModel();
        DbSet<Voucher> vouchers;

        public VoucherController(IMapper mapper)
        {
            vouchers = DbContextModel.Vouchers;
            AllRepo<Voucher> all = new AllRepo<Voucher>(DbContextModel, vouchers);
            voucherRepo = new VoucherRepo();
            allRepo = all;
            _mapper = mapper;
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
        public bool AddVoucher(string ten, int loaihinhkm, decimal mucuudai, int dieukien, int soluongton, DateTime ngaybatdau, DateTime ngayketthuc)
        {
            string randomVoucherCode = GenerateRandomVoucherCode();

            var voucher = new Voucher()
            {
                Id = Guid.NewGuid(),
                Ten = ten,
                Ma = randomVoucherCode,
                LoaiHinhKm = loaihinhkm,
                MucUuDai = mucuudai,
                DieuKien = dieukien,
                SoLuongTon = soluongton,
                NgayBatDau = ngaybatdau,
                NgayKetThuc = ngayketthuc,
            };
            if (voucher.NgayBatDau > DateTime.Now)
            {
                voucher.TrangThai = (int)TrangThaiVoucher.ChuaBatDau;
            }
            if (voucher.NgayBatDau <= DateTime.Now)
            {
                voucher.TrangThai = (int)TrangThaiVoucher.HoatDong;
            }
            if (voucher.SoLuongTon == 0)
            {
                voucher.TrangThai = (int)TrangThaiVoucher.KhongHoatDong;
            }
            return allRepo.AddItem(voucher);
        }
        [HttpPut("UpdateVoucher")]
        public bool UpdateVoucher(VoucherDTO voucherDTO)
        {
            var voucherGet = allRepo.GetAll().FirstOrDefault(c => c.Id == voucherDTO.Id);
            if (voucherGet != null)
            {
                _mapper.Map(voucherDTO, voucherGet);
                if (voucherGet.NgayBatDau > DateTime.Now)
                {
                    voucherGet.TrangThai = (int)TrangThaiVoucher.ChuaBatDau;
                }
                if (voucherGet.NgayBatDau <= DateTime.Now)
                {
                    voucherGet.TrangThai = (int)TrangThaiVoucher.HoatDong;
                }
                if (voucherGet.SoLuongTon > 0)
                {
                    voucherGet.TrangThai = (int)TrangThaiVoucher.HoatDong;
                }
                if (voucherGet.SoLuongTon < 0)
                {
                    voucherGet.TrangThai = (int)TrangThaiVoucher.KhongHoatDong;
                }
            }

            return allRepo.EditItem(voucherGet);
        }
        [HttpDelete("{id}")]
        public bool DeleteVoucher(Guid id)
        {
            return allRepo.RemoveItem(allRepo.GetAll().FirstOrDefault(c => c.Id == id));
        }
        [HttpPut("UpdateExpiredVouchers")]
        public bool UpdateExpiredVouchers()
        {
            var currentDate = DateTime.Today;
            var expiredVouchers = allRepo.GetAll()
                .Where(v => v.SoLuongTon == 0 && v.TrangThai == (int)TrangThaiVoucher.HoatDong || v.SoLuongTon < 0 && v.TrangThai == (int)TrangThaiVoucher.HoatDong || v.NgayKetThuc < currentDate && v.TrangThai == (int)TrangThaiVoucher.HoatDong)
                .ToList();
            return voucherRepo.EditAllVoucher(expiredVouchers);
        }
        [HttpGet("GetVoucherDTOByMa/{id}")]
        public VoucherDTO? GetVoucherDTO(Guid id)
        {
            var Voucher = allRepo.GetAll().FirstOrDefault(c => c.Id == id);
            var VoucherDTO = _mapper.Map<VoucherDTO>(Voucher);
            return VoucherDTO;
        }
    }
}
