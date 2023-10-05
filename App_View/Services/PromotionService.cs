using App_Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace App_View.Services
{
    public class PromotionService
    {
        DbContextModel _dbContext=new DbContextModel();

        public PromotionService()
        {
            _dbContext = new DbContextModel();
        }

        public void CheckNgayKetThuc()
        {
            var ngayKetThucSale = _dbContext.Sales
                .Where(p => p.NgayKetThuc <= DateTime.Now && p.TrangThai != 0)
                .ToList();

            foreach (var sale in ngayKetThucSale)
            {
                sale.TrangThai = 0;
            }

            _dbContext.SaveChanges();
        }
    }
}
