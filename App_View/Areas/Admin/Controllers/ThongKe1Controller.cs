using App_Data.ViewModel;
using App_View.IServices;
using App_View.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace App_View.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ThongKe1Controller : Controller
    {
        IBillServices _iBillService;
        IBillDetailsServices _iBillDetailService;

        IUserService _iUserService;
        private readonly IProductDetailService iProductDetailService;

        public ThongKe1Controller(IProductDetailService _iProductDetailService)
        {
            _iBillService = new BillServices();
            _iBillDetailService = new BillDetailServices();
            iProductDetailService = _iProductDetailService;
            _iUserService = new UserService();
        }
        public IActionResult ThongKe()
        {
            return View();
        }
        [HttpGet]
        public async Task<JsonResult> CountHoaDon(DateTime? start, DateTime? end)
        {
            int soLuongHoaDon = 0;
            if (start == null || end == null)
            {
                soLuongHoaDon = _iBillService.GetAllBillsAsync().Result.Count;
                return new JsonResult(soLuongHoaDon);
            }
            soLuongHoaDon = _iBillService.GetBillsByDateRangeAsync(Convert.ToDateTime(start), Convert.ToDateTime(end)).Result.Count;
            return new JsonResult(soLuongHoaDon);
        }
        [HttpGet]
        public async Task<JsonResult> Profit(DateTime? start, DateTime? end)
        {
            decimal profit = 0;
            if (start == null || end == null)
            {
                var hoaDon = await _iBillService.GetAllBillsAsync();

                foreach (var item in hoaDon)
                {
                    profit += Convert.ToDecimal(item.TongTien);
                }
                return new JsonResult(profit);
            }
            var hoaDonRange = await _iBillService.GetBillsByDateRangeAsync(Convert.ToDateTime(start), Convert.ToDateTime(end));

            foreach (var item in hoaDonRange)
            {
                profit += Convert.ToDecimal(item.TongTien);
            }
            return new JsonResult(profit);
        }
        [HttpGet]
        public async Task<JsonResult> CountKhachHang()
        {
            var hoaDon = await _iBillService.GetAllBillsAsync();
            var khachHang = await _iUserService.GetUsersAsync();
            HashSet<Guid> uniqueCustomerIds = new HashSet<Guid>();

            foreach (var item in hoaDon)
            {
                // Kiểm tra xem Id của khách hàng đã tồn tại trong HashSet hay chưa
                if (!uniqueCustomerIds.Contains(Guid.Parse(item.IdUser.ToString())))
                {
                    // Nếu chưa tồn tại, thêm Id vào HashSet và tăng biến đếm
                    uniqueCustomerIds.Add(Guid.Parse(item.IdUser.ToString()));
                }
            }

            // Sử dụng Count để đếm số lượng khách hàng đã mua hàng
            int soKhachHang = uniqueCustomerIds.Count;
            return new JsonResult(soKhachHang);
        }
        [HttpGet]
        public async Task<JsonResult> ThongKeTheoThoiGian(DateTime start, DateTime end)
        {
            int soLuongHoaDon = 0;
            decimal profit = 0;
            int soKhachHang = 0;
            if (start == null || end == null || start > end)
            {
                soLuongHoaDon = _iBillService.GetAllBillsAsync().Result.Count;
                var hoaDon = await _iBillService.GetAllBillsAsync();
                var khachHang = await _iUserService.GetUsersAsync();
                foreach (var item in hoaDon)
                {
                    profit += Convert.ToDecimal(item.TongTien);
                }
                HashSet<Guid> uniqueCustomerIds = new HashSet<Guid>();

                foreach (var item in hoaDon)
                {
                    if (!uniqueCustomerIds.Contains(Guid.Parse(item.IdUser.ToString())))
                    {
                        uniqueCustomerIds.Add(Guid.Parse(item.IdUser.ToString()));
                    }
                }
                soKhachHang = uniqueCustomerIds.Count;
                ThongKeViewModel thongKe = new ThongKeViewModel()
                {
                    SoLuongHoaDon = soLuongHoaDon,
                    DoanhThu = profit,
                    SoLuongKhachHang = soKhachHang
                };
                return new JsonResult(thongKe);
            }
            //Có nhập ngày tháng năm
            soLuongHoaDon = _iBillService.GetBillsByDateRangeAsync(start, end).Result.Count;
            var hoaDonRange = await _iBillService.GetBillsByDateRangeAsync(start, end);
            foreach (var item in hoaDonRange)
            {
                profit += Convert.ToDecimal(item.TongTien);
            }
            HashSet<Guid> uniqueCustomerIds2 = new HashSet<Guid>();

            foreach (var item in hoaDonRange)
            {
                // Kiểm tra xem Id của khách hàng đã tồn tại trong HashSet hay chưa
                if (!uniqueCustomerIds2.Contains(Guid.Parse(item.IdUser.ToString())))
                {
                    // Nếu chưa tồn tại, thêm Id vào HashSet và tăng biến đếm
                    uniqueCustomerIds2.Add(Guid.Parse(item.IdUser.ToString()));
                }
            }

            // Sử dụng Count để đếm số lượng khách hàng đã mua hàng
            soKhachHang = uniqueCustomerIds2.Count;
            ThongKeViewModel thongKe2 = new ThongKeViewModel()
            {
                SoLuongHoaDon = soLuongHoaDon,
                DoanhThu = profit,
                SoLuongKhachHang = soKhachHang
            };
            return new JsonResult(thongKe2);
        }
        public async Task<ActionResult> LowInventoryProducts()
        {
            // Lấy danh sách sản phẩm có tồn kho thấp
            ViewBag.lowInventoryProducts = (await iProductDetailService.GetListProductViewModelAsync()).Where(c => c.SoLuongTon <= 5).ToList();
            int count = (await iProductDetailService.GetListProductViewModelAsync()).Count;
            for (int i = 0; i < count; i++)
            {
                i++;
            }
            return View();
        }
    }

}
