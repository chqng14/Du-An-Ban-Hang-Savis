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
using Microsoft.AspNetCore.Authorization;

namespace App_View.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private IUserService _iUserService;
        private IRoleService _iRoleService;

        public UsersController()
        {
            _iUserService = new UserService();
            _iRoleService = new RoleService();
        }

        // GET: Admin/Users
        public async Task<IActionResult> Index()
        {
            if(SessionService.GetUserFromSession(HttpContext.Session, "SaveLoginAdmin") == null) return RedirectToAction("Login","Home", new { area =""});
            ViewBag.Role = await _iRoleService.GetRolesAsync();
            ViewBag.Roles = new SelectList(await _iRoleService.GetRolesAsync(), "Id", "Ten");
            return View(await _iUserService.GetUsersAsync());
        }

        // GET: Admin/Users/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null || _iUserService.GetUsersAsync() == null)
            {
                return NotFound();
            }

            var user = await _iUserService.GetUserByIdAsync(id);
            ViewBag.Role = await _iRoleService.GetRoleByIdAsync(user.IdRole);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Admin/Users/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Role = new SelectList(await _iRoleService.GetRolesAsync(), "Id", "Ten");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
                var result = await _iUserService.CreateUserAsync(user);
                if (result) return RedirectToAction("Index");
                ViewBag.Role = new SelectList(await _iRoleService.GetRolesAsync(), "Id", "Ten");
                return View();
        }

        // GET: Admin/Users/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null || _iUserService.GetUsersAsync() == null)
            {
                return NotFound();
            }

            var user = await _iUserService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewBag.Role = new SelectList(await _iRoleService.GetRolesAsync(), "Id", "Ten", user.IdRole);
            return View(user);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }
                var result = await _iUserService.EditUserAsync(id, user);
                if (result) return RedirectToAction("Index");
            ViewBag.Role = new SelectList(await _iRoleService.GetRolesAsync(), "Id", "Ten", user.IdRole);
            return View(user);
        }

        // GET: Admin/Users/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null || _iUserService.GetUsersAsync() == null)
            {
                return NotFound();
            }

            var result = await _iUserService.DeleteUserAsync(id);
            if(result) return RedirectToAction("Index");
            return View();
        }
    }
}
