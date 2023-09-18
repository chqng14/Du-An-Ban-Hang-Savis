using App_Data.Models;
using App_View.IServices;
using App_View.Services;
using Microsoft.AspNetCore.Mvc;

namespace App_View.Controllers
{
    public class UserController : Controller
    {
        IUserService _iUserService;
        public UserController()
        {
            _iUserService = new UserService();
        }
        public async Task<IActionResult> GetAllUser()
        {
            ViewBag.User = await _iUserService.GetUsersAsync();
            return View();
        }
        public async Task<IActionResult> CreateUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            var result = await _iUserService.CreateUserAsync(user);
            if (result) return RedirectToAction("GetAllUser");
            return View();
        }
        public async Task<IActionResult> EditUser(Guid id)
        {
            var result = await _iUserService.GetUserByIdAsync(id);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(Guid id, User obj)
        {
            var result = await _iUserService.EditUserAsync(id, obj);
            if (result) return RedirectToAction("GetAllUser");
            return View();
        }
        public async Task<IActionResult> DetailUSer(Guid id)
        {
            var result = await _iUserService.GetUserByIdAsync(id);
            return View(result);
        }
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await _iUserService.DeleteUserAsync(id);
            if (result) return RedirectToAction("GetAllUser");
            return View();
        }
    }
}
