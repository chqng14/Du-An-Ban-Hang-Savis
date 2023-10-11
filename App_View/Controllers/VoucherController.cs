using App_Data.Models;
using App_View.IServices;
using App_View.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App_View.Controllers
{
    public class VoucherController : Controller
    {
        // GET: VoucherController
        private IVoucherServices voucherServices;



        public VoucherController(IVoucherServices voucherServices)
        {
            this.voucherServices = voucherServices;
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

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> EditAsync(Voucher voucher)
        //{
        //    await voucherServices.EditVoucher(voucher);
        //    return RedirectToAction("ShowAllVoucher");
        //}


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
