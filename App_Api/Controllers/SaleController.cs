using App_Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {

        DbContextModel context = new DbContextModel();
        DbSet<Sale> sale;
        public SaleController()
        {
            sale = context.Sales;
        }

        // GET: api/<SaleController>
        [HttpGet]
        public IEnumerable<Sale> Get()
        {
            return repos.GetAll();
        }

        // GET api/<SaleController>/5


        // POST api/<SaleController>
        [HttpPost]
        public bool CreateSale(string ma, string ten, DateTime ngaybatdau, DateTime ngayketthuc, string LoaiHinhKm, string mota, decimal mucgiam, int trangthai)
        {
            Sale sale = new Sale();
            sale.Ten = ten;
            sale.Ma = ma;
            sale.NgayBatDau = ngaybatdau;
            sale.NgayKetThuc = ngayketthuc;
            sale.LoaiHinhKm = LoaiHinhKm;
            sale.MoTa = mota;
            sale.MucGiam = mucgiam;
            sale.TrangThai = trangthai;
            sale.Id = Guid.NewGuid();

            return repos.AddItem(sale);
        }

        // PUT api/<SaleController>/5

        [HttpPut("{id}")]
        public bool Put(Guid id, string ma, string ten, DateTime ngaybatdau, DateTime ngayketthuc, string LoaiHinhKm, string mota, decimal mucgiam, int trangthai)
        {
            var sale = repos.GetAll().First(p => p.Id == id);
            sale.Ten = ten;
            sale.Ma = ma;
            sale.NgayBatDau = ngaybatdau;
            sale.NgayKetThuc = ngayketthuc;
            sale.LoaiHinhKm = LoaiHinhKm;
            sale.MoTa = mota;
            sale.MucGiam = mucgiam;
            sale.TrangThai = trangthai;
            return repos.EditItem(sale);
        }

        // DELETE api/<SaleController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            var sale = repos.GetAll().First(p => p.Id == id);
            return repos.RemoveItem(sale);
        }

    }
}
