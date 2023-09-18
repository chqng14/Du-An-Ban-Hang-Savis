using App_Data.IRepositories;
using App_Data.Models;
using App_Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IAllRepo<Product> _allRepo;

        public ProductController(IAllRepo<Product> allRepo)
        {
            _allRepo = allRepo;
        }
        [HttpGet("GetAllProduct")]
        public IEnumerable<Product> Get()
        {
            return _allRepo.GetAll();
        }
      
        [HttpPost("CreateProduct")]
     
        public bool CreateProduct(string ma, string ten, int trangthai)
        {
            Product Product = new Product();
            Product.Ten = ten;
            Product.Ma = ma;
            Product.TrangThai = trangthai;
            Product.Id = Guid.NewGuid();
            return _allRepo.AddItem(Product);
        }


        [HttpPut("UpdateProduct")]
        public bool Put(Guid id, string ma, string ten, int trangthai)
        {
            var Product = _allRepo.GetAll().First(p => p.Id == id);
            Product.Ten = ten;
            Product.Ma = ma;
            Product.TrangThai = trangthai;
            return _allRepo.EditItem(Product);
        }

        [HttpDelete("DeleteProduct")]
        public bool Delete(Guid id)
        {
            var Product = _allRepo.GetAll().First(p => p.Id == id);
            return _allRepo.RemoveItem(Product);
        }   
    }
}

