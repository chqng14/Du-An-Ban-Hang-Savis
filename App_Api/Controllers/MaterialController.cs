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
    public class MaterialController : ControllerBase
    {
        private readonly IAllRepo<Material> _allRepo;

        public MaterialController(IAllRepo<Material> allRepo)
        {
            _allRepo = allRepo;
        }
        [HttpGet("GetAllMaterial")]
        public IEnumerable<Material> Get()
        {
            return _allRepo.GetAll();
        }
        [HttpGet("GetMaterialByName")]
        public IEnumerable<Material> Get(string name)
        {
            return _allRepo.GetAll().Where(c => c.Ten.Contains(name));
        }
        [HttpPost("createMaterial")]
        public bool createMaterial(string ten)
        {

            var Material = new Material();
            Material.Ten = ten; Material.Id = Guid.NewGuid();
            Material.TrangThai = 0;
            return _allRepo.AddItem(Material);
        }
        [HttpDelete("DeleteMaterial")]
        public bool deleteMaterial(Guid id)
        {
            var idMaterial = _allRepo.GetAll().First(c => c.Id == id);
            return _allRepo.RemoveItem(idMaterial);
        }
        [HttpPut("EditMaterial")]
        public bool editMaterial(Guid id, string ten, int trangthai)
        {
            var idMaterial = _allRepo.GetAll().First(c => c.Id == id);
            idMaterial.Ten = ten;
            idMaterial.TrangThai = trangthai;
            return _allRepo.EditItem(idMaterial);
        }

    }
}
