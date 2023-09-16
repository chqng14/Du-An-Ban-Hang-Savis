using App_Data.IRepositories;
using App_Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IAllRepo<Cart> iCartRepos;

        public CartController(IAllRepo<Cart> _iCartRepos)
        {
            iCartRepos = _iCartRepos;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = iCartRepos.GetAll();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<Cart> GetById(Guid IdUser)
        {
            return iCartRepos.GetAll().FirstOrDefault(c => c.IdUser == IdUser);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid IdUser, int TrangThai)
        {
            var cart = iCartRepos.GetAll().FirstOrDefault(c => c.IdUser == IdUser);
            cart.Trangthai = TrangThai;
            var result = iCartRepos.EditItem(cart);
            return Ok(result);
        }
    }
}
