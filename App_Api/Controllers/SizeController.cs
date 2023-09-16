using App_Data.IRepositories;
using App_Data.Models;
using App_Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        private readonly IAllRepo<App_Data.Models.Size> _allRepo;

        public SizeController(IAllRepo<App_Data.Models.Size> allRepo)
        {
            _allRepo = allRepo;
        }
        [HttpGet("GetAllSize")]
        public IEnumerable<App_Data.Models.Size> Get()
        {
            return _allRepo.GetAll();
        }
        [HttpGet("GetSizeByName")]
        public IEnumerable<App_Data.Models.Size> Get(string name)
        {
            return _allRepo.GetAll().Where(c => c.Size1.Contains(name));
        }
        [HttpPost("createSize")]
        public bool createSize(string tenSize, decimal CM)
        {
            string ma;
            if (_allRepo.GetAll().Count() == 0)
            {
                ma = "Size1";
            }
            else ma = "Size" + _allRepo.GetAll().Max(c => Convert.ToInt32(c.Ma.Substring(4, c.Ma.Length - 4)) + 1);

            var Size = new App_Data.Models.Size();
            Size.Id = Guid.NewGuid();
            Size.Size1 = tenSize;
            Size.Cm = CM;
            Size.Ma = ma;
            Size.TrangThai = 0;
            return _allRepo.AddItem(Size);
        }
        [HttpDelete("DeleteSize")]
        public bool deleteSize(Guid id)
        {
            var idSize = _allRepo.GetAll().First(c => c.Id == id);
            return _allRepo.RemoveItem(idSize);
        }
        [HttpPut("EditSize")]
        public bool editSize(Guid id, string ten, decimal CM, int trangthai)
        {
            var idSize = _allRepo.GetAll().First(c => c.Id == id);
            idSize.Size1 = ten;
            idSize.Cm = CM;
            idSize.TrangThai = trangthai;
            return _allRepo.EditItem(idSize);
        }

    }
}
