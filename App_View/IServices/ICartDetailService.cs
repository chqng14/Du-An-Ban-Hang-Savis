using App_Data.Models;

namespace App_View.IServices
{
    public interface ICartDetailService
    {
        Task<List<CartDetails>> GetCartDetailsAsync();
        Task<bool> CreateCartDetailAsync(CartDetails cartDetails);
        Task<bool> EditCartDetailAsync(Guid id, CartDetails cartDetails);
        Task<bool> DeleteCartDetailAsync(Guid id);
        Task<CartDetails> GetCartDetailByIdAsync(Guid id);
    }
}
