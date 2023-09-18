using App_Data.Models;
using App_View.IServices;
using App_View.Services;
using Microsoft.AspNetCore.Mvc;

namespace App_View.Controllers
{
    public class RoleController : Controller
    {
        IRoleService _roleService;
        public RoleController()
        {
            _roleService = new RoleService();
        }
        public async Task<IActionResult> ShowAllRole()
        {
            ViewBag.Role = await _roleService.GetRolesAsync();

            return View();
        }
        public ActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(Role obj)
        {
            var result = await _roleService.CreateRoleAsync(obj);
            if (result) return RedirectToAction("ShowAllRole");
            return View();
        }
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            var result = await _roleService.DeleteRoleAsync(id);
            if (result) return RedirectToAction("ShowAllRole");
            return View();
        }
        public async Task<IActionResult> DetailRole(Guid id)
        {
            ViewBag.Role = await _roleService.GetRoleByIdAsync(id);
            return View();
        }
        public async Task<IActionResult> EditRole(Guid id)
        {
            var result = await _roleService.GetRoleByIdAsync(id);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> EditRole(Guid id, Role obj)
        {
            var result = await _roleService.EditRoleAsync(id, obj);
            if(result) return RedirectToAction("ShowAllRole");
            return View();
        }
    }
}
