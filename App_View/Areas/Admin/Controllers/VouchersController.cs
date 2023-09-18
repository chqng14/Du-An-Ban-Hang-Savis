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

namespace App_View.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VouchersController : Controller
    {
        private readonly IVoucherServices voucherServices;

        public VouchersController()
        {
            voucherServices = new VoucherServices();
        }

        public async Task<IActionResult> ShowAllVoucher()
        {
            var lst = await voucherServices.GetAllAsync();
            return View(lst);
        }

        public async Task<ActionResult> CreateAsync()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> CreateAsync(Voucher voucher)
        {
            await voucherServices.AddVoucherAsync(voucher);
            return RedirectToAction("ShowAllVoucher");
        }
        public async Task<ActionResult> EditAsync(Guid id)
        {
            var a = (await voucherServices.GetAllAsync()).FirstOrDefault(c => c.Id == id);
            return View(a);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(Voucher voucher)
        {
            await voucherServices.EditVoucher(voucher);
            return RedirectToAction("ShowAllVoucher");
        }


        public async Task<ActionResult> DetailsAsync(Guid id)
        {
            var a = (await voucherServices.GetAllAsync()).FirstOrDefault(c => c.Id == id);
            return View(a);
        }


        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await voucherServices.RemoveVoucher((await voucherServices.GetAllAsync()).FirstOrDefault(x => x.Id == id));
            return RedirectToAction("ShowAllVoucher");
        }
    }
}
