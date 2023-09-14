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
    public class MaterialController : ControllerBase
    {
        private readonly IAllRepo<Material> allRepo;
        DBContextModel dbContextModel = new DBContextModel();
        DbSet<Material> Materials;
        public MaterialController()
        {
            Materials = dbContextModel.Materials;
            AllRepo<Material> all = new AllRepo<Material>(dbContextModel, Materials);
            allRepo = all;
        }
        [HttpGet("GetAllMaterial")]
        public IEnumerable<Material> Get()
        {
            return allRepo.GetAll();
        }
        [HttpGet("GetMaterialByName")]
        public IEnumerable<Material> Get(string name)
        {
            return allRepo.GetAll().Where(c => c.Ten.Contains(name));
        }
        [HttpPost("createMaterial")]
        public bool createMaterial(string ten)
        {
            string ma;
            if (allRepo.GetAll().Count() == 0)
            {
                ma = "Material1";
            }
            else ma = "Material" + allRepo.GetAll().Max(c => Convert.ToInt32(c.Ma.Substring(8, c.Ma.Length - 8)) + 1);

            var Material = new Material();
            Material.Ten = ten; Material.Id = Guid.NewGuid();
            Material.Ma = ma;
            Material.TrangThai = 0;
            return allRepo.AddItem(Material);
        }
        [HttpDelete("DeleteMaterial")]
        public bool deleteMaterial(Guid id)
        {
            var idMaterial = allRepo.GetAll().First(c => c.Id == id);
            return allRepo.RemoveItem(idMaterial);
        }
        [HttpPut("EditMaterial")]
        public bool editMaterial(Guid id, string ten, int trangthai)
        {
            var idMaterial = allRepo.GetAll().First(c => c.Id == id);
            idMaterial.Ten = ten;
            idMaterial.TrangThai = trangthai;
            return allRepo.EditItem(idMaterial);
        }

    }
}
