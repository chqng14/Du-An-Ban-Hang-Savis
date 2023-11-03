using App_Data.Models;
using App_Data.ViewModels.Blog;

namespace App_View.IServices
{
    public interface IBlogServices
    {
        public Task<List<Blog>> GetAllBlog();
        public Task<bool> CreateBlog(BlogDTO blogDTO,IFormFile file);
        public Task<bool> EditBlog(Blog p);
        public Task<bool> DeleteBlog(Guid id);
    }
}
