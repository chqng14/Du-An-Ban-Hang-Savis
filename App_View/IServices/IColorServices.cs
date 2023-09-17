using App_Data.Models;

namespace App_View.IServices
{
    public interface IColorServices
    {
        public Task<List<Color>> GetAllColor();
        public Task<bool> AddColor(Color color);
        public Task<bool> DeleteColor(Guid id);
        public Task<bool> EditColor(Color color);
    }
}
