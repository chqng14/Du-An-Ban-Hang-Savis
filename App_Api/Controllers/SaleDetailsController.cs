using App_Data.IRepositories;
using App_Data.Models;
using App_Data.Repositories;
using App_Data.ViewModel;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
        DbSet<SaleDetail> _SaleDetail;
        private readonly IMapper _mapper;
        public SaleDetailController(IMapper mapper)
        {
            _SaleDetail = context.DetailSales;
            DbSet<Sale> Sale = context.Sales;
            DbSet<Product> Product = context.Products;
            DbSet<ProductDetails> productDetails = context.ProductDetails;
            _mapper = mapper;
            SaleDetailRepos = new AllRepo<SaleDetail>(context, _SaleDetail);
            SaleRepos = new AllRepo<Sale>(context, Sale);
            ProductRepos = new AllRepo<Product>(context, Product);
            productDTRepos = new AllRepo<ProductDetails>(context, productDetails);
        }

        [HttpGet]
        public IEnumerable<SaleDTViewModel> Get()
        {
            var allSale =  ( _SaleDetail.Include(c => c.Sale).Include(c => c.ProductDetail).ThenInclude(y => y.Products)).ToList();
            var allSaleDTO =  _mapper.Map<List<SaleDTViewModel>>(allSale);
            return allSaleDTO;
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
