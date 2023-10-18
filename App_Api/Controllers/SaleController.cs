using App_Data.IRepositories;
using App_Data.Models;
using App_Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp.Formats.Jpeg;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly IAllRepo<Sale> repos;
        DbContextModel context = new DbContextModel();
        DbSet<Sale> sale;
        public SaleController()
        {
            sale = context.Sales;
            AllRepo<Sale> all = new AllRepo<Sale>(context, sale);
            repos = all;
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
        public async Task<bool> CreateSaleAsync(string ma, string ten, DateTime ngaybatdau, DateTime ngayketthuc, string LoaiHinhKm, string mota, decimal mucgiam, int trangthai, IFormFile formFile)
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

            string currentDirectory = Directory.GetCurrentDirectory();
            string rootPath = Directory.GetParent(currentDirectory).FullName;
            string uploadDirectory = Path.Combine(rootPath, "App_View", "wwwroot", "images", "AnhSale");
            if (formFile.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    formFile.CopyTo(stream);
                    stream.Position = 0;

                    using (var image = SixLabors.ImageSharp.Image.Load(stream))
                    {
                        if (image.Width > 400 || image.Height > 300)
                        {
                            image.Mutate(x => x.Resize(new ResizeOptions
                            {
                                Size = new SixLabors.ImageSharp.Size(400, 300),
                                Mode = ResizeMode.Max
                            }));
                        }

                        var encoder = new JpegEncoder
                        {
                            Quality = 80
                        };

                        string fileName = Guid.NewGuid().ToString() + formFile.FileName;
                        string outputPath = Path.Combine(uploadDirectory, fileName);

                        using (var outputStream = new FileStream(outputPath, FileMode.Create))
                        {
                            await image.SaveAsync(outputStream, encoder);
                        }
                        sale.DuongDanAnh = fileName;
                        repos.AddItem(sale);

                    }
                }
            }
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
