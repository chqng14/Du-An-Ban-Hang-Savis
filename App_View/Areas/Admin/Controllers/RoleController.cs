using App_Data.Models;
using App_View.IServices;
using App_View.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace App_View.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        IRoleService _roleService;
        public RoleController()
        {
            _roleService = new RoleService();
        }
        public async Task<IActionResult> ShowAllRole()
        {   
            return View(await _roleService.GetRolesAsync());
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
        public async Task<IActionResult> RemoveRole(Guid id)
        {
            var result = await _roleService.DeleteRoleAsync(id);
            if(result) return RedirectToAction("ShowAllRole");
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
            if (result) return RedirectToAction("ShowAllRole");
            return View();
        }
    }
}
