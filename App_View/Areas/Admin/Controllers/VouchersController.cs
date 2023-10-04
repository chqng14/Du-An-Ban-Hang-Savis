using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App_Data.Models;
using App_View.IServices;
using App_View.Services;
using App_Data.IRepositories;
using App_Data.Repositories;
using X.PagedList;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App_View.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VouchersController : Controller
    {
        private readonly IVoucherServices voucherServices;
        private readonly ITypeProductRepo typeProductRepo;
        public VouchersController()
        {
            voucherServices = new VoucherServices();
            typeProductRepo = new TypeProductRepo();
        }

        //public async Task<IActionResult> ShowAllVoucher()
        //{
        //    var lstVoucher = await voucherServices.GetAllAsync();
        //    var lstHoatDong = lstVoucher.Where(v => v.TrangThai == 0).ToList();
        //    var lstKhongHoatDong = lstVoucher.Where(v => v.TrangThai == 1).ToList();
        //    var lstChuaBatDau = lstVoucher.Where(v => v.TrangThai == 2).ToList();

        //    // Gán danh sách voucher vào ViewBag
        //    ViewBag.TatCa = lstHoatDong;
        //    ViewBag.HoatDong = lstHoatDong;
        //    ViewBag.KhongHoatDong = lstKhongHoatDong;
        //    ViewBag.ChuaBatDau = lstChuaBatDau;
        //    return View(lstVoucher);
        //}
        public async Task<IActionResult> ShowAllVoucher(string trangThai)
        {
            var lstVoucher = await voucherServices.GetAllAsync();

            switch (trangThai)
            {
                case "hoatDong":
                    lstVoucher = lstVoucher.Where(v => v.TrangThai == 0).ToList();
                    break;
                case "khongHoatDong":
                    lstVoucher = lstVoucher.Where(v => v.TrangThai == 1).ToList();
                    break;
                case "chuaBatDau":
                    lstVoucher = lstVoucher.Where(v => v.TrangThai == 2).ToList();
                    break;
                default:
                    break;
            }

            ViewBag.TatCa = lstVoucher; // Gán danh sách lọc được vào ViewBag.TatCa

            return View(lstVoucher);
        }



        public async Task<ActionResult> Create()
        {
            ViewBag.TypeProduct = new SelectList(typeProductRepo.GetAllProductType(), "Id", "Ten");

            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(Voucher voucher)
        {
            ViewBag.TypeProduct = new SelectList(typeProductRepo.GetAllProductType(), "Id", "Ten");
            if (await voucherServices.AddVoucherAsync(voucher))
            {
                TempData["MessageForCreate"] = "Thêm thành công";
                return RedirectToAction("ShowAllVoucher");
            }
            else
            {
                return View();
            }

        }
        public async Task<ActionResult> Edit(Guid id)
        {
            var a = (await voucherServices.GetAllAsync()).FirstOrDefault(c => c.Id == id);
            return View(a);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Voucher voucher)
        {
            if (await voucherServices.EditVoucher(voucher))
            {
                TempData["AlertMessage"] = "Cập nhật thành công";
                return RedirectToAction("ShowAllVoucher");
            }
            return View(); ;
        }


        public async Task<ActionResult> Details(Guid id)
        {
            var a = (await voucherServices.GetAllAsync()).FirstOrDefault(c => c.Id == id);
            return View(a);
        }
        public async Task<ActionResult> DetailsBeta()
        {
            return View();
        }

        public async Task<ActionResult> Delete(Guid id)
        {
            await voucherServices.RemoveVoucher((await voucherServices.GetAllAsync()).FirstOrDefault(x => x.Id == id));
            TempData["AlertMessage"] = "Xoá thành công";
            return RedirectToAction("ShowAllVoucher");
        }
    }
}
