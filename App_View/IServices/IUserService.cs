using App_Data.Models;

namespace App_View.IServices
{
    public interface IUserService
    {
        Task<List<User>> GetUsersAsync();
        Task<bool> CreateUserAsync(User user);
        Task<bool> EditUserAsync(Guid id, User user);
        Task<bool> DeleteUserAsync(Guid id);
        Task<User> GetUserByIdAsync(Guid id);
    }
}
