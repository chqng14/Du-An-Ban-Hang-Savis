using App_Data.IRepositories;
using App_Data.Models;
using App_Data.Repositories;
using App_Data.ViewModels.Blog;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

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
        private readonly IMapper _mapper;

        public BlogController(IMapper mapper)
        {
            blog = context.Blogs;
            AllRepo<Blog> all = new AllRepo<Blog>(context, blog);
            repos = all;
            _mapper = mapper;
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
        public async Task<bool> CreateBlog([FromForm]BlogDTO blogDTO,[FromForm]IFormFile file)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string rootPath = Directory.GetParent(currentDirectory)!.FullName;
            string uploadDirectory = Path.Combine(rootPath, "App_View", "wwwroot", "images", "blog");
            var blog = _mapper.Map<Blog>(blogDTO);
            blog.Ma = !repos.GetAll().Any() ? "Blog1" : "Blog" + (repos.GetAll().Count() + 1);
            blog.Id = Guid.NewGuid();

            if (file.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    file.CopyTo(stream);
                    stream.Position = 0;

                    using (var image = SixLabors.ImageSharp.Image.Load(stream))
                    {
                        if (image.Width > 400 || image.Height > 300)
                        {
                            image.Mutate(x => x.Resize(new ResizeOptions
                            {
                                Size = new SixLabors.ImageSharp.Size(400, 300),
                                Mode = ResizeMode.Max
                            }));
                        }

                        var encoder = new JpegEncoder
                        {
                            Quality = 80
                        };

                        string fileName = Guid.NewGuid().ToString() + file.FileName;
                        string outputPath = Path.Combine(uploadDirectory, fileName);

                        using (var outputStream = new FileStream(outputPath, FileMode.Create))
                        {
                            await image.SaveAsync(outputStream, encoder);
                        }

                        blog.TenAnh = fileName;
                    }
                }
            }
            return repos.AddItem(blog);
        }

        // PUT api/<BlogController>/5
        [HttpPut("{id}")]
        public bool Put(Guid id, string ma, string ten, string noidung, string mota)
        {
            var blog = repos.GetAll().First(p => p.Id == id);
            blog.TieuDe = ten;
            blog.Ma = ma;
            blog.NoiDung = noidung;
            blog.MoTaNgan = mota;
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
