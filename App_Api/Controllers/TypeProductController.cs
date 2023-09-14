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
    public class TypeProductController : ControllerBase
    {
        private readonly IAllRepo<TypeProduct> _allRepo;

        public TypeProductController(IAllRepo<TypeProduct> allRepo)
        {
            _allRepo = allRepo;
        }

        [HttpGet("{id}")]
        public TypeProduct Get(Guid id)
        {
            return _allRepo.GetAll().First(x => x.Id == id);
        }
        [HttpGet]
        public IEnumerable<TypeProduct> Get()
        {
            return _allRepo.GetAll();
        }

        [HttpPost]
        public bool CreateBill(string ten, int trangThai)
        {
            string ma;
            if (_allRepo.GetAll().Count() == null)
            {
                ma = "TypeProduct1";
            }
            else
            {
                ma = "TypeProduct" + (_allRepo.GetAll().Count() + 1);
            }
            TypeProduct typeProduct = new TypeProduct()
            {
                Id = Guid.NewGuid(),
                Ten = ten,
                Ma = ma,
                TrangThai = trangThai
            };
            return _allRepo.AddItem(typeProduct);
        }


        [HttpDelete("Delete-TypeProduct")]
        public bool Delete(Guid id)
        {
            var typeProduct = _allRepo.GetAll().First(x => x.Id == id);
            return _allRepo.RemoveItem(typeProduct);
        }

        [HttpPut("{id}")]
        public bool Put(string ten, string ma, int trangThai)
        {
            TypeProduct typeProduct = new TypeProduct()
            {
                Ten = ten,
                Ma = ma,
                TrangThai = trangThai
            };
            return _allRepo.EditItem(typeProduct);
        }
    }
}

