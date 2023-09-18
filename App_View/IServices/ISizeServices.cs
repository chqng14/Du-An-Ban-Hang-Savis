using App_Data.Models;

namespace App_View.IServices
{
    public interface ISizeServices
    {
        public Task<List<Size>> GetAllSize();
        public Task<bool> AddSize(Size size);
        public Task<bool> DeleteSize(Guid id);
        public Task<bool> EditSize(Size size);
    }
}
