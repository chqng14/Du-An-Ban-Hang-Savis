using App_Data.Models;
using App_View.IServices;
using App_View.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace App_View.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IColorServices colorServices;
        public ColorController()
        {
            colorServices = new ColorServices();
        }
       
        [HttpPost]
        public async Task<IActionResult> Create(Color color)
        {
            await colorServices.AddColor(color);
            return RedirectToAction("Create");
        }
    }
}
