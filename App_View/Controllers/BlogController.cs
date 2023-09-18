using App_Data.IRepositories;
using App_Data.Models;
using App_Data.Repositories;
using App_View.IServices;
using App_View.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App_View.Controllers
{
    public class BlogController : Controller
    {
        private readonly IAllRepo<Blog> repos;
        DbContextModel context = new DbContextModel();
        DbSet<Blog> Blog;
        IBlogServices BlogService;
        public BlogController()
        {
            Blog = context.Blogs;
            AllRepo<Blog> all = new AllRepo<Blog>(context, Blog);
            repos = all;
            BlogService = new BlogService();
        }
        // GET: BlogController
        public async Task<IActionResult> GetAllBlog()
        {
            var blog = await BlogService.GetAllBlog();
            return View(blog);
        }

        // GET: BlogController/Details/5
        public ActionResult Details(Guid id)
        {
            var blog = repos.GetAll().FirstOrDefault(x => x.Id == id);
            return View(blog);
        }

        // GET: BlogController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog p)
        {
            if (await BlogService.CreateBlog(p))
            {
                return RedirectToAction("GetAllBlog");
            }
            else return BadRequest();
        }

        // GET: BlogController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var blog = repos.GetAll().FirstOrDefault(x => x.Id == id);
            return View(blog);
        }

        // POST: BlogController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Blog p)
        {
            if (await BlogService.EditBlog(p))
            {
                return RedirectToAction("GetAllBlog");
            }
            else return BadRequest();
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            if (await BlogService.DeleteBlog(id))
            {
                return RedirectToAction("GetAllBlog");
            }
            else return BadRequest();
        }
    }
}
