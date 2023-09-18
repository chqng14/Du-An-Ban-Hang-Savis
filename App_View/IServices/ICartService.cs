using App_Data.Models;

namespace App_View.IServices
{
    public interface ICartService
    {
        Task<List<Cart>> GetCartsAsync();
        Task<bool> CreateCartAsync(Cart cart);
        Task<bool> UpdateCartAsync(Guid IdUser, Cart cart);
        //Task<bool> DeleteCartAsync(Guid IdUser);
        Task<Cart> GetCartByIdAsync(Guid IdUser);
    }
}
