using App_Data.Models;

namespace App_View.IServices
{
    public interface ISizeServices
    {
        public Task<List<App_Data.Models.Size>> GetAllSize();
        public Task<bool> AddSize(App_Data.Models.Size size);
        public Task<bool> DeleteSize(Guid id);
        public Task<bool> EditSize(App_Data.Models.Size size);
    }
}
