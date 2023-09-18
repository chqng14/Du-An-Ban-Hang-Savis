using App_Data.Models;

namespace App_View.IServices
{
    public interface IRoleService
    {
        Task<List<Role>> GetRolesAsync();
        Task<bool> CreateRoleAsync(Role role);
        Task<bool> EditRoleAsync(Guid id, Role role);
        Task<bool> DeleteRoleAsync(Guid id);
        Task<Role> GetRoleByIdAsync(Guid id);
    }
}
