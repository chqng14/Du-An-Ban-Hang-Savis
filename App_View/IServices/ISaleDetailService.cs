using App_Data.Models;
using App_Data.ViewModel;

namespace App_View.IServices
{
    public interface ISaleDetailService
    {
        public Task<List<SaleDTViewModel>> GetAllDetaiSale();
        public Task<bool> CreateDetaiSale(SaleDetail p);
        public Task<bool> EditDetaiSale(SaleDetail p);
        public Task<bool> DeleteDetaiSale(Guid id);
    }
}
