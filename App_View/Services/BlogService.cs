using App_Data.Models;
using App_Data.ViewModels.Blog;
using App_View.IServices;

namespace App_View.Services
{
    public class BlogService : IBlogServices
    {
        public async Task<bool> CreateBlog(BlogDTO blogDTO, IFormFile file)
        {
            try
            {
                var _httpClient = new HttpClient();
                using var content = new MultipartFormDataContent();
                content.Add(new StringContent(blogDTO.TieuDe!), "TieuDe");
                content.Add(new StringContent(blogDTO.MoTaNgan!), "MoTaNgan");
                content.Add(new StringContent(blogDTO.NoiDung!), "NoiDung");
                content.Add(new StreamContent(file.OpenReadStream()),"file",file.FileName);
                var response = await _httpClient.PostAsync("https://localhost:7165/api/Blog", content);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<bool>();
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<bool> DeleteBlog(Guid id)
        {
            try
            {
                string apiUrl = $"https://localhost:7165/api/Blog/{id}";
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

        public async Task<bool> EditBlog(Blog p)
        {
            try
            {
                string apiUrl = $"https://localhost:7165/api/Blog/{p.Id}?ma={p.Ma}&ten={p.TieuDe}&noidung={p.NoiDung}&mota={p.MoTaNgan}";
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

        public async Task<List<Blog>> GetAllBlog()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetFromJsonAsync<List<Blog>>("https://localhost:7165/api/Blog");
            return response;
        }
    }
}
