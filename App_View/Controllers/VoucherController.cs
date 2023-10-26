using App_Data.IRepositories;
using App_Data.Models;
using App_Data.ViewModels.Voucher;
using App_View.IServices;
using App_View.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App_View.Controllers
{
    public class VoucherController : Controller
    {
        private readonly IVoucherServices voucherServices;
        private readonly ITypeProductRepo typeProductRepo;
        public VoucherController(IVoucherServices voucherServices, ITypeProductRepo typeProductRepo)
        {
            this.voucherServices = voucherServices;
            this.typeProductRepo = typeProductRepo;
        }

        public async Task<IActionResult> ShowAllVoucher()
        {
            var lstVoucher = (await voucherServices.GetAllAsync()).Where(c => c.TrangThai == 0).ToList();
            return View(lstVoucher);
        }
        public async Task<IActionResult> UpdateVoucherAfterUseIt(Guid id)
        {
            if (await voucherServices.UpdateVoucherAfterUseIt(id))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
