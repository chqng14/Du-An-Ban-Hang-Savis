using App_Data.IRepositories;
using App_Data.Models;
using App_Data.Repositories;
using App_Data.ViewModel;
using App_View.IServices;
using App_View.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private ProductDetailService productDetailService;

        public CartDetailController()
        {
            Carts = dbContextModel.Carts;
            AllRepo<Cart> all = new AllRepo<Cart>(dbContextModel, Carts);
            repos = all;
            cartServices = new CartService();
            cartDetailServices = new CartDetailService();
            userServices = new UserService();
            //productDetailService = new ProductDetailService();
        }
        public async Task<ActionResult> ShowAllCartDetail()
        {
            var cart = await cartDetailServices.GetCartDetailsAsync();
            return View(cart);
        }
        public async Task<ActionResult> ShowCart()
        {
            //var acc = SessionServices.GetObjFromSession(HttpContext.Session, "acc").TaiKhoan;
            //var idCart = (await userServices.GetAllUser()).FirstOrDefault(c => c.TaiKhoan == acc).Id;
            var a = (await cartDetailServices.GetCartDetailsAsync()).Where(c => c.IdUser == Guid.Parse("36668394-764E-71F5-D3BE-278C8C20C8A1")).ToList();
            return View(a);
        }

        public async Task<ActionResult> CheckOut()
        {
            var a = (await cartDetailServices.GetCartDetailsAsync()).Where(c => c.IdUser == Guid.Parse("36668394-764E-71F5-D3BE-278C8C20C8A1")).ToList();
            return View(a);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCartUser(CartViewModel model)
        {
            //var acc = SessionServices.GetObjFromSession(HttpContext.Session, "acc");
            var product = await productDetailService.GetProductDTOByIdAsync(model.IdProduct);
            //if (acc == null)
            //{

            //    TempData["dangnhap"] = "Bạn phải đăng nhập";
            //}
            //else
            //{

            var IdCart = (await userServices.GetUsersAsync()).FirstOrDefault(c => c.Id == Guid.Parse("36668394-764E-71F5-D3BE-278C8C20C8A1")).Id;
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
                cartDetails.ID = Guid.NewGuid();
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




        public async Task<IActionResult> AddCartDetail()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCartDetail(CartDetails obj)
        {
            var result = await cartDetailServices.CreateCartDetailAsync(obj);
            if (result) return RedirectToAction("ShowAllCartDetail");
            return View();
        }
        public async Task<IActionResult> EditCartDetail(Guid id)
        {
            var result = await cartDetailServices.GetCartDetailByIdAsync(id);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> EditCartDetail(CartDetails obj)
        {
            var result = await cartDetailServices.EditCartDetailAsync(obj);
            if (result) return RedirectToAction("ShowAllCartDetail");
            return View();
        }
        public async Task<IActionResult> DetailCartDetail(Guid id)
        {
            var cart = repos.GetAll().FirstOrDefault(c => c.IdUser == id);
            return View(cart);
        }
        public async Task<IActionResult> DeleteCartDetail(Guid id)
        {
            var result = await cartDetailServices.DeleteCartDetailAsync(id);
            if (result) return RedirectToAction("ShowAllCartDetail");
            return View();
        }
    }
}
