using App_Data.Models;
using App_View.IServices;
using App_View.Models;
using App_View.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using System.Diagnostics;

namespace App_View.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IUserService _iUserService;
        IRoleService _iRoleService;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _iUserService = new UserService();
            _iRoleService = new RoleService();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            var checkLogin = _iUserService.GetUsersAsync().Result.FirstOrDefault(c => c.TaiKhoan.Equals(user.TaiKhoan) && c.MatKhau.Equals(user.MatKhau));
            if (checkLogin == null)
            {
                ViewBag.LoginFail = "Tên tài khoản hoặc mật khẩu không chính xác !";
                return View();
            }
            var checkRole = await _iRoleService.GetRolesAsync();
            if (checkRole.Where(c => c.Id == checkLogin.IdRole).FirstOrDefault().Ten.Contains("admin"))
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(User obj,string rePassword)
        {
            User user = new User();
            if(_iUserService.GetUsersAsync().Result.Where(c=>c.TaiKhoan.Equals(obj.TaiKhoan)).ToList().Count >=1)
            {
                ViewBag.SignUpFail = "Tài khoản đã tồn tại, vui lòng chọn một tên tài khoản khác !";
                return View();
            }
            user.Ten = obj.Ten;
            user.GioiTinh = obj.GioiTinh;
            user.NgaySinh = DateTime.Now;
            user.DiaChi = "Chưa biết";
            user.Sdt = obj.Sdt;
            user.TaiKhoan = obj.TaiKhoan;
            if(rePassword != obj.MatKhau)
            {
                ViewBag.SignUpFail = "Mật khẩu nhập lại chưa đúng !";
                return View();
            } 
            user.MatKhau = obj.MatKhau;
            user.Email = obj.Email;
            user.IdRole = _iRoleService.GetRolesAsync().Result.FirstOrDefault(c=>c.Ten.Contains("user")).Id;
            user.TrangThai = 0;
            var result = await _iUserService.CreateUserAsync(user);
            if (result) return RedirectToAction("Login");
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}