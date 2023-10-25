using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App_Data.Models;
using App_Data.IRepositories;
using App_Data.Repositories;
using App_View.IServices;
using App_View.Services;
using NuGet.Packaging.Signing;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Newtonsoft.Json;
using System.Text;

namespace App_View.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SaleDetailController : Controller
    {
        private readonly IAllRepo<SaleDetail> repos;
        private readonly IAllRepo<Product> repoproduct;
        DbContextModel context = new DbContextModel();
        DbSet<SaleDetail> saleDetail;
        ISaleDetailService SaleDetailService;
        IProductDetailService ProductDetailService;
        public SaleDetailController(IProductDetailService productDetailService)
        {
            saleDetail = context.DetailSales;
            AllRepo<SaleDetail> all = new AllRepo<SaleDetail>(context, saleDetail);
            repos = all;
            ProductDetailService = productDetailService;
            SaleDetailService = new SaleDetailService();
           
        }
        // GET: SaleDetailController
        public async Task<IActionResult> ShowAllSaleDetail()
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
                return RedirectToAction("ShowAllSaleDetail");
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
                return RedirectToAction("ShowAllSaleDetail");
            }
            else return BadRequest();
        }


        public async Task<IActionResult> Delete(Guid id)
        {
            if (await SaleDetailService.DeleteDetaiSale(id))
            {
                return RedirectToAction("ShowAllSaleDetail");
            }
            else return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> ApllySale()
        {
            var saledetail = await ProductDetailService.GetListProductViewModelAsync();
            ViewData["IdSale"] = new SelectList(context.Sales, "Id", "Ten");
            return View(saledetail);
        }
        [HttpPost]
        public async Task<IActionResult> ApllySale(Guid idSale, List<Guid> selectedProducts)
        {
            ViewData["IdSale"] = new SelectList(context.Sales, "Id", "Ten");
            List<string> DataMessage= new List<string>();
            var successApllySale = "";
            var saledetailVM = await SaleDetailService.GetAllDetaiSale();
            try
            {
                int temp = 0;
                foreach (var IdProduct in selectedProducts)
                {
                    var saledetail = repos.GetAll().Where(x => x.IdChiTietSp == IdProduct);
                    var name = context.Products.FirstOrDefault(x => x.Id == context.ProductDetails.FirstOrDefault(x => x.Id == IdProduct).IdProduct).Ten;
                    var nameSale = context.Sales.FirstOrDefault(x => x.Id == idSale).Ten;
                    if (saledetail!=null&&saleDetail.Count()>0)
                    {
                        int i = 0;
                        
                        foreach (var checkSale in saledetail)
                        {
                            if(checkSale.IdSale== idSale)
                            {
                                i++;
                                break;
                            }
                        }
                        if (i != 0)
                        {
                            
                            DataMessage.Add($"Sản phẩm {name} đang áp dụng chương trình {nameSale}");
                        }
                        else 
                        {
                            var addSale = new SaleDetail()
                            {
                                Id = Guid.NewGuid(),
                                IdSale = idSale,
                                IdChiTietSp = IdProduct,
                                MoTa = "Kaisan",
                                TrangThai = 0
                            };
                            SaleDetailService.CreateDetaiSale(addSale);
                            DataMessage.Add($"Áp dụng thành công chương trình giảm giá {nameSale} với sản phẩm {name}") ;
                        }
                    }
                    else
                    {
                        var addSale = new SaleDetail()
                        {
                            Id = Guid.NewGuid(),
                            IdSale = idSale,
                            IdChiTietSp = IdProduct,
                            MoTa = "Kaisan",
                            TrangThai = 0
                        };
                        SaleDetailService.CreateDetaiSale(addSale);
                        successApllySale = $"Ap dụng thành công chương trình {nameSale} với sản phẩm đã chọn";
                    }
                    temp++;
                }
                ViewBag.Sales = DataMessage;
                return Ok(new {err=DataMessage, add= successApllySale });
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
