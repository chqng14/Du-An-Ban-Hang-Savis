using App_Data.IRepositories;
using App_Data.Models;
using App_Data.Repositories;
using App_View.IServices;
using App_View.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using App_Data.Models;
using System.Linq;
using App_Data.ViewModels.ProductDetail;
using App_Data.ViewModel;
using App_Data.ViewModels.Voucher;

namespace App_View.Controllers
{
    public class BillController : Controller
    {
        public IAllRepo<Bill> allRepo;
        public IBillServices billService;
        private ICartDetailService CartDetailServices;
        private IProductDetailService ProductDetailServices;
        private IVoucherServices VoucherServices;

        private readonly IUserService userServices;
        private readonly ICartService cartServices;
        private readonly IRoleService roleServices;
        private IBillDetailsServices billDetailServices;
        private readonly VoucherDTO voucherDTO;

        DbContextModel _dbContextModel;

        public BillController(IProductDetailService productDetailService, IVoucherServices voucherServices)
        {
            allRepo = new AllRepo<Bill>();
            userServices = new UserService();

            billService = new BillServices();
            billDetailServices = new BillDetailServices();
            CartDetailServices = new CartDetailService();
            ProductDetailServices = productDetailService;
            VoucherServices = voucherServices;

            cartServices = new CartService();
            roleServices = new RoleService();
            voucherDTO = new VoucherDTO();
            _dbContextModel = new DbContextModel();

        }
        public async Task<IActionResult> GetAllBill()
        {
            var a = await billService.GetAllBillsAsync();
            ViewBag.Bills = a;
            return View(a);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Bill bill)
        {
            await billService.CreateBillAsync(bill);
            return RedirectToAction("GetAllBill");
        }
        public async Task<IActionResult> Details(Guid id)
        {

            var a = (await billService.GetAllBillsAsync()).FirstOrDefault(x => x.Id == id);
            return View(a);
        }
        //[HttpGet]
        //public IActionResult Edit(Guid id)
        //{
        //    DBContextModel dBContextModel = new DBContextModel();
        //    var a = dBContextModel.Bills.Find(id);
        //    return View(a);
        //}

        public async Task<IActionResult> Edit(Bill bill)
        {
            await billService.UpdateBillAsync(bill);
            return RedirectToAction("GetAllBill");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await billService.DeleteBillAsync(id);
            return RedirectToAction("GetAllBill");
        }

        public async Task<IActionResult> Pay(HoaDonViewModel hoaDonViewModel)
        {
            //decimal tien = Convert.ToDecimal(tongtien);
            //decimal ship = Convert.ToDecimal(phiship);
            var acc = SessionService.GetUserFromSession(HttpContext.Session, "SaveLoginUser").Id;
            //var UserID = (await userServices.GetUsersAsync()).FirstOrDefault(c => c.Id == acc).Id;
            var listcart = (await CartDetailServices.GetCartDetailsAsync()).Where(c => c.IdUser == acc);
            //var IDvoucher = await VoucherServices.GetVoucherAsync(voucher1);
            var vcDTo = new Voucher()
            {
                Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                DieuKien = 0,
                LoaiHinhKm = 0,
                MucUuDai = 0,
                NgayBatDau = DateTime.Now,
                NgayKetThuc = DateTime.Now.AddDays(1),
                Ten = "voucherMacDinh",
                SoLuongTon = 999,
                TrangThai = 0,
                Ma = "VCMACDINH"
            };
            if (_dbContextModel.Vouchers.Any(x => x.Id == vcDTo.Id))
            {
            }
            else
            {
                _dbContextModel.Add(vcDTo);
                _dbContextModel.SaveChanges();
            }
            var bill = new Bill()
            {
                Id = Guid.NewGuid(),
                IdUser = acc,
                IdVoucher = hoaDonViewModel.IdVoucher == null ? Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6") : hoaDonViewModel.IdVoucher,
                NgayTao = DateTime.Now,
                NgayShip = DateTime.Now.AddDays(2),
                NgayNhan = DateTime.Now.AddDays(4),
                NgayThanhToan = DateTime.Now.AddDays(4),
                TenNguoiNhan = hoaDonViewModel.TenNguoiNhan,
                DiaChi = hoaDonViewModel.DiaChi,
                Sdt = hoaDonViewModel.SDT,
                TongTien = hoaDonViewModel.TongTien,
                SoTienGiam = hoaDonViewModel.TienGiam == null ? 0 : hoaDonViewModel.TienGiam,
                TienShip = hoaDonViewModel.TienShip,
                MoTa = "0",
                TrangThai = 0
            };
           
            await billService.CreateBillAsync(bill);
            foreach (var item in listcart)
            {
                await billDetailServices.AddItemAsync(new BillDetails()
                {
                    IdBill = bill.Id,
                    IdProductDetail = item.IdProduct,
                    SoLuong = item.SoLuongCart,
                    DonGia = item.GiaBan,
                    TrangThai = 0
                });
                var productdto = new ProductUpdateDTO()
                {
                    Id = item.IdProduct,
                    SoLuongTon = item.SoLuongCart
                };
                await CartDetailServices.DeleteCartDetailAsync(item.Id);
                var product = await ProductDetailServices.GetProductVMsAsync(item.IdProduct);
                await ProductDetailServices.UpdateProductAsync(productdto);
            }

            return Ok();
        }

        //public async Task<IActionResult> SearchBill(DateTime startDate, DateTime endDate, string ma)
        //{
        //    if (startDate.Year != 1 && endDate.Year != 1)
        //    {
        //        if (ma != null && ma != "")
        //        {
        //            ViewBag.Bills = await billService.GetBillsByDateRangeAsync(startDate, endDate, ma);
        //            return PartialView("SearchBill");
        //        }
        //        else
        //            ViewBag.Bills = await billService.GetBillsByDateRangeAsync(startDate, endDate);
        //        return PartialView("SearchBill");
        //    }
        //    if (ma != null && ma != "")
        //    {
        //        ViewBag.Bills = (await billService.GetAllBillsAsync()).Where(x => x.Ma.Contains(ma)).ToList();
        //        return PartialView("SearchBill");
        //    }
        //    else
        //        ViewBag.Bills = await billService.GetAllBillsAsync();
        //    return PartialView("SearchBill");
        //}
        //public async Task<IActionResult> ShowBillForUser()
        //{
        //    var acc = SessionServices.GetObjFromSession(HttpContext.Session, "acc");
        //    var UserID = (await userServices.GetAllUser()).FirstOrDefault(c => c.TaiKhoan == acc.TaiKhoan).Id;
        //    List<Bill> billList = (await billService.GetAllBillsAsync()).Where(c => c.IdUser == UserID).OrderByDescending(c => c.NgayTao).ToList();
        //    return View(billList);
        //}
        //public async Task<IActionResult> ShowBillDetails(Guid id)
        //{
        //    List<BillDetailView> bills = await billDetailServices.GetByBill(id);
        //    return View(bills);
        //}
        //public async Task<IActionResult> FilterBills(DateTime startDate, DateTime endDate, string ma)
        //{

        //    if (startDate.Year != 1 && endDate.Year != 1)
        //    {
        //        if (ma != null && ma != "")
        //        {
        //            ViewBag.Date = await billService.GetBillsByDateRangeAsync(startDate, endDate, ma);
        //            return PartialView("FilterBills");
        //        }
        //        else
        //            ViewBag.Date = await billService.GetBillsByDateRangeAsync(startDate, endDate);
        //    }
        //    else
        //        ViewBag.Date = await billService.GetAllBillsAsync();
        //    return PartialView("FilterBills");
        //}

    }
}
