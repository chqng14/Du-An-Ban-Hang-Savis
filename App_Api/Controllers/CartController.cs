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
        [HttpPost]
        public async Task<IActionResult> Post(Guid IdUser, int TrangThai)
        {
            Cart cart = new Cart();
            cart.IdUser = IdUser;
            cart.Trangthai = TrangThai;
            var result = iCartRepos.AddItem(cart);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = iCartRepos.GetAll();
            return Ok(result);
        }
        [HttpGet("{IdUser}")]
        public async Task<Cart> GetById(Guid IdUser)
        {
            return iCartRepos.GetAll().FirstOrDefault(c => c.IdUser == IdUser);
        }
        [HttpPut("{IdUser}")]
        public async Task<IActionResult> Put(Guid IdUser, int TrangThai)
        {
            var cart = iCartRepos.GetAll().FirstOrDefault(c => c.IdUser == IdUser);
            cart.Trangthai = TrangThai;
            var result = iCartRepos.EditItem(cart);
            return Ok(result);
        }
    }
}
