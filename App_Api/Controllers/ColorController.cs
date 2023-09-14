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
        private readonly IAllRepo<Color> allRepo;

        public ColorController()
        {
            allRepo = new AllRepo<Color>();
        }
        [HttpGet("GetAllColor")]
        public IEnumerable<Color> Get()
        {
            return allRepo.GetAll();
        }
        [HttpGet("GetColorByName")]
        public IEnumerable<Color> Get(string name)
        {
            return allRepo.GetAll().Where(c => c.Ten.Contains(name));
        }
        [HttpPost("createColor")]
        public bool createColor(string ten)
        {
            string ma;
            if (allRepo.GetAll().Count() == 0)
            {
                ma = "Color1";
            }
            else ma = "Color" + allRepo.GetAll().Max(c => Convert.ToInt32(c.Ma.Substring(5, c.Ma.Length - 5)) + 1);

            var color = new Color();
            color.Ten = ten; color.Id = Guid.NewGuid();
            color.Ma = ma;
            color.TrangThai = 0;
            return allRepo.AddItem(color);
        }
        [HttpDelete("DeleteColor")]
        public bool deleteColor(Guid id)
        {
            var idColor = allRepo.GetAll().First(c => c.Id == id);
            return allRepo.RemoveItem(idColor);
        }
        [HttpPut("EditColor")]
        public bool editColor(Guid id, string ten, int trangthai)
        {
            var idColor = allRepo.GetAll().First(c => c.Id == id);
            idColor.Ten = ten;
            idColor.TrangThai = trangthai;
            return allRepo.EditItem(idColor);
        }

    }
}
