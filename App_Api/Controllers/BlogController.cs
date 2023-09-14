using App_Data.IRepositories;
using App_Data.Models;
using App_Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IAllRepo<Blog> repos;
        DbContextModel context = new DbContextModel();
        DbSet<Blog> blog;

        public BlogController()
        {
            blog = context.Blogs;
            AllRepo<Blog> all = new AllRepo<Blog>(context, blog);
            repos = all;
        }

        // GET: api/<BlogController>
        [HttpGet]
        public IEnumerable<Blog> Get()
        {
            return repos.GetAll();
        }

        // GET api/<BlogController>/5
        [HttpGet("{id}")]
        public IEnumerable<Blog> Get(Guid id)
        {
            return repos.GetAll().Where(c => c.Id == id);
        }

        // POST api/<BlogController>
        [HttpPost]
        public bool CreateSale(string ma, string ten, string noidung, string mota)
        {
            Blog blog = new Blog();
            blog.TenBlog = ten;
            blog.Ma = ma;
            blog.NoiDung = noidung;
            blog.MoTa = mota;
            blog.Id = Guid.NewGuid();

            return repos.AddItem(blog);
        }

        // PUT api/<BlogController>/5
        [HttpPut("{id}")]
        public bool Put(Guid id, string ma, string ten, string noidung, string mota)
        {
            var blog = repos.GetAll().First(p => p.Id == id);
            blog.TenBlog = ten;
            blog.Ma = ma;
            blog.NoiDung = noidung;
            blog.MoTa = mota;
            return repos.EditItem(blog);
        }

        // DELETE api/<BlogController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            var blog = repos.GetAll().First(p => p.Id == id);
            return repos.RemoveItem(blog);
        }
    }
}
