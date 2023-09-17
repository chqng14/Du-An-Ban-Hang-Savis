using App_Data.IRepositories;
using App_Data.Models;
using App_Data.Repositories;
using App_View.IServices;
using App_View.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App_View.Controllers
{
    public class SaleDetailController : Controller
    {
        private readonly IAllRepo<SaleDetail> repos;
        private readonly IAllRepo<Product> repoproduct;
        DbContextModel context = new DbContextModel();
        DbSet<SaleDetail> saleDetail;
        ISaleDetailService SaleDetailService;
        public SaleDetailController()
        {
            saleDetail = context.DetailSales;
            AllRepo<SaleDetail> all = new AllRepo<SaleDetail>(context, saleDetail);
            repos = all;
            SaleDetailService = new SaleDetailService();
        }
        // GET: SaleDetailController
        public async Task<IActionResult> GetAllSaleDetail()
        {
            var saledetail = await SaleDetailService.GetAllDetaiSale();
            return View(saledetail);
        }

        // GET: SaleDetailController/Details/5
        public ActionResult Details(Guid id)
        {
            var saledetail = repos.GetAll().FirstOrDefault(c => c.Id == id);
            return View(saledetail);
        }

        // GET: SaleDetailController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SaleDetailController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaleDetail p)
        {
            if (await SaleDetailService.CreateDetaiSale(p))
            {
                return RedirectToAction("GetAllSaleDetail");
            }
            else return BadRequest();
        }

        // GET: SaleDetailController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var saledetail = repos.GetAll().FirstOrDefault(c => c.Id == id);
            return View(saledetail);
        }

        // POST: SaleDetailController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SaleDetail p)
        {
            if (await SaleDetailService.EditDetaiSale(p))
            {
                return RedirectToAction("GetAllSaleDetail");
            }
            else return BadRequest();
        }


        public async Task<IActionResult> Delete(Guid id)
        {
            if (await SaleDetailService.DeleteDetaiSale(id))
            {
                return RedirectToAction("GetAllSaleDetail");
            }
            else return BadRequest();
        }
    }
}
