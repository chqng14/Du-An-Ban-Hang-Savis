using App_Data.Models;

namespace App_View.IServices
{
    public interface ISaleService
    {
        public Task<List<Sale>> GetAllSale();
        public Task<bool> CreateSale(Sale p, IFormFile formFile);
        public Task<bool> EditSale(Sale p);
        public Task<bool> DeleteSale(Guid id);
    }
}
