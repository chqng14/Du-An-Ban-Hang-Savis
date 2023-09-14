using App_Data.IRepositories;
using App_Data.Models;
using App_Data.Repositories;
using App_Data.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillDetailsController : ControllerBase
    {
        private readonly IAllRepo<BillDetails> BillRepo;
        DbContextModel DbContextModel = new DbContextModel();
        DbSet<BillDetails> BillDetails;
        private readonly IAllRepo<ProductDetails> ProductDetailRepo;
        DbSet<ProductDetails> ProductDetails;
        private readonly IAllRepo<Product> ProductRepo;
        DbSet<Product> Products;

        public BillDetailsController()
        {
            BillDetails = DbContextModel.BillDetails;
            ProductDetails = DbContextModel.ProductDetails;
            BillRepo = new AllRepo<BillDetails>(DbContextModel, BillDetails); ;
            ProductDetailRepo = new AllRepo<ProductDetails>(DbContextModel, ProductDetails);
            Products = DbContextModel.Products;
            ProductRepo = new AllRepo<Product>(DbContextModel, Products);
        }
        // GET: api/<BillDetailsController>
        [HttpGet]
        public IEnumerable<BillDetails> Get()
        {
            return BillRepo.GetAll().ToList();
        }
        [HttpGet("idBill")]
        public IEnumerable<BillDetailView> GetByBill(Guid id)
        {
            var lst = (from a in BillRepo.GetAll()
                       join b in (from a1 in ProductDetailRepo.GetAll()
                                  join b1 in ProductRepo.GetAll() on a1.IdProduct equals b1.Id
                                  select new ProductDetailDTO()
                                  {
                                      Id = a1.Id,
                                      Name = b1.Ten
                                  }
                                  ).ToList() on a.IdProductDetail equals b.Id
                       select new BillDetailView()
                       {
                           Id = a.Id,
                           DonGia = a.DonGia,
                           IdProductDetail = a.IdProductDetail,
                           IdBill = a.IdBill,
                           Ten = b.Name,
                           SoLuong = a.SoLuong,
                           TrangThai = a.TrangThai
                       }
                       ).ToList();
            return lst.Where(c => c.IdBill == id);
        }

        // GET api/<BillDetailsController>/5
        [HttpGet("{id}")]
        public IEnumerable<BillDetails> Get(Guid id)
        {
            return BillRepo.GetAll().Where(c => c.IdBill == id).ToList();
        }

        // POST api/<BillDetailsController>
        [HttpPost]
        public string Post(Guid idBill, Guid idProduct, int sl, int trangthai)
        {
            var b = BillRepo.GetAll().FirstOrDefault(c => c.IdBill == idBill && c.IdProductDetail == idProduct);
            var c = ProductDetailRepo.GetAll().FirstOrDefault(a => a.Id == idProduct);
            if (b != null)
            {
                b.SoLuong = b.SoLuong + sl;
                if (b.SoLuong > c.SoLuongTon)
                {
                    return "khum du so luong";
                }
                if (BillRepo.EditItem(b))
                    return "san pham nay da co tron bill va da duoc cap nhap";
                return "khong thanh cong";

            }
            var d = new BillDetails() { Id = Guid.NewGuid(), IdBill = idBill, IdProductDetail = idProduct, DonGia = c.GiaBan, SoLuong = sl, TrangThai = trangthai };
            if (BillRepo.AddItem(d)) return "Them thanh cong";
            return "them khong thanh cong";
        }

        // PUT api/<BillDetailsController>/5
        [HttpPut("{id}")]
        public bool Put(Guid id, Guid idBill, Guid idProduct, int sl, int trangthai)
        {
            var c = ProductDetailRepo.GetAll().FirstOrDefault(a => a.IdProduct == a.IdProduct);
            var a = new BillDetails() { Id = id, IdBill = idBill, IdProductDetail = idProduct, DonGia = (decimal)c.GiaBan, SoLuong = sl, TrangThai = trangthai };
            return BillRepo.EditItem(a);
        }

        // DELETE api/<BillDetailsController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            return BillRepo.RemoveItem(BillRepo.GetAll().FirstOrDefault(c => c.Id == id));
        }
    }
}
