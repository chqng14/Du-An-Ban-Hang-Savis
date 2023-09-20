using App_View.IServices;
using App_View.Services;
using Microsoft.AspNetCore.Mvc;

namespace App_View.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ImagesController : Controller
    {
        private readonly IImageService _imageService;

        public ImagesController(IImageService imageService)
        {
            _imageService = imageService;
        }
        [HttpPost]
        public async Task CreateImage([FromForm]Guid idProductDetail,[FromForm] List<IFormFile> lstIFormFile)
        {
            await _imageService.CreatImage(idProductDetail,lstIFormFile);
        }

        [HttpPost]
        public async Task DeleteImage([FromForm]Guid idProductDetail,[FromForm] List<string> lstImageRemove)
        {
            await _imageService.DeleteImage(idProductDetail, lstImageRemove);
        }
    }
}
