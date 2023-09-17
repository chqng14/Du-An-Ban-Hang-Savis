using App_Data.Models;
using System;

namespace App_View.IServices
{
    public interface ITypeProductServices
    {
        public Task<List<Color>> GetAllTypeProduct();
        public Task<bool> EditTypeProduct(TypeProduct typeProduct);
        public Task<bool> DeleteTypeProduct(Guid id);
        public Task<bool> AddTypeProduct(TypeProduct typeProduct);
    }
}
