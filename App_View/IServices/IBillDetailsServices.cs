using App_Data.Models;
using App_Data.ViewModel;

namespace App_View.IServices
{
    public interface IBillDetailsServices
    {
        public Task<List<BillDetails>> GetAllAsync();
        public Task<List<BillDetails>> GetById(Guid id);
        public Task<List<BillDetailView>> GetByBill(Guid id);
        public Task<bool> AddItemAsync(BillDetails item);
        public Task<bool> RemoveItem(BillDetails item);
        public Task<bool> EditItem(BillDetails item);
    }
}
