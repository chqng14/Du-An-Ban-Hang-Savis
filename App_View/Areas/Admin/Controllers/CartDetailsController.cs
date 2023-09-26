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
    public class CartDetailsController : Controller
    {
        ICartDetailService _iCartDetailService;
        IUserService _iUserService;
        private readonly IProductDetailService iProductDetailService;

        public CartDetailsController(IProductDetailService _iProductDetailService)
        {
            _iCartDetailService = new CartDetailService();
            _iUserService = new UserService();
            iProductDetailService = _iProductDetailService;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.CartDetail = await _iCartDetailService.GetCartDetailsAsync();
            ViewBag.ProductDetail = await iProductDetailService.GetListProductViewModelAsync();
            ViewBag.User = await _iUserService.GetUsersAsync();
            return View();
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.ProductDetail = new SelectList(await iProductDetailService.GetListProductViewModelAsync(), "Id", "NameProduct");
            ViewBag.User = new SelectList(await _iUserService.GetUsersAsync(), "Id", "Ten");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CartDetails obj)
        {
            var result = await _iCartDetailService.CreateCartDetailAsync(obj);
            if (result) return RedirectToAction("Index");
            ViewBag.ProductDetail = new SelectList(await iProductDetailService.GetListProductViewModelAsync(), "Id", "NameProduct");
            ViewBag.User = new SelectList(await _iUserService.GetUsersAsync(), "Id", "Ten");
            return View();
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _iCartDetailService.GetCartDetailByIdAsync(id);
            ViewBag.ProductDetail = new SelectList(await iProductDetailService.GetListProductViewModelAsync(), "Id", "NameProduct",result.IDCTSP);
            ViewBag.User = new SelectList(await _iUserService.GetUsersAsync(), "Id", "Ten",result.IDUser);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CartDetails obj)
        {
            var result = await _iCartDetailService.EditCartDetailAsync(obj);
            if (result) return RedirectToAction("Index");
            ViewBag.ProductDetail = new SelectList(await iProductDetailService.GetListProductViewModelAsync(), "ID", "NameProduct", obj.IDCTSP);
            ViewBag.User = new SelectList(await _iUserService.GetUsersAsync(), "Id", "Ten", obj.IDUser);
            return View();
        }
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _iCartDetailService.GetCartDetailByIdAsync(id);
            return View(result);
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _iCartDetailService.DeleteCartDetailAsync(id);
            if (result) return RedirectToAction("Index");
            return View();
        }
    }
}
