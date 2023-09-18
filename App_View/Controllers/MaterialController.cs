using App_Data.Models;
using App_View.IServices;
using App_View.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;

namespace App_View.Controllers
{
    public class MaterialController : Controller
    {
        private readonly IMaterialServices materialServices;

        public MaterialController()
        {
            materialServices = new MaterialServices();
        }
        public async Task<IActionResult> Index()
        {
            var allMaterial = await materialServices.GetAllMateroal();
            return View(allMaterial);
        }
        public IActionResult Create() 
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Material material)
        {
            await materialServices.AddMaterial(material);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var editColor = (await materialServices.GetAllMateroal()).FirstOrDefault(x => x.Id == id);
            return View(editColor);
        }
        public async Task<IActionResult> Edit(Material material)
        {
            await materialServices.Edit(material);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            await materialServices.RemoveMaterial(id);
            return RedirectToAction("Index");
        }
    }
}
