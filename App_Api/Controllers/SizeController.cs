using AppData.IRepositories;
using AppData.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nhom1_Pro.Models;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        private readonly IAllRepo<Size> allRepo;
        DBContextModel dbContextModel = new DBContextModel();
        DbSet<Size> Sizes;
        public SizeController()
        {
            Sizes = dbContextModel.Sizes;
            AllRepo<Size> all = new AllRepo<Size>(dbContextModel, Sizes);
            allRepo = all;
        }
        [HttpGet("GetAllSize")]
        public IEnumerable<Size> Get()
        {
            return allRepo.GetAll();
        }
        [HttpGet("GetSizeByName")]
        public IEnumerable<Size> Get(string name)
        {
            return allRepo.GetAll().Where(c => c.Size1.Contains(name));
        }
        [HttpPost("createSize")]
        public bool createSize(string tenSize, decimal CM)
        {
            string ma;
            if (allRepo.GetAll().Count() == 0)
            {
                ma = "Size1";
            }
            else ma = "Size" + allRepo.GetAll().Max(c => Convert.ToInt32(c.Ma.Substring(4, c.Ma.Length - 4)) + 1);

            var Size = new Size();
            Size.Id = Guid.NewGuid();
            Size.Size1 = tenSize;
            Size.Cm = CM;
            Size.Ma = ma;
            Size.TrangThai = 0;
            return allRepo.AddItem(Size);
        }
        [HttpDelete("DeleteSize")]
        public bool deleteSize(Guid id)
        {
            var idSize = allRepo.GetAll().First(c => c.Id == id);
            return allRepo.RemoveItem(idSize);
        }
        [HttpPut("EditSize")]
        public bool editSize(Guid id, string ten, decimal CM, int trangthai)
        {
            var idSize = allRepo.GetAll().First(c => c.Id == id);
            idSize.Size1 = ten;
            idSize.Cm = CM;
            idSize.TrangThai = trangthai;
            return allRepo.EditItem(idSize);
        }

    }
}
