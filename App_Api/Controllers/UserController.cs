using App_Data.IRepositories;
using App_Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAllRepo<User> iUserRepos;
        private readonly IAllRepo<Cart> iCartRepos;

        public UserController(IAllRepo<User> _iUserRepos, IAllRepo<Cart> _iCartRepos)
        {
            iUserRepos = _iUserRepos;
            iCartRepos = _iCartRepos;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = iUserRepos.GetAll();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<User> GetUserById(Guid id)
        {
            var result = iUserRepos.GetAll().FirstOrDefault(c=>c.Id == id);
            return result;
        }
        [HttpPost]
        public async Task<IActionResult> Post(Guid IdRole, string Ten, int GioiTinh, DateTime NgaySinh, string DiaChi, string SDT, string MatKhau, string Email, string TaiKhoan)
        {
            User obj = new User();
            Random random = new Random();
            Cart cart = new Cart();
            obj.Id = Guid.NewGuid();
            obj.Ma = "USER" + random.Next(100, 999).ToString();
            obj.Ten = Ten;
            obj.GioiTinh = GioiTinh;
            obj.NgaySinh = NgaySinh;
            obj.DiaChi = DiaChi;
            obj.Sdt = SDT;
            obj.TaiKhoan = TaiKhoan;
            obj.MatKhau = MatKhau;
            obj.Email = Email;
            obj.IdRole = IdRole;
            obj.TrangThai = 0;
            cart.IdUser = obj.Id;
            cart.Trangthai = 0;
            var result = iUserRepos.AddItem(obj);
            if(result) iCartRepos.AddItem(cart);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, Guid IdRole, string Ten, int GioiTinh, DateTime NgaySinh, string DiaChi, string SDT, string MatKhau, string Email, string TaiKhoan, int TrangThai)
        {
            var obj = iUserRepos.GetAll().FirstOrDefault(c => c.Id == id);
            obj.Ten = Ten;
            obj.GioiTinh = GioiTinh;
            obj.NgaySinh = NgaySinh;
            obj.DiaChi = DiaChi;
            obj.Sdt = SDT;
            obj.TaiKhoan = TaiKhoan;
            obj.MatKhau = MatKhau;
            obj.Email = Email;
            obj.IdRole = IdRole;
            obj.TrangThai = TrangThai;
            var result = iUserRepos.EditItem(obj);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var obj = iUserRepos.GetAll().FirstOrDefault(c => c.Id == id);
            var result = iUserRepos.RemoveItem(obj);
            return Ok(result);
        }
        [HttpGet("[action]")]
        public async Task<User> GetUserByName(string TenUser)
        {
            var obj = iUserRepos.GetAll().Where(c => c.Ten.Contains(TenUser)).FirstOrDefault();
            return obj;
        }
    }
}
