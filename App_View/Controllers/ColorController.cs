using App_Data.Models;
using App_View.IServices;
using App_View.Services;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace App_View.Controllers
{
    public class ColorController : Controller
    {
        private readonly IColorServices colorServices;

        public ColorController()
        {
            colorServices = new ColorServices();
        }
        public async Task<IActionResult> Index()
        {
            var allColor = await colorServices.GetAllColor();
            return View(allColor);
        }
        public IActionResult Create() 
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Color color)
        {
            await colorServices.AddColor(color);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var editColor = (await colorServices.GetAllColor()).FirstOrDefault(x => x.Id == id);
            return View(editColor);
        }
        public async Task<IActionResult> Edit(Color color)
        {
            await colorServices.EditColor(color);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            await colorServices.DeleteColor(id);
            return RedirectToAction("Index");
        }
    }
}
