using App_Data.IRepositories;
using App_Data.Models;
using App_Data.Repositories;
using App_Data.ViewModel;
using App_View.IServices;
using App_View.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Net.WebSockets;

namespace App_View.Controllers
{
    public class CartDetailController : Controller
    {
        private readonly IAllRepo<Cart> repos;
        DbContextModel dbContextModel = new DbContextModel();
        DbSet<Cart> Carts;
        CartService cartServices;
        CartDetailService cartDetailServices;
        UserService userServices;

        private IProductDetailService _productDetailService;

        public CartDetailController(IProductDetailService productDetailService)
        {
            Carts = dbContextModel.Carts;
            AllRepo<Cart> all = new AllRepo<Cart>(dbContextModel, Carts);
            repos = all;
            cartServices = new CartService();
            cartDetailServices = new CartDetailService();
            userServices = new UserService();
            _productDetailService = productDetailService;
        }
        public async Task<ActionResult> ShowAllCartDetail()
        {
            var cart = await cartDetailServices.GetCartDetailsAsync();
            return View(cart);
        }
        public async Task<ActionResult> ShowCart()
        {
            var acc = SessionService.GetUserFromSession(HttpContext.Session, "SaveLoginUser").Id;
            var idCart = (await userServices.GetUserByIdAsync(acc)).Id;
            var a = (await cartDetailServices.GetCartDetailsAsync()).Where(c => c.IdUser == acc).ToList();
            return View(a);
        }

        public async Task<ActionResult> CheckOut()
        {
            var acc = SessionService.GetUserFromSession(HttpContext.Session, "SaveLoginUser").Id;
            var a = (await cartDetailServices.GetCartDetailsAsync()).Where(c => c.IdUser == acc).ToList();
            return View(a);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCartUser(CartViewModel model)
        {
            var acc = SessionService.GetUserFromSession(HttpContext.Session, "SaveLoginUser").Id;
            var product = await _productDetailService.GetProductDTOByIdAsync(model.IdProduct);
            //if (acc == null)
            //{

            //    TempData["dangnhap"] = "Bạn phải đăng nhập";
            //}
            //else
            //{

            var IdCart = (await userServices.GetUsersAsync()).FirstOrDefault(c => c.Id == acc).Id;
            var existing = (await cartDetailServices.GetCartDetailsAsync()).FirstOrDefault(x => x.IdProduct == product.Id && x.IdUser == IdCart);
            if (existing != null)
            {
                //Kiểm tra số lượng vs số lượng tồn
                if (existing.SoLuongCart + model.SoLuongCart <= product.SoLuongTon)
                {
                    // Nếu sản phẩm đã có trong giỏ hàng thì tăng số lượng lên 1
                    existing.SoLuongCart += model.SoLuongCart;
                }
                else
                {
                    TempData["quantityCartUser"] = "Số lượng bạn chọn đã đạt mức tối đa của sản phẩm này";
                    existing.SoLuongCart = Convert.ToInt32(product.SoLuongTon);
                }
                var cartdetail = new CartDetails()
                {
                    ID = existing.Id,
                    IDCTSP = product.Id,
                    IDUser = IdCart,
                    GiaKhuyenMai = Convert.ToDecimal(existing.GiaBan),
                    SoLuong = existing.SoLuongCart,
                };
                cartDetailServices.EditCartDetailAsync(cartdetail);
            }
            else
            {
                var cartDetails = new CartDetails();
                cartDetails.IDUser = IdCart;
                cartDetails.IDCTSP = model.IdProduct;
                cartDetails.SoLuong = model.SoLuongCart;
                cartDetails.GiaKhuyenMai = Convert.ToDecimal(product.GiaBan);
                cartDetails.TrangThai = 0;
                cartDetailServices.CreateCartDetailAsync(cartDetails);
            }

            //}
            return RedirectToAction("ChiTietSP", "Home", new { id = product.Id });

        }


        public async Task<IActionResult> DetailCartDetail(Guid id)
        {
            var cart = repos.GetAll().FirstOrDefault(c => c.IdUser == id);
            return View(cart);
        }
        public async Task<IActionResult> DeleteCartDetail(Guid id)
        {
            var result = await cartDetailServices.DeleteCartDetailAsync(id);
            if (result) return RedirectToAction("ShowCart");
            return RedirectToAction("ShowCart");
        }
        public async Task<IActionResult> CapNhatSoLuongGioHang(Guid idCart, int SoLuong, Guid IdProduct)
        {
            var apiUrl = $"https://localhost:7165/api/CartDetail/Update-cart?id={idCart}&soLuongCart={SoLuong}";
            var httpClient = new HttpClient();
            var response = await httpClient.PutAsync(apiUrl, null);
            var price = Convert.ToDecimal((await _productDetailService.GetProductDTOByIdAsync(IdProduct)).GiaBan * SoLuong);
            var tonggia = string.Format(CultureInfo.GetCultureInfo("vi-VN"), "{0:N0}đ", price);
            //var acc = SessionServices.GetObjFromSession(HttpContext.Session, "acc").TaiKhoan;
            var giohang = (await cartDetailServices.GetCartDetailsAsync()).Where(c => c.IdUser == Guid.Parse("36668394-764E-71F5-D3BE-278C8C20C8A1")).ToList();
            decimal? TongTien = 0;
            foreach (var item in giohang)
            {
                TongTien += item.GiaBan * Convert.ToDecimal(item.SoLuongCart);
            }
            return Json(new { TongTien = TongTien, TongGia = tonggia });

        }
    }
}
