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
using App_Data.IRepositories;
using App_Data.Repositories;

namespace App_View.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly IAllRepo<Blog> repos;
        DbContextModel _context = new DbContextModel();
        DbSet<Blog> Blog;
        IBlogServices blogServices;

        public BlogController()
        {
            Blog = _context.Blogs;
            AllRepo<Blog> all = new AllRepo<Blog>(_context, Blog);
            repos = all;
            blogServices = new BlogService();
        }

        // GET: Admin/Blog
        public async Task<IActionResult> ShowAllBlog()
        {
            return View(await blogServices.GetAllBlog());
        }

        // GET: Admin/Blog/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || blogServices.GetAllBlog() == null)
            {
                return NotFound();
            }

            var blog = repos.GetAll()
                .FirstOrDefault(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // GET: Admin/Blog/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Blog/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ma,TenBlog,NoiDung,MoTa")] Blog blog)
        {
            if (await blogServices.CreateBlog(blog))
            {
                return RedirectToAction("GetAllBlog");
            }
            else return BadRequest();
        }

        // GET: Admin/Blog/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            var blog = repos.GetAll()
                .FirstOrDefault(m => m.Id == id);
            return View(blog);
        }

        // POST: Admin/Blog/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Ma,TenBlog,NoiDung,MoTa")] Blog blog)
        {
            if (await blogServices.EditBlog(blog))
            {
                return RedirectToAction("GetAllBlog");
            }
            else return BadRequest();
        }

        // GET: Admin/Blog/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (await blogServices.DeleteBlog(id))
            {
                return RedirectToAction("GetAllBlog");
            }
            else return BadRequest();
        }

        //// POST: Admin/Blog/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(Guid id)
        //{
        //    if (_context.Blogs == null)
        //    {
        //        return Problem("Entity set 'DbContextModel.Blogs'  is null.");
        //    }
        //    var blog = await _context.Blogs.FindAsync(id);
        //    if (blog != null)
        //    {
        //        _context.Blogs.Remove(blog);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool BlogExists(Guid id)
        //{
        //    return (_context.Blogs?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
