using App_Data.IRepositories;
using App_Data.Models;
using App_Data.Repositories;
using App_Data.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleDetailController : ControllerBase
    {
        private readonly IAllRepo<SaleDetail> SaleDetailRepos;
        private readonly IAllRepo<Sale> SaleRepos;
        private readonly IAllRepo<Product> ProductRepos;
        private readonly IAllRepo<ProductDetails> productDTRepos;
        private DbContextModel context = new DbContextModel();

        public SaleDetailController()
        {
            DbSet<SaleDetail> SaleDetail = context.DetailSales;
            DbSet<Sale> Sale = context.Sales;
            DbSet<Product> Product = context.Products;
            DbSet<ProductDetails> productDetails = context.ProductDetails;

            SaleDetailRepos = new AllRepo<SaleDetail>(context, SaleDetail);
            SaleRepos = new AllRepo<Sale>(context, Sale);
            ProductRepos = new AllRepo<Product>(context, Product);
            productDTRepos = new AllRepo<ProductDetails>(context, productDetails);
        }

        [HttpGet]
        public IEnumerable<SaleDTViewModel> Get()
        {
            var saleDetail = SaleDetailRepos.GetAll();
            var saleDetails = saleDetail.Select(pd => new SaleDTViewModel
            {
                Id = pd.Id,
                Sale = SaleRepos.GetAll().FirstOrDefault(sale => sale.Id == pd.IdSale)?.Ten,
                ChiTietSp = ProductRepos.GetAll().FirstOrDefault(x => x.Id == productDTRepos.GetAll().FirstOrDefault(p => p.Id == pd.IdChiTietSp).IdProduct).Ten,
                MoTa = pd.MoTa,
                TrangThai = pd.TrangThai,
                
            });
            return saleDetails;
        }

        [HttpPost]
        public bool CreateSaleDetail(string mota, int trangthai, Guid IdSale, Guid IdChiTietSp)
        {
            SaleDetail saleDetail = new SaleDetail();
            saleDetail.IdSale = IdSale;
            saleDetail.IdChiTietSp = IdChiTietSp;
            saleDetail.TrangThai = trangthai;
            saleDetail.Id = Guid.NewGuid();
            saleDetail.MoTa = mota;
            return SaleDetailRepos.AddItem(saleDetail);
        }


        [HttpPut("{id}")]
        public bool Put(Guid id, string mota, int trangthai, Guid IdSale, Guid IdChiTietSp)
        {
            var SaleDetail = SaleDetailRepos.GetAll().First(p => p.Id == id);
            SaleDetail.IdSale = IdSale;
            SaleDetail.IdChiTietSp = IdChiTietSp;
            SaleDetail.TrangThai = trangthai;
            SaleDetail.MoTa = mota;
            return SaleDetailRepos.EditItem(SaleDetail);
        }

        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            var SaleDetail = SaleDetailRepos.GetAll().First(p => p.Id == id);
            return SaleDetailRepos.RemoveItem(SaleDetail);
        }
    }
}
