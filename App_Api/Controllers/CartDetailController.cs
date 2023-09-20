using App_Data.IRepositories;
using App_Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartDetailController : ControllerBase
    {
        private readonly IAllRepo<CartDetails> iCartDetailRepos;
        private readonly IAllRepo<ProductDetails> iProductDetailRepos;

        public CartDetailController(IAllRepo<CartDetails> _iCartDetailRepos, IAllRepo<ProductDetails> _iProductDetailRepos)
        {
            iCartDetailRepos = _iCartDetailRepos;
            iProductDetailRepos = _iProductDetailRepos;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = iCartDetailRepos.GetAll();
            return Ok(result);
        }
        [HttpPost]
        public async Task<string> Post(Guid IDUSer, Guid IDCTSP, int SoLuong, decimal GiaKhuyenMai, int TrangThai)
        {
            var cartDetail = iCartDetailRepos.GetAll().FirstOrDefault(c => c.IDUser == IDUSer && c.IDCTSP == IDCTSP);
            var productDetail = iProductDetailRepos.GetAll().FirstOrDefault(c => c.Id == IDCTSP);
            if (cartDetail != null)
            {
                cartDetail.SoLuong += SoLuong;
                if (cartDetail.SoLuong > productDetail.SoLuongTon) return "Số lượng không đủ";
                if (iCartDetailRepos.EditItem(cartDetail)) return "Sản phẩm này đã có trong bill và sẽ được cập nhật ngay";
                return "fail";
            }
            CartDetails cartDetails = new CartDetails();
            cartDetails.ID = Guid.NewGuid();
            cartDetails.IDUser = IDUSer;
            cartDetails.IDCTSP = IDCTSP;
            cartDetails.SoLuong = SoLuong;
            cartDetails.GiaKhuyenMai = GiaKhuyenMai;
            cartDetails.TrangThai = TrangThai;
            var result = iCartDetailRepos.AddItem(cartDetails);
            if (result) return "Them thanh cong";
            return "Them khong thanh cong";
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var cartDetails = iCartDetailRepos.GetAll().FirstOrDefault(c => c.ID == id);
            var result = iCartDetailRepos.RemoveItem(cartDetails);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, int SoLuong, decimal GiaKhuyenMai, int TrangThai)
        {
            var cartDetails = iCartDetailRepos.GetAll().FirstOrDefault(c => c.ID == id);
            cartDetails.SoLuong = SoLuong;
            cartDetails.GiaKhuyenMai = GiaKhuyenMai;
            cartDetails.TrangThai = TrangThai;
            var result = iCartDetailRepos.EditItem(cartDetails);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<CartDetails> GetById(Guid id)
        {
            return iCartDetailRepos.GetAll().FirstOrDefault(c => c.ID == id);
        }
    }
}
