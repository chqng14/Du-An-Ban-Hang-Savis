using App_Data.Models;

namespace App_View.IServices
{
    public interface IColorServices
    {
        public Task<List<App_Data.Models.Color>> GetAllColor();
        public Task<bool> AddColor(App_Data.Models.Color color);
        public Task<bool> DeleteColor(Guid id);
        public Task<bool> EditColor(App_Data.Models.Color color);
    }
}
