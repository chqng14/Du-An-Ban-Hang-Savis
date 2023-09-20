using App_View.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace App_View.Services
{
    public class ImageService : IImageService
    {
        private readonly HttpClient _httpClient;

        public ImageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreatImage(Guid idProductDetail, List<IFormFile> lstIFormFile)
        {
            try
            {
                using (var content = new MultipartFormDataContent())
                {
                    content.Add(new StringContent(idProductDetail.ToString()), "idProductDetail");

                    foreach (var file in lstIFormFile)
                    {
                        var streamContent = new StreamContent(file.OpenReadStream());
                        content.Add(streamContent, "lstIFormFile", file.FileName);
                    }

                    var response = await _httpClient.PostAsync("/api/Image/create-list-image", content);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Delete successful. Response: " + responseContent);
                    }
                    else
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Delete failed. Response: " + responseContent);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace); ;
            }
            
        }

        public async Task DeleteImage(Guid idProductDetail, List<string> lstImageRemove)
        {
            try
            {
                var deleteUrl = "/api/Image/delete-list-image";
                var content = new StringContent(JsonConvert.SerializeObject(new { idProductDetail = idProductDetail, lstImageRemove = lstImageRemove }), Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage(HttpMethod.Delete, deleteUrl);
                request.Content = content;

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Delete successful. Response: " + responseContent);
                }
                else
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Delete failed. Response: " + responseContent);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace); // In stack trace để biết vị trí lỗi cụ thể
            }
        }
    }
}
