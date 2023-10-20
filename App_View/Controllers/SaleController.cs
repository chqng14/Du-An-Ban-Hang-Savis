using App_Data.IRepositories;
using App_Data.Models;
using App_Data.Repositories;
using App_View.IServices;
using App_View.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace App_View.Controllers
{
    public class SaleController : Controller
    {
        private readonly IAllRepo<Sale> repos;
        DbContextModel context = new DbContextModel();
        DbSet<Sale> sale;
        ISaleService SaleService;
        public SaleController()
        {
            sale = context.Sales;
            AllRepo<Sale> all = new AllRepo<Sale>(context, sale);
            repos = all;
            SaleService = new SaleService();
        }
        // GET: SaleController
        public async Task<IActionResult> GetAllSale()
        {

            var sales = await SaleService.GetAllSale();
            return View(sales);
        }

        // GET: SaleController/Details/5
        public ActionResult Details(Guid id)
        {
            var sales = repos.GetAll().FirstOrDefault(c => c.Id == id);
            return View(sales);
        }

        // GET: SaleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SaleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Sale sale, [FromForm] IFormFile formFile)
        {

            //string currentDirectory = Directory.GetCurrentDirectory();
            //string rootPath = Directory.GetParent(currentDirectory).FullName;
            //string uploadDirectory = Path.Combine(rootPath, "App_View", "wwwroot", "images", "AnhSale");
            //if (formFile.Length > 0)
            //{
            //    using (var stream = new MemoryStream())
            //    {
            //        formFile.CopyTo(stream);
            //        stream.Position = 0;

            //        using (var image = SixLabors.ImageSharp.Image.Load(stream))
            //        {
            //            if (image.Width > 400 || image.Height > 300)
            //            {
            //                image.Mutate(x => x.Resize(new ResizeOptions
            //                {
            //                    Size = new SixLabors.ImageSharp.Size(400, 300),
            //                    Mode = ResizeMode.Max
            //                }));
            //            }

            //            var encoder = new JpegEncoder
            //            {
            //                Quality = 80
            //            };

            //            string fileName = Guid.NewGuid().ToString() + formFile.FileName;
            //            string outputPath = Path.Combine(uploadDirectory, fileName);

            //            using (var outputStream = new FileStream(outputPath, FileMode.Create))
            //            {
            //                await image.SaveAsync(outputStream, encoder);
            //            }
            //            sale.DuongDanAnh = fileName;
            //        }
            //    }
            //}
            if (await SaleService.CreateSale(sale,formFile))
            {
                return RedirectToAction("GetAllSale");
            }
            else return BadRequest();
        }

        // GET: SaleController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var sp = repos.GetAll().FirstOrDefault(x => x.Id == id);

            if (sp == null)
            {
                return NotFound();
            }

            return View(sp);
        }

        // POST: SaleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Sale p)
        {
            if (await SaleService.EditSale(p))
            {
                return RedirectToAction("GetAllSale");
            }
            else return BadRequest();
        }


        public async Task<IActionResult> Delete(Guid id)
        {
            if (await SaleService.DeleteSale(id))
            {
                return RedirectToAction("GetAllSale");
            }
            else return BadRequest();
        }
    }
}
