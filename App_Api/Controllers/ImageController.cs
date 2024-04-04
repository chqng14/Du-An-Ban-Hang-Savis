using App_Data.IRepositories;
using App_Data.Models;
using App_Data.ViewModel;
using App_Data.ViewModels.Image;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

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
        [HttpPost("create-list-image")]
        public async Task<IActionResult> CreateImage([FromForm] Guid idProductDetail, [FromForm] List<IFormFile> lstIFormFile)
        {
            try
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                string rootPath = Directory.GetParent(currentDirectory).FullName;
                string uploadDirectory = Path.Combine(rootPath, "App_View", "wwwroot", "images", "AnhSanPham");

                foreach (var file in lstIFormFile)
                {
                    if (file.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            file.CopyTo(stream);
                            stream.Position = 0;

                            using (var image = SixLabors.ImageSharp.Image.Load(stream))
                            {
                                if (image.Width > 400 || image.Height > 300)
                                {
                                    image.Mutate(x => x.Resize(new ResizeOptions
                                    {
                                        Size = new SixLabors.ImageSharp.Size(400, 300),
                                        Mode = ResizeMode.Max
                                    }));
                                }

                                var encoder = new JpegEncoder
                                {
                                    Quality = 80
                                };

                                string fileName = Guid.NewGuid().ToString() + file.FileName;
                                string outputPath = Path.Combine(uploadDirectory, fileName);

                                using (var outputStream = new FileStream(outputPath, FileMode.Create))
                                {
                                    await image.SaveAsync(outputStream, encoder);
                                }
                                _allRepoImage.AddItem(new Images { DuongDan = fileName, TenAnh = file.FileName, IdProductDetail = idProductDetail, TrangThai = 1 });

                            }
                        }
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("delete-list-image")]
        public IActionResult DeleteListImage(ResponseImageDeleteVM responseImageDeleteVM)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string rootPath = Directory.GetParent(currentDirectory).FullName;
            string uploadDirectory = Path.Combine(rootPath, "App_View", "wwwroot", "images", "AnhSanPham");
            try
            {
                foreach (var item in _allRepoImage.GetAll().Where(im => im.IdProductDetail == responseImageDeleteVM.idProductDetail && responseImageDeleteVM.lstImageRemove!.Contains(im.DuongDan)))
                {
                    item.TrangThai = 0;
                    _allRepoImage.EditItem(item);
                    string filePath = Path.Combine(uploadDirectory, item.DuongDan);

                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }
    }
}
