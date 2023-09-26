using App_Data.IRepositories;
using App_Data.Models;
using App_Data.Repositories;
using App_Data.ViewModel;
using App_Data.ViewModels.ProductDetail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartDetailController : ControllerBase
    {
        private readonly IAllRepo<CartDetails> allRepo;
        DbContextModel dbContextModel = new DbContextModel();
        DbSet<CartDetails> CartDetails;
        private readonly IAllRepo<ProductDetails> ProductRepo;
        DbSet<ProductDetails> ProductDetails;
        private readonly IAllRepo<Product> _reposSP;
        private readonly IAllRepo<App_Data.Models.Size> _reposSize;
        private readonly IAllRepo<App_Data.Models.Color> _reposColor;
        private readonly IAllRepo<TypeProduct> _reposTypeProduct;
        private readonly IAllRepo<Material> _reposMaterial;
        private readonly IAllRepo<Image> _reposImage;
        private readonly IAllRepo<ProductDetails> _reposCTSP;
        private readonly IAllRepo<User> _repoUser;

        public CartDetailController()
        {
            CartDetails = dbContextModel.CartDetails;
            ProductDetails = dbContextModel.ProductDetails;
            allRepo = new AllRepo<CartDetails>(dbContextModel, CartDetails);
            ProductRepo = new AllRepo<ProductDetails>(dbContextModel, ProductDetails);
            _reposSP = new AllRepo<Product>();
            _reposSize = new AllRepo<App_Data.Models.Size>();
            _reposColor = new AllRepo<App_Data.Models.Color>();
            _reposTypeProduct = new AllRepo<TypeProduct>();
            _reposMaterial = new AllRepo<Material>();
            _reposImage = new AllRepo<Image>();
            _reposCTSP = new AllRepo<ProductDetails>();
            _repoUser = new AllRepo<User>();
        }
        [HttpGet]
        public IEnumerable<CartViewModel> Get()
        {
            var cartdetail = allRepo.GetAll();
            var cartDetails = cartdetail.Select(pd => new CartViewModel
            {
                Id = pd.ID,
                IdUser = pd.IDUser,
                IdProduct = pd.IDCTSP,
                Name = _reposSP.GetAll().FirstOrDefault(x => x.Id == _reposCTSP.GetAll().FirstOrDefault(p => p.Id == pd.IDCTSP).IdProduct).Ten,
                Size = _reposSize.GetAll().FirstOrDefault(s => s.Id == _reposCTSP.GetAll().FirstOrDefault(p => p.Id == pd.IDCTSP).IdSize)?.Size1,
                Color = _reposColor.GetAll().FirstOrDefault(c => c.Id == _reposCTSP.GetAll().FirstOrDefault(p => p.Id == pd.IDCTSP).IdColor)?.Ten,
                Material = _reposMaterial.GetAll().FirstOrDefault(m => m.Id == _reposCTSP.GetAll().FirstOrDefault(p => p.Id == pd.IDCTSP).IdMaterial)?.Ten,
                TypeProduct = _reposTypeProduct.GetAll().FirstOrDefault(tp => tp.Id == _reposCTSP.GetAll().FirstOrDefault(p => p.Id == pd.IDCTSP).IdTypeProduct)?.Ten,
                GiaBan = _reposCTSP.GetAll().FirstOrDefault(c => c.Id == pd.IDCTSP).GiaBan,
                TrangThai = pd.TrangThai,
                SoLuongCart = pd.SoLuong,
                //LinkImage = _reposImage.GetAll().Any(x => x.IdProductDetail == pd.IDCTSP) ? _reposImage.GetAll().Where(pro => pro.IdProductDetail == pd.IDCTSP).FirstOrDefault().TenAnh : null,
            });
            return cartDetails;
        }

        [HttpPut("Update-cart")]
        public async Task<bool> UpdateCart1(Guid Id, int soLuongCart)
        {
            var cartUpdate = allRepo.GetAll().FirstOrDefault(x => x.ID == Id);
            if (cartUpdate != null)
            {
                cartUpdate.SoLuong = soLuongCart;
                allRepo.EditItem(cartUpdate);
            }
            return true;
        }

        [HttpPost]
        public async Task<string> Post(Guid IDUSer, Guid IDCTSP, int SoLuong, decimal GiaKhuyenMai, int TrangThai)
        {
            var cartDetail = allRepo.GetAll().FirstOrDefault(c => c.IDUser == IDUSer && c.IDCTSP == IDCTSP);
            var productDetail = _reposCTSP.GetAll().FirstOrDefault(c => c.Id == IDCTSP);
            if (cartDetail != null)
            {
                cartDetail.SoLuong += SoLuong;
                if (cartDetail.SoLuong > productDetail.SoLuongTon) return "Số lượng không đủ";
                if (allRepo.EditItem(cartDetail)) return "Sản phẩm này đã có trong bill và sẽ được cập nhật ngay";
                return "fail";
            }
            CartDetails cartDetails = new CartDetails();
            cartDetails.ID = Guid.NewGuid();
            cartDetails.IDUser = IDUSer;
            cartDetails.IDCTSP = IDCTSP;
            cartDetails.SoLuong = SoLuong;
            cartDetails.GiaKhuyenMai = GiaKhuyenMai;
            cartDetails.TrangThai = TrangThai;
            var result = allRepo.AddItem(cartDetails);
            if (result) return "Them thanh cong";
            return "Them khong thanh cong";
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var cartDetails = allRepo.GetAll().FirstOrDefault(c => c.ID == id);
            var result = allRepo.RemoveItem(cartDetails);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, int SoLuong, decimal GiaKhuyenMai, int TrangThai)
        {
            var cartDetails = allRepo.GetAll().FirstOrDefault(c => c.ID == id);
            cartDetails.SoLuong = SoLuong;
            cartDetails.GiaKhuyenMai = GiaKhuyenMai;
            cartDetails.TrangThai = TrangThai;
            var result = allRepo.EditItem(cartDetails);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<CartDetails> GetById(Guid id)
        {
            return allRepo.GetAll().FirstOrDefault(c => c.ID == id);
        }
    }
}
