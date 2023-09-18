using App_Data.Models;
using System;

namespace App_View.IServices
{
    public interface IMaterialServices
    {
        public Task<List<Material>> GetAllMateroal();
        public Task<bool> AddMaterial(Material material);
        public Task<bool> RemoveMaterial(Guid id);
        public Task<bool> Edit(Material material);
    }
}
