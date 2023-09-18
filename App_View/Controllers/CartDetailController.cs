using App_Data.Models;
using App_View.IServices;
using App_View.Services;
using Microsoft.AspNetCore.Mvc;

namespace App_View.Controllers
{
    public class CartDetailController : Controller
    {
        ICartDetailService _iCartDetailService;
        public CartDetailController()
        {
            _iCartDetailService = new CartDetailService();
        }
        public async Task<IActionResult> ShowAllCartDetail()
        {
            ViewBag.CartDetail = await _iCartDetailService.GetCartDetailsAsync();
            return View();
        }
        public async Task<IActionResult> AddCartDetail()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCartDetail(CartDetails obj)
        {
            var result = await _iCartDetailService.CreateCartDetailAsync(obj);
            if (result) return RedirectToAction("ShowAllCartDetail");
            return View();
        }
        public async Task<IActionResult> EditCartDetail(Guid id)
        {
            var result = await _iCartDetailService.GetCartDetailByIdAsync(id);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> EditCartDetail(Guid id, CartDetails obj)
        {
            var result = await _iCartDetailService.EditCartDetailAsync(id, obj);
            if (result) return RedirectToAction("ShowAllCartDetail");
            return View();
        }
        public async Task<IActionResult> DetailCartDetail(Guid id)
        {
            var result = await _iCartDetailService.GetCartDetailByIdAsync(id);
            return View(result);
        }
        public async Task<IActionResult> DeleteCartDetail(Guid id)
        {
            var result = await _iCartDetailService.DeleteCartDetailAsync(id);
            if (result) return RedirectToAction("ShowAllCartDetail");
            return View();
        }
    }
}
