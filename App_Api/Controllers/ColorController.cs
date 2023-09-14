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
    public class ColorController : ControllerBase
    {
        private readonly IAllRepo<Color> _allRepo;

        public ColorController(IAllRepo<Color> allRepo)
        {
            _allRepo = allRepo;
        }

        [HttpGet("GetAllColor")]
        public IEnumerable<Color> Get()
        {
            return _allRepo.GetAll();
        }
        [HttpGet("GetColorByName")]
        public IEnumerable<Color> Get(string name)
        {
            return _allRepo.GetAll().Where(c => c.Ten.Contains(name));
        }
        [HttpPost("createColor")]
        public bool createColor(string ten)
        {
            if (_allRepo.GetAll().Where(x => x.Ten.ToLower() == ten.ToLower()).Count() > 0) { return false; }
            string ma;
            if (_allRepo.GetAll().Count() == 0)
            {
                ma = "Color1";
            }
            else ma = "Color" + _allRepo.GetAll().Max(c => Convert.ToInt32(c.Ma.Substring(5, c.Ma.Length - 5)) + 1);

            var color = new Color();
            color.Ten = ten; color.Id = Guid.NewGuid();
            color.Ma = ma;
            color.TrangThai = 0;
            return _allRepo.AddItem(color);
        }
        [HttpDelete("DeleteColor")]
        public bool deleteColor(Guid id)
        {
            var idColor = _allRepo.GetAll().First(c => c.Id == id);
            return _allRepo.RemoveItem(idColor);
        }
        [HttpPut("EditColor")]
        public bool editColor(Guid id, string ten, int trangthai)
        {
            var idColor = _allRepo.GetAll().First(c => c.Id == id);
            idColor.Ten = ten;
            idColor.TrangThai = trangthai;
            return _allRepo.EditItem(idColor);
        }

    }
}
