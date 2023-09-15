using App_Data.IRepositories;
using App_Data.Models;
using App_Data.ViewModel;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IAllRepo<Images> _allRepoImage;

        public ImageController(IAllRepo<Images> allRepoImage)
        {
            _allRepoImage = allRepoImage;
        }

        public async Task<IActionResult> CreateImage([FromForm] Guid idProductDetail, [FromForm] List<IFormFile> lstIFormFile)
        {
            //string currentDirectory = Directory.GetCurrentDirectory();
            //string rootPath = Directory.GetParent(currentDirectory).FullName;
            //string uploadDirectory = Path.Combine(rootPath, "App_Api", "wwwroot", "Images");

            //foreach (var file in lstIFormFile)
            //{
            //    if (file.Length > 0)
            //    {
            //        using (var stream = new MemoryStream())
            //        {
            //            file.CopyTo(stream);
            //            stream.Position = 0;

            //            using (var image = SixLabors.ImageSharp.Image.Load(stream))
            //            {
            //                if (image.Width > 400 || image.Height > 300)
            //                {
            //                    image.Mutate(x => x.Resize(new ResizeOptions
            //                    {
            //                        Size = new SixLabors.ImageSharp.Size(400, 300),
            //                        Mode = ResizeMode.Max
            //                    }));
            //                }

            //                var encoder = new JpegEncoder
            //                {
            //                    Quality = 80
            //                };

            //                string fileName = Guid.NewGuid().ToString() + file.FileName;
            //                string outputPath = Path.Combine(uploadDirectory, fileName);

            //                using (var outputStream = new FileStream(outputPath, FileMode.Create))
            //                {
            //                    image.Save(outputStream, encoder);
            //                }
            //            }
            //        }
            //    }
            //}

            return Ok();
        }


    
    }
}
