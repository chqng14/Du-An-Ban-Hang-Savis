using App_Data.Models;
using App_View.IServices;
using App_View.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App_View.Controllers
{
    public class BillDetailController : Controller
    {
        private IBillDetailsServices billDetailsServices;
        public BillDetailController()
        {
            billDetailsServices = new BillDetailServices();
        }
        // GET: BillDetailController
        public async Task<IActionResult> ShowAllBillDetails()
        {
            var lst = await billDetailsServices.GetAllAsync();
            return View(lst);
        }

        // GET: BillDetailController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var a = (await billDetailsServices.GetAllAsync()).FirstOrDefault(x => x.IdBill == id);
            return View(a);
        }

        // GET: BillDetailController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BillDetailController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BillDetails billDetails)
        {
            await billDetailsServices.AddItemAsync(billDetails);
            return RedirectToAction("ShowAllBillDetails");
        }

        // GET: BillDetailController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var lst = (await billDetailsServices.GetAllAsync()).FirstOrDefault(c => c.Id == id);
            return View(lst);
        }

        // POST: BillDetailController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(BillDetails billDetails)
        {
            await billDetailsServices.EditItem(billDetails);
            return RedirectToAction("ShowAllBillDetails");
        }

        // GET: BillDetailController/Delete/5

        public async Task<ActionResult> Delete(BillDetails billDetails)
        {
            try
            {
                await billDetailsServices.RemoveItem(billDetails);
                return RedirectToAction("ShowAllBillDetails");
            }
            catch
            {
                return RedirectToAction("ShowAllBillDetails");
            }
        }
    }
}
