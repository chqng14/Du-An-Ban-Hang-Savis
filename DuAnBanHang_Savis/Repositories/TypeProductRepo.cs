using App_Data.IRepositories;
using App_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Data.Repositories
{
    public class TypeProductRepo : ITypeProductRepo
    {
        private readonly DbContextModel _dbContext;
        public TypeProductRepo()
        {
            _dbContext = new DbContextModel();
        }
        public List<TypeProduct> GetAllProductType()
        {
            // Sử dụng Entity Framework để truy vấn cơ sở dữ liệu
            return _dbContext.TypeProducts.ToList();
        }
    }
}
