using App_Data.Models;
using App_View.IServices;
using App_View.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace App_View.Controllers
{
    public class BillController : Controller
    {
        private IBillServices billServices;
        public BillController()
        {
            billServices = new BillServices();
        }
        // GET: BillController
        public async Task<ActionResult> ShowAllBill()
        {
            var lst = await billServices.GetAllBillsAsync();
            return View(lst);
        }

        // GET: BillController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var lst = (await billServices.GetAllBillsAsync()).FirstOrDefault(c => c.Id == id);
            return View(lst);
        }

        // GET: BillController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BillController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Bill bill)
        {
            await billServices.CreateBillAsync(bill);
            return RedirectToAction("ShowAllBill");
        }

        // GET: BillController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var lst = (await billServices.GetAllBillsAsync()).FirstOrDefault(c => c.Id == id);
            return View(lst);
        }

        // POST: BillController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Bill bill)
        {
            await billServices.UpdateBillAsync(bill);
            return RedirectToAction("ShowAllBill");
        }
        public async Task<ActionResult> Delete(Guid id)
        {
            await billServices.DeleteBillAsync(id);
            return RedirectToAction("ShowAllBill");
        }
        //chang
    }
}
