using App_Data.Models;
using App_Data.ViewModel;

namespace App_View.IServices
{
    public interface ICartDetailService
    {
        Task<List<CartViewModel>> GetCartDetailsAsync();
        Task<bool> CreateCartDetailAsync(CartDetails cartDetails);
        Task<bool> EditCartDetailAsync(CartDetails cartDetails);
        Task<bool> DeleteCartDetailAsync(Guid id);
        Task<CartDetails> GetCartDetailByIdAsync(Guid id);
    }
}
