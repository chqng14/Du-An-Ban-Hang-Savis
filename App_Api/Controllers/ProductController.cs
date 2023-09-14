using AppData.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nhom1_Pro.Models;
using AppData.Repositories;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IAllRepo<Product> repos;
        DBContextModel context = new DBContextModel();
        DbSet<Product> product;
        public ProductController()
        {
            product = context.Products;
            AllRepo<Product> all = new AllRepo<Product>(context, product);
            repos = all;
        }
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return repos.GetAll();
        }
      
        [HttpPost]
     
        public bool CreateProduct(string ma, string ten, int trangthai)
        {
            Product Product = new Product();
            Product.Ten = ten;
            Product.Ma = ma;
            Product.TrangThai = trangthai;
            Product.Id = Guid.NewGuid();
            return repos.AddItem(Product);
        }


        [HttpPut("{id}")]
        public bool Put(Guid id, string ma, string ten, int trangthai)
        {
            var Product = repos.GetAll().First(p => p.Id == id);
            Product.Ten = ten;
            Product.Ma = ma;
            Product.TrangThai = trangthai;
            return repos.EditItem(Product);
        }

        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            var Product = repos.GetAll().First(p => p.Id == id);
            return repos.RemoveItem(Product);
        }   
    }
}

