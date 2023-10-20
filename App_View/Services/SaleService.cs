using App_Data.IRepositories;
using App_Data.Models;
using App_Data.Repositories;
using App_View.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using SixLabors.ImageSharp.Formats.Jpeg;
using System.Net.Http;

namespace App_View.Services
{
    public class SaleService : ISaleService
    {
        public async Task<bool> CreateSale([FromForm] Sale sale, [FromForm] IFormFile formFile)
        {
            var _httpClient = new HttpClient();
            DbContextModel dbContextModel = new DbContextModel();
            try
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                string rootPath = Directory.GetParent(currentDirectory).FullName;
                string uploadDirectory = Path.Combine(rootPath, "App_View", "wwwroot", "images", "AnhSale");

               
                    if (formFile.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                        formFile.CopyTo(stream);
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

                                string fileName = Guid.NewGuid().ToString() + formFile.FileName;
                                string outputPath = Path.Combine(uploadDirectory, fileName);

                                using (var outputStream = new FileStream(outputPath, FileMode.Create))
                                {
                                    await image.SaveAsync(outputStream, encoder);
                                }
                                     sale.DuongDanAnh = fileName;
                                 sale.Id = Guid.NewGuid();
                                 dbContextModel.Sales.Add(sale);
                                 dbContextModel.SaveChanges();

                            }
                        }
                    return true;
                    }
                    return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<bool> DeleteSale(Guid id)
        {
            try
            {
                string apiUrl = $"https://localhost:7165/api/Sale/{id}";
                var httpClient = new HttpClient();
                var response = await httpClient.DeleteAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine(response.StatusCode);
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<bool> EditSale(Sale p)
        {
            try
            {
                string apiUrl = $"https://localhost:7165/api/Sale/{p.Id}?ma={p.Ma}&ten={p.Ten}&ngaybatdau={p.NgayBatDau}&ngayketthuc={p.NgayKetThuc}&LoaiHinhKm={p.LoaiHinhKm}&mota={p.MoTa}&mucgiam={p.MucGiam}&trangthai={p.TrangThai}";
                var httpClient = new HttpClient();
                var response = await httpClient.PutAsync(apiUrl, null);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine(response.StatusCode);
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<List<Sale>> GetAllSale()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetFromJsonAsync<List<Sale>>("https://localhost:7165/api/Sale");
            return response;
        }
    }
}
