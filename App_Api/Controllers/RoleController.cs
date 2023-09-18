using App_Data.IRepositories;
using App_Data.Models;
using App_Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IAllRepo<Role> iRepos;

        public RoleController(IAllRepo<Role> _iRepos)
        {
            iRepos = _iRepos;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = iRepos.GetAll().ToList();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Post(string TenRole)
        {
            Role role = new Role();
            Random random = new Random();
            role.Id = Guid.NewGuid();
            role.Ten = TenRole;
            role.Ma = "ROLE" + random.Next(100, 999).ToString();
            role.TrangThai = 0;
            var result = iRepos.AddItem(role);  
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, string TenRole, int TrangThai)
        {
            var role = iRepos.GetAll().FirstOrDefault(c => c.Id == id);
            role.Ten = TenRole;
            role.TrangThai =TrangThai;
            var result = iRepos.EditItem(role);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var role = iRepos.GetAll().FirstOrDefault(c => c.Id == id);
            var result = iRepos.RemoveItem(role);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<Role> GetById(Guid id)
        {
            var role = iRepos.GetAll().FirstOrDefault(c => c.Id == id);
            return role;
        }
        [HttpGet("[action]")]
        public async Task<Role> GetRoleByName(string TenRole)
        {
            var role = iRepos.GetAll().Where(c=>c.Ten.Contains(TenRole)).FirstOrDefault();
            return role;
        }
    }
}
