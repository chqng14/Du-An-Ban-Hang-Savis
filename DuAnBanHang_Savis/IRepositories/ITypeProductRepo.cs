using App_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Data.IRepositories
{
    public interface ITypeProductRepo
    {
        public List<TypeProduct> GetAllProductType();
    }
}
