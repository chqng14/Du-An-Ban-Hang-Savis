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
        public async Task<IActionResult> Post(Role obj)
        {
            var result = iRepos.AddItem(obj);
            return Ok(result);
        }
    }
}
