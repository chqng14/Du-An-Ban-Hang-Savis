using App_Data.Models;

namespace App_View.IServices
{
    public interface IBlogServices
    {
        public Task<List<Blog>> GetAllBlog();
        public Task<bool> CreateBlog(Blog p);
        public Task<bool> EditBlog(Blog p);
        public Task<bool> DeleteBlog(Guid id);
    }
}
